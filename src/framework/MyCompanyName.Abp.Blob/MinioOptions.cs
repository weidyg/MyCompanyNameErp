namespace MyCompanyName.Abp.Blob
{
    public class MinioOptions
    {
        public string EndPoint { get; set; }

        public string AccessKey { get; set; }

        public string SecretKey { get; set; }
        public string BucketName { get; set; }
    }
}