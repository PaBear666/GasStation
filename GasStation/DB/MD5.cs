using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.DB
{
    public class Md5
    {
        public static string hashPassword(string password)
        {
            var md5 = MD5.Create();
            byte[] b = Encoding.UTF8.GetBytes(password);
            byte[] hash = md5.ComputeHash(b);
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i]);
            }
            return sb.ToString();

        }
    }
}
