using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace FoodOrg.Common
{
    /// <summary>
    /// Security 的摘要描述
    /// </summary>
    public class ClashFlowSecurity
    {
        public ClashFlowSecurity()
	    {
		    //
		    // TODO: 在此加入建構函式的程式碼
		    //
	    }

        public static string strMerchantID = "1039415";
        //All in One HashKey
        static string strHashKey = "u0gfkFRGB32Ogzcu";
        //StrHahIv
        static string strHashIV = "p5ihxawFRkGQ0zeV";


        ////public static string strMerchantID = "2000132";
        ////all in one介接的HashKey
        ////static string strHashKey = "5294y06JbISpM5x9";
        ////all in one介接的HashIV
        ////static string strHashIV = "v77hoKGq4kWxNNIS";



        //一般金流
	    //宣告資料加密標準 (DES) 演算法的秘密金鑰。
        static string mstrKey = "u0gfkFRGB32Ogzcu";
	    //宣告對稱演算法的初始化向量 (IV)。
        static string mstrIV = "p5ihxawFRkGQ0zeV";


        #region AES密算法

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="strValue">原始字串</param>
        /// <returns>AES加密後字串</returns> 
        public static string Encrypt(string strValue)
        {
            //將加密字串、Key與IV轉為所指定的由 base 64 數字所組成之值的 String 表示轉換為相等的 8 位元不帶正負號的整數陣列。
            byte[] buffer = Encoding.Default.GetBytes(strValue);
            byte[] key = Encoding.Default.GetBytes(mstrKey);
            byte[] iv = Encoding.Default.GetBytes(mstrIV);

            //宣告ms為記憶體為資料來源的資料流。
            MemoryStream ms = new MemoryStream();

            //定義包裝函式 (Wrapper) 物件，以存取資料加密標準 (DES) 演算法的密碼編譯服務供應者 (CSP) 版本。
            //DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            AesManaged aes = new AesManaged();
            aes.Mode = CipherMode.CBC;
            aes.BlockSize = 128;

            aes.Padding = PaddingMode.PKCS7;
            //使用指定的 Key 和初始化向量 (IV)，建立對稱資料加密標準 (DES) 加密子物件。
            ICryptoTransform transform = aes.CreateEncryptor(key, iv);

            //定義連結資料流到密碼編譯轉換的資料流。建構函式==>CryptoStream(要在其上執行密碼編譯轉換的資料流,要在資料流上執行的密碼編譯轉換,密碼編譯資料流的模式)
            CryptoStream encStream = new CryptoStream(ms, transform, CryptoStreamMode.Write);

            //encStream.Write(從 buffer 複製 count 位元組到目前資料流,開始複製位元組到目前資料流的 buffer 的位元組位移,寫入目前資料流的位元組數目)
            encStream.Write(buffer, 0, buffer.Length);

            //以緩衝區的目前狀態更新基礎資料來源或存放庫，並接著清除緩衝區。
            encStream.FlushFinalBlock();

            //將 8 位元不帶正負號的整數陣列的值，轉換為由 base 64 數字所組成的相等 String 表示。
            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="strValue">AES加密後字串</param>
        /// <returns>原始字串</returns> 
        public static string Decrypt(string strValue)
        {

            //將加密字串、Key與IV轉為所指定的由 base 64 數字所組成之值的 String 表示轉換為相等的 8 位元不帶正負號的整數陣列。
            byte[] buffer = Convert.FromBase64String(strValue);
            byte[] key = Encoding.Default.GetBytes(mstrKey);
            byte[] iv = Encoding.Default.GetBytes(mstrIV);

            //宣告ms為記憶體為資料來源的資料流。
            MemoryStream ms = new MemoryStream();

            //定義包裝函式 (Wrapper) 物件，以存取資料加密標準 (DES) 演算法的密碼編譯服務供應者 (CSP) 版本。
            //DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            AesManaged aes = new AesManaged();
            aes.Mode = CipherMode.CBC;
            aes.BlockSize = 128;
            aes.Padding = PaddingMode.PKCS7;
            //使用指定的 Key 和初始化向量 (IV)，建立對稱資料加密標準 (DES) 解密子物件。
            ICryptoTransform transform = aes.CreateDecryptor(key, iv);

            //定義連結資料流到密碼編譯轉換的資料流。建構函式==>CryptoStream(要在其上執行密碼編譯轉換的資料流,要在資料流上執行的密碼編譯轉換,密碼編譯資料流的模式)
            CryptoStream encStream = new CryptoStream(ms, transform, CryptoStreamMode.Write);

            //encStream.Write(從 buffer 複製 count 位元組到目前資料流 ,開始複製位元組到目前資料流的 buffer 的位元組位移,寫入目前資料流的位元組數目)
            encStream.Write(buffer, 0, buffer.Length);

            //以緩衝區的目前狀態更新基礎資料來源或存放庫，並接著清除緩衝區。
            encStream.FlushFinalBlock();

            //將 8 位元不帶正負號的整數陣列的值，轉換為由 base 64 數字所組成的相等 String 表示。
            return Encoding.Default.GetString(ms.ToArray());

        }

        #endregion

        #region MD5密算法

        /// <summary>
        /// DM5加密
        /// </summary>
        /// <param name="strValue">原始字串</param>
        /// <returns>MD5加密後字串</returns> 
        public static string MD5Encrypt(string strValue)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.ASCII.GetBytes(strValue);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2"));
            }
            strValue = s.ToString();
            return strValue;
        }


        public static string MD5Encrypt2(string phrase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            MD5CryptoServiceProvider md5hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes = md5hasher.ComputeHash(encoder.GetBytes(phrase));

            return byteArrayToString(hashedDataBytes);
        }

        private static string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");

            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }

            return output.ToString();
        }

        #endregion

        /// <summary>
        /// 設定CheckValue回傳
        /// </summary>
        public static string SetCheckValue(string strCheckValue)
        {
            strCheckValue = "HashKey=" + strHashKey + "&" + strCheckValue + "HashIV=" + strHashIV;
            strCheckValue = HttpUtility.UrlEncode(strCheckValue, System.Text.Encoding.UTF8);
            //轉成小寫
            strCheckValue = strCheckValue.ToLower();
            //md5加密
            return strCheckValue = ClashFlowSecurity.MD5Encrypt(strCheckValue);
        }     


    }
}