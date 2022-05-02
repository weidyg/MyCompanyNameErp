using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Aliyun;
using Volo.Abp.BlobStoring.Minio;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace MyCompanyName.Abp.Blob
{
    [DependsOn(
       typeof(AbpMultiTenancyModule),
       typeof(AbpBlobStoringMinioModule),
       typeof(AbpBlobStoringAliyunModule)
   )]
    public class MyCompanyNameAbpBlobModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var minioEndPoint = configuration["Minio:EndPoint"];
            var minioAccessKey = configuration["Minio:AccessKey"];
            var minioSecretKey = configuration["Minio:SecretKey"];
            var minioBucketName = configuration["Minio:BucketName"];
            //var aliyunEndpoint = configuration["Aliyun:Endpoint"];
            //var aliyunAccessKeyId = configuration["Aliyun:AccessKeyId"];
            //var aliyunAccessKeySecret = configuration["Aliyun:AccessKeySecret"];
            //var aliyunContainerName = configuration["Aliyun:ContainerName"];

            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.ConfigureDefault(container =>
                {
                    container.UseMinio(minio =>
                    {
                        minio.EndPoint = minioEndPoint;
                        minio.AccessKey = minioAccessKey;
                        minio.SecretKey = minioSecretKey;
                        minio.BucketName = minioBucketName ?? "erp";
                        minio.CreateBucketIfNotExists = true;
                    });
                    //container.UseAliyun(aliyun =>
                    //{
                    //    aliyun.Endpoint = aliyunEndpoint;
                    //    aliyun.AccessKeyId = aliyunAccessKeyId;
                    //    aliyun.AccessKeySecret = aliyunAccessKeySecret;
                    //    aliyun.ContainerName = aliyunContainerName ?? "erp";
                    //    aliyun.CreateContainerIfNotExists = true;
                    //});
                });
            });
        }
    }
}
