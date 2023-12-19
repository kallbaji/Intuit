using Google.Protobuf.WellKnownTypes;
using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UploadDBOperation : IUploadInterface
    {
        public IDatabaseOperationInterface dbHelper { get; set; }
        public IVideoOperationInterface uploadHelper { get; set; }

        public static readonly UploadDBOperation Instance = new UploadDBOperation();
        private UploadDBOperation()
        {
                
        }
        public async Task<bool> UploadVideo(string fileName, string tumbhnail, string filePath,int userIndex)
        {
            try
            {

            
            await uploadHelper.UploadFile(fileName, filePath);
            await uploadHelper.UploadFile(fileName + "_Tumbnail", tumbhnail);
            DateTime theDate = DateTime.Now;
            dbHelper.OpenConnection();
            dbHelper.ExecuteQuery(String.Format("INSERT INTO video(title, uploader, thumbnail,video,views,date_upload) VALUES(\"{0}\", " + userIndex + ", \"{1}\",\"{2}\",0,\'{3}\');", fileName, uploadHelper.CreateURLS3(fileName + "_Tumbnail"), uploadHelper.CreateURLS3(fileName), theDate.ToString("yyyy-MM-dd H:mm:ss")));
           
            }
            catch (Exception ex)
            {

               return false;
            }

        finally { dbHelper.CloseConnection(); }
            return true;
        }
    }
}
