using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IVideoOperationInterface
    {
         Task<bool> UploadFile(string keyName, string filePath);

        Task<bool> DownloadFile(string keyName, string filePath);

        string CreateURLS3(string key);
    }
}
