using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IViedoInfoInterface
    {

        IDatabaseOperationInterface dbHelper { get; set; }
        Task<bool> DownloadFile(string title, string fileName);

        List<Comment> GetComments(int videoIndex);
        void AddViewCount(int views, int videoIndex);

        void PostComment(int uid, string content, int videoIndex);

        void DeleteComment(int cid);

        List<Rate> GetRate(int vid);

        RateEnum GetRate(int vid,int uid);

        void DeleteRate(int uid, int vid);
        void InsertRate (int uid,int vid, int rate);
    }
}
