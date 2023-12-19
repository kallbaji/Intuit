using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IVideoInterface
    {
        List<Video> GetVideos();
        List<Video> GetVideos(int userIndex);
    }
}
