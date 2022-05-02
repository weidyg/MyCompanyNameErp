using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sunton.Erp.BlobManagement;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;

namespace Sunton.Erp.SystemService
{
    [Authorize]
    [Consumes("application/json", "multipart/form-data")]
    public class FileAppService : ApplicationService
    {
        private readonly PictureContainerManager _pictureContainer;
        public FileAppService(
            PictureContainerManager pictureContainer
            )
        {
            _pictureContainer = pictureContainer;
        }

        public async Task<string> SaveQrcodePictureAsync(IFormFile file, string name = null)
        {
            var fileName = $"{"qrcode"}/{GetFileName(file, name)}";
            return await SavePictureAsync(fileName, file, true);
        }

        public async Task<string> SaveProfilePictureAsync(IFormFile file)
        {
            var fileName = $"{"profile"}/{CurrentUser.GetId()}";
            return await SavePictureAsync(fileName, file, true);
        }



        #region 
        protected async Task<string> SavePictureAsync(string fileName, IFormFile file, bool overrideExisting = false)
        {
            var extension = Path.GetExtension(fileName);
            var ovName = overrideExisting ? "" : $"_{Guid.NewGuid()}";
            var storageFileName = $"{Path.GetFileNameWithoutExtension(fileName)}{ovName}{extension}";
            var stream = CheckIsImageAndGetStream(file);
            return await _pictureContainer.SaveAsync(storageFileName, stream, overrideExisting);
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
