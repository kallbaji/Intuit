using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Interface;
using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AWS_S3_Storage
{
    public class AWSStorage : IVideoOperationInterface
    {
        public static readonly AWSStorage Instance = new AWSStorage();
        private AWSStorage()
        {
                
        }
        private string bucketName = "intuitbuket";

        public async Task<bool> UploadFile(string keyName, string filePath)
        {
            var client = new AmazonS3Client("AKIA2YNUHHPWK7JUWOGF", "D8Wpeg4WpCQB3PpOaMMhkCnBF9mt8cCrLoew2PA8", Amazon.RegionEndpoint.APSouth1);

            try
            {
                PutObjectRequest putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    FilePath = filePath,
                    //  ContentType = "video/mp4"
                };

                PutObjectResponse response = await client.PutObjectAsync(putRequest);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DownloadFile(string keyName, string filePath)
        {
            
            var client = new AmazonS3Client("AKIA2YNUHHPWK7JUWOGF", "D8Wpeg4WpCQB3PpOaMMhkCnBF9mt8cCrLoew2PA8", Amazon.RegionEndpoint.APSouth1);

            try
            {
                GetObjectRequest getObjectRequest = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName
                };

                using (var response = await client.GetObjectAsync(getObjectRequest))
                {
                    await response.WriteResponseStreamToFileAsync(filePath + @"\" + keyName+"."+ response.Headers.ContentType.Split("/")[1], false,new System.Threading.CancellationToken());
                    
                }

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public string CreateURLS3(string key)
        {

            return @"https://intuitbuket.s3.ap-south-1.amazonaws.com/" + key;

        }
    }
}
