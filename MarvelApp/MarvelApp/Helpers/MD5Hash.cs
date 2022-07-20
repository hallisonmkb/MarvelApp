using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MarvelApp.Helpers
{
    public static class MD5Hash
    {
        private static string GetMd5Hash(byte[] data)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sb.Append(data[i].ToString("x2"));
            return sb.ToString();
        }

        public static string Hash(string data)
        {
            using (var md5 = MD5.Create())
                return GetMd5Hash(md5.ComputeHash(Encoding.UTF8.GetBytes(data)));
        }
    }
}
