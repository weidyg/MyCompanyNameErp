using System.IO;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace MyCompanyName.Abp.Blob
{
    public class PictureBlobManager : ITransientDependency
    {
        public const string Path = "/file/image";

        private readonly IBlobContainer _blobContainer;

        public PictureBlobManager(
            IBlobContainer blobContainer)
        {
            _blobContainer = blobContainer;
        }

        public async Task<string> SaveAsync(string blobName, Stream stream, bool overrideExisting = false)
        {
            await _blobContainer.SaveAsync(blobName, stream, overrideExisting);
            return $"{Path}/{blobName}";
        }

        public async Task<byte[]> GetAllBytesOrNullAsync(string blobName)
        {
            return await _blobContainer.GetAllBytesOrNullAsync(blobName);
        }
    }
}
