using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IUploadInterface
    {
        Task<bool> UploadVideo(string fileName, string tumbhnail, string filePath, int userIndex);
    }
}
