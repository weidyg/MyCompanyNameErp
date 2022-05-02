using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCompanyName.Abp.Blob;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Users;

namespace MyCompanyName.Web.Shared.Controllers
{
    [Authorize]
    [Consumes("application/json", "multipart/form-data")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class FileController : AbpController
    {
        private readonly ICurrentUser _currentUser;
        private readonly PictureBlobManager _pictureContainer;
        public FileController(
            ICurrentUser currentUser,
            PictureBlobManager pictureContainer
            )
        {
            _currentUser = currentUser;
            _pictureContainer = pictureContainer;
        }

        [Route("[controller]/save-picture")]
        public async Task<string> SavePictureAsync(IFormFile file, string name = null, string type = null)
        {
            var fileName = $"{(type.IsNullOrWhiteSpace() ? "" : $"{type}/")}{GetFileName(file, name)}";
            return await SavePictureAsync(fileName, file, true);
        }

        [Route("[controller]/save-avatar")]
        public async Task<string> SaveAvatarAsync(IFormFile file)
        {
            var userId = _currentUser?.Id.ToString();
            if (userId.IsNullOrWhiteSpace()) { return ""; }
            var fileName = $"avatar/{userId}";
            return await SavePictureAsync(fileName, file, true);
        }

        [Route(PictureBlobManager.Path + "/{*blobName}")]
        public async Task<ActionResult> ImageAsync(string blobName)
        {
            var byteData = await _pictureContainer.GetAllBytesOrNullAsync(blobName);
            if (byteData == null) { return NoContent(); }
            var type = Path.GetExtension(blobName);
            type = type.IsNullOrWhiteSpace() ? "png" : type.TrimStart('.');
            if (type == "svg") { type = "svg+xml"; }
            return File(byteData, $"image/{type}");
        }

        #region 
        protected async Task<string> SavePictureAsync(string fileName, IFormFile file, bool overrideExisting = false)
        {
            var extension = Path.GetExtension(fileName);
            var newfileName = overrideExisting ? fileName : fileName.Replace(extension, $"_{Guid.NewGuid()}{extension}");
            var stream = CheckIsImageAndGetStream(file);
            return await _pictureContainer.SaveAsync(newfileName, stream, overrideExisting);
        }

        protected static string GetFileName(IFormFile file, string name = null)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = name.IsNullOrWhiteSpace() ? file.FileName : $"{name}{extension}";
            return fileName;
        }

        protected static Stream CheckIsImageAndGetStream(IFormFile file)
        {
            if (!(file?.ContentType?.ToLower().StartsWith("image") ?? false))
            {
                throw new UserFriendlyException($"图片格式不正确！");
            }
            var stream = file.OpenReadStream();
            return stream;
        }
        #endregion
    }
}
