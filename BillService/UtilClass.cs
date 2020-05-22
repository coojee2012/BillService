using System;
namespace BillService
{
    public static class UtilClass
    {
        public static int Conver16To10(byte[] b)
        {
            return Convert.ToInt32(BitConverter.ToString(b), 16);
        }

        // 左对齐右边补0,默认，也可以根据需要传入补位
        public static string Conver16To2Left(byte[] b, char c = '0')
        {
            var str = Convert.ToString(Conver16To10(b), 2);


            return str.PadRight(8 - str.Length, c);
        }
        // 右对齐左边补0默认，也可以根据需要传入补位
        public static string Conver16To2Right(byte[] b, char c = '0')
        {
            var str = Convert.ToString(Conver16To10(b), 2);
            return str.PadLeft(8 - str.Length, c);
        }


        // 左对齐右边补0,默认，也可以根据需要传入补位
        public static string Conver16To2Left2(byte b, char c = '0')
        {
            var bstr = b.ToString("X2");
            //Console.WriteLine("test:" + bstr);
            var str = Convert.ToString(Convert.ToInt32(bstr, 16), 2);
            return str.PadRight(8, c);
        }


        private static byte ConvertBCD(byte b)//byte转换为BCD码
        {
            //高四位  
            byte b1 = (byte)(b / 10);
            //低四位  
            byte b2 = (byte)(b % 10);
            return (byte)((b1 << 4) | b2);
        }

        /// <summary>  
        /// 将BCD一字节数据转换到byte 十进制数据  
        /// </summary>  
        /// <param name="b" />字节数  
        /// <returns>返回转换后的BCD码</returns>  
        public static byte ConvertBCDToInt(byte b)
        {
            //高四位  
            byte b1 = (byte)((b >> 4) & 0xF);
            //低四位  
            byte b2 = (byte)(b & 0xF);

            return (byte)(b1 * 10 + b2);
        }

        public static int ConvertBCDToInt2(string s, int start = 8)
        {
            var a1 = Convert.ToInt32(s.Substring(0, 1));
            var a2 = Convert.ToInt32(s.Substring(1, 1));
            var a3 = Convert.ToInt32(s.Substring(2, 1));
            var a4 = Convert.ToInt32(s.Substring(3, 1));

            var result = a1 * start + a2 * 4 + a3 * 2 + a4 * 1;
            return result;
        }




    }
}
