using System;
using System.Linq;

namespace CreateQRCode
{
    /// <summary>
    /// 加密解密
    /// </summary>
    class EncryptAndDecode
    {
        // 加密
        public String EncryptCode(String number)
        {
            char[] arrStr = number.ToArray();
            for (int i = 0; i < arrStr.Length; i++)
            {
                arrStr[i] = (char)((arrStr[i] - 41) % 10 + 48);
            }
            return new String(arrStr);
        }

        // 解密
        public String Decode(String number)
        {
            char[] arrStr = number.ToArray();
            for (int i = 0; i < arrStr.Length; i++)
            {
                arrStr[i] = (char)((arrStr[i] - 45) % 10 + 48);
            }
            return new String(arrStr);
        }
    }
}
