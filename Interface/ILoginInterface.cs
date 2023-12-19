using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface ILoginInterface
    {
        Tuple<bool,Profile> OnCreate(string username, string password, string name);
        Tuple<bool,Profile> OnLogin(string username, string password);
    }
}
