using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

//https://dotblogs.com.tw/shadow/2019/03/20/232821

namespace AESEncoder
{
    public class Code
    {
        public class CodeConfiguration
        {
            public string Key { get; set; }
            public string IV { get; set; }
            public string Pwd { get; set; }
        }

        public CodeConfiguration Encoder(string pwd)
        {
            var sourceBytes = Encoding.UTF8.GetBytes(pwd);

            var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // aes key 有支援的格式長度，所以字串轉不出這種長度就會出錯
            //aes.Key = Encoding.UTF8.GetBytes(_key);

            // aes iv 有支援的格式長度 長度要是 key的一半
            //aes.IV = Encoding.UTF8.GetBytes("123");

            aes.Key = GenerateBitsOfRandomEntropy(32);
            aes.IV = GenerateBitsOfRandomEntropy(16);

            var transform = aes.CreateEncryptor();

            return new CodeConfiguration()
            {
                Key = Convert.ToBase64String(aes.Key),
                IV = Convert.ToBase64String(aes.IV),
                Pwd = Convert.ToBase64String(transform.TransformFinalBlock(sourceBytes, 0, sourceBytes.Length))
            };
        }

        private static byte[] GenerateBitsOfRandomEntropy(int num)
        {
            // 32 Bytes will give us 256 bits.
            var randomBytes = new byte[num];

            Random genByteValue = new Random();
            genByteValue.NextBytes(randomBytes);

            return randomBytes;
        }
    }
}