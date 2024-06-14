using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibManager.Services
{
    public class BarcodeService
    {
        public const string ISBN_HEAD = "978";
        public const int ISBN_LENGTH = 13;
        public const int USERCODE_LENGTH = 1;
        public static bool CheckIsIsbn(string code)
        {
            int length = code.Length;
            
            if(length != ISBN_LENGTH)
            {
                return false;
            }

            string head = code.Substring(0, ISBN_HEAD.Length);

            if(head != ISBN_HEAD)
            {
                return false;
            }

            return true;



        }

        public static bool CheckIsUserCode(string code)
        {

            return true;
        }
    }
}
