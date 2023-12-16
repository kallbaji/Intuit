using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AWS_S3_Storage
{
    public class AWSStorage
    {
        private string bucketName = "intuitbuket";
        private string keyName = "Remove_Filter.mp4";
        private string filePath = @"C:\Users\432179\Desktop\WZInProgressIssue.mp4";



        public async Task<HttpStatusCode> UploadFile()
        {
            var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);

            try
            {
                PutObjectRequest putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    FilePath = filePath,
                    ContentType = "video/mp4"
                };

                PutObjectResponse response = await client.PutObjectAsync(putRequest);
               return response.HttpStatusCode;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.NoContent;
            }
        }


    }
}
