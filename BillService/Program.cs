using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BillService
{
    class MainClass
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

       
      

        public static void Main(string[] args)
        {
            try
            {
                StreamReader file = File.OpenText("config.json");
                JsonTextReader reader = new JsonTextReader(file);
                JObject jsonObject = (JObject)JToken.ReadFrom(reader);
                //MainClass.CAN_Communication = (bool)jsonObject["CAN"];
                //AccCode = (uint)jsonObject["AccCode"];
                //Id = (uint)jsonObject["Id"];

                //// Configure Json
                //BPointMove = (bool)jsonObject["BPointMove"];
                //_classLeft.DelayBPointMove = (int)jsonObject["L_BPointMoveDelay"];
                //_classRight.DelayBPointMove = (int)jsonObject["R_BPointMoveDelay"];
                #region 高凌话单读取
                var br = new BinaryReader(new FileStream("20160608.cdr", FileMode.Open));
                Console.WriteLine("Total Records:" + (br.BaseStream.Length/96).ToString());
                while (br.BaseStream.Position < br.BaseStream.Length)
                {

                    // Console.WriteLine("Remain Bytes:"+ (br.BaseStream.Length - br.BaseStream.Position).ToString());

                    var glCdr = new GaoLingCDR()
                    {
                        LetterCode = br.ReadBytes(1),
                        ModuleNumber = br.ReadBytes(1),
                        CallCategory = br.ReadBytes(1),
                        CallerAreaCode = br.ReadBytes(1),
                        CalleeAreaCode = br.ReadBytes(1),
                        EnterDirectionSign = br.ReadBytes(1), // 入局局向号/路由号
                        BillingType = br.ReadBytes(1), //
                        FreeOCharge = br.ReadBytes(1),
                        StationOrNewServiceType = br.ReadBytes(1),
                        CallerAndCalleeAddress = br.ReadBytes(1),
                        CalleeNumber = br.ReadBytes(12),
                        CallerUserType = br.ReadBytes(1),
                        BillingSign = br.ReadBytes(1),
                        CallEndDate = br.ReadBytes(4),
                        OutTo = br.ReadBytes(1),
                        Duration = br.ReadBytes(3),
                        HangupReason = br.ReadBytes(1),
                        HangupTime = br.ReadBytes(3),
                        CallerNumer = br.ReadBytes(10),
                        CTXCommunityNo = br.ReadBytes(1),
                        CTXGroupNo = br.ReadBytes(1),
                        ISDNType = br.ReadBytes(1),
                        AlternateNumber = br.ReadBytes(1),

                        NewBusinesIdentity = br.ReadBytes(7),
                        UUS1 = br.ReadBytes(1),
                        UUS2 = br.ReadBytes(1),
                        BillingAddressNature = br.ReadBytes(1),
                        BillingNumber = br.ReadBytes(10),
                        CallerSpecialNumber = br.ReadBytes(4),
                        CalleeSpecialNumber = br.ReadBytes(4),
                        ConnectionNumberType = br.ReadBytes(1),

                        TakeLength = br.ReadBytes(3),
                        ConnectionNnumber = br.ReadBytes(8),
                        CIC = br.ReadBytes(3),
                        Other = br.ReadBytes(3), //文档记录有错误，应该是93-95

                    };

                    // Convert.ToInt32("28de1212", 16);
                    //Console.WriteLine(Convert.ToInt32(BitConverter.ToString(glCdr.CallCategory), 16));


                    Console.Write("CallEndDate:" + BitConverter.ToString(glCdr.CallEndDate));
                    Console.Write(" HangupTime:" + BitConverter.ToString(glCdr.HangupTime));
                    Console.Write(" Duration:");
                    foreach (byte b in glCdr.Duration)
                    {
                        // 需要确认这个是取3个的和还是？
                        var bstr = b.ToString("X2");

                        Console.Write(Convert.ToInt32(bstr, 16).ToString() + " ");
                    }

                    Console.Write(" CallerNumer:");

                    var CallerNumber = "";
                    for (var i = 0; i < 4; i++)
                    {
                        var str = Conver16To2Left2(glCdr.CallerNumer[i]);

                        CallerNumber += ConvertBCDToInt2(str.Substring(0, 4), 2).ToString();
                        CallerNumber += ConvertBCDToInt2(str.Substring(3, 4), 2).ToString();

                    }
                    Console.Write(CallerNumber);



                    Console.Write(" CalleeNumber:");

                    var CalleeNumber = "";
                    for (var i = 0; i < 4; i++)
                    {
                        var str = Conver16To2Left2(glCdr.CalleeNumber[i]);

                        CalleeNumber += ConvertBCDToInt2(str.Substring(0, 4), 2).ToString();
                        CalleeNumber += ConvertBCDToInt2(str.Substring(3, 4), 2).ToString();

                    }
                    Console.Write(CalleeNumber);
                    Console.WriteLine();




                    // 下面是测试会用到的日志输出 不要删除

                    //Console.WriteLine("LetterCode:" + Conver16To10(glCdr.LetterCode).ToString());
                    //Console.WriteLine("ModuleNumber:" + Conver16To10(glCdr.ModuleNumber).ToString());
                    //Console.WriteLine("CallCategory:" + Conver16To10(glCdr.CallCategory).ToString());
                    //Console.WriteLine("CallerAreaCode:" + Conver16To10(glCdr.CallerAreaCode).ToString());
                    //Console.WriteLine("CallerAreaCode:" + Conver16To10(glCdr.CallerAreaCode).ToString());
                    //Console.WriteLine("CalleeAreaCode:" + Conver16To10(glCdr.CalleeAreaCode).ToString());
                    //Console.WriteLine("EnterDirectionSign:" + Conver16To10(glCdr.EnterDirectionSign).ToString());
                    //Console.WriteLine("BillingType:" + Conver16To10(glCdr.BillingType).ToString());
                    //Console.WriteLine("FreeOCharge:" + Conver16To10(glCdr.FreeOCharge).ToString());
                    //Console.WriteLine("StationOrNewServiceType:" + Conver16To10(glCdr.StationOrNewServiceType).ToString());
                    //Console.WriteLine("CallerAndCalleeAddress:" + Conver16To10(glCdr.CallerAndCalleeAddress).ToString());


                    //Console.Write("CalleeNumber 2421:");

                    //var CalleeNumber = "";
                    //for (var i = 0; i < 4; i++)
                    //{
                    //    var str = Conver16To2Left2(glCdr.CalleeNumber[i]);

                    //    CalleeNumber += ConvertBCDToInt2(str.Substring(0, 4), 2).ToString();
                    //    CalleeNumber += ConvertBCDToInt2(str.Substring(3, 4), 2).ToString();

                    //}
                    //Console.WriteLine(CalleeNumber);

                    //Console.WriteLine();

                    //Console.WriteLine("CallerUserType:" + Conver16To10(glCdr.CallerUserType).ToString());

                    //Console.WriteLine("BillingSign:" + Conver16To10(glCdr.BillingSign).ToString());

                    //Console.WriteLine("CallEndDate:" + BitConverter.ToString(glCdr.CallEndDate));

                    //Console.WriteLine("OutTo:" + Conver16To10(glCdr.OutTo).ToString());


                    //Console.Write("Duration:");
                    //foreach (byte b in glCdr.Duration)
                    //{
                    //    // 需要确认这个是取3个的和还是？
                    //    var bstr = b.ToString("X2");

                    //    Console.Write(Convert.ToInt32(bstr, 16).ToString() + " ");
                    //}
                    //Console.WriteLine();


                    //Console.WriteLine("HangupReason:" + Conver16To10(glCdr.HangupReason).ToString()); //需要开处理 暂时不管 TODO
                    //Console.WriteLine("HangupTime:" + BitConverter.ToString(glCdr.HangupTime));


                    //Console.Write("CallerNumer 8421:");

                    //var CallerNumber = "";
                    //for (var i = 0; i < 4; i++)
                    //{
                    //    var str = Conver16To2Left2(glCdr.CallerNumer[i]);

                    //    CallerNumber += ConvertBCDToInt2(str.Substring(0, 4), 8).ToString();
                    //    CallerNumber += ConvertBCDToInt2(str.Substring(3, 4), 8).ToString();

                    //}
                    //Console.WriteLine(CallerNumber);


                    //Console.Write("CallerNumer 5421:");

                    //CallerNumber = "";
                    //for (var i = 0; i < 4; i++)
                    //{
                    //    var str = Conver16To2Left2(glCdr.CallerNumer[i]);

                    //    CallerNumber += ConvertBCDToInt2(str.Substring(0, 4), 5).ToString();
                    //    CallerNumber += ConvertBCDToInt2(str.Substring(3, 4), 5).ToString();

                    //}
                    //Console.WriteLine(CallerNumber);


                    //Console.Write("CallerNumer 2421:");

                    //CallerNumber = "";
                    //for (var i = 0; i < 4; i++)
                    //{
                    //    var str = Conver16To2Left2(glCdr.CallerNumer[i]);

                    //    CallerNumber += ConvertBCDToInt2(str.Substring(0, 4), 2).ToString();
                    //    CallerNumber += ConvertBCDToInt2(str.Substring(3, 4), 2).ToString();

                    //}
                    //Console.WriteLine(CallerNumber);


                    //Console.WriteLine("CTXCommunityNo:" + Conver16To10(glCdr.CTXCommunityNo).ToString());

                    //Console.WriteLine("CTXGroupNo:" + Conver16To10(glCdr.CTXGroupNo).ToString());

                    //Console.WriteLine("ISDNType:" + Conver16To10(glCdr.ISDNType).ToString());

                    //Console.WriteLine("AlternateNumber:" + Conver16To10(glCdr.AlternateNumber).ToString());
                    //Console.WriteLine("NewBusinesIdentity:" + BitConverter.ToString(glCdr.ISDNType));
                    //Console.WriteLine("UUS1:" + Conver16To10(glCdr.UUS1).ToString());
                    //Console.WriteLine("UUS2:" + Conver16To10(glCdr.UUS2).ToString());

                    //Console.WriteLine("BillingAddressNature:" + Conver16To10(glCdr.BillingAddressNature).ToString());



                    //Console.Write("BillingNumber 8421:");

                    //var BillingNumber = "";
                    //for (var i = 0; i < 4; i++)
                    //{
                    //    var str = Conver16To2Left2(glCdr.BillingNumber[i]);

                    //    BillingNumber += ConvertBCDToInt2(str.Substring(0, 4), 8).ToString();
                    //    BillingNumber += ConvertBCDToInt2(str.Substring(3, 4), 8).ToString();

                    //}
                    //Console.WriteLine(BillingNumber);


                    //Console.Write("BillingNumber 5421:");

                    //BillingNumber = "";
                    //for (var i = 0; i < 4; i++)
                    //{
                    //    var str = Conver16To2Left2(glCdr.BillingNumber[i]);

                    //    BillingNumber += ConvertBCDToInt2(str.Substring(0, 4), 5).ToString();
                    //    BillingNumber += ConvertBCDToInt2(str.Substring(3, 4), 5).ToString();

                    //}
                    //Console.WriteLine(BillingNumber);


                    //Console.Write("BillingNumber 2421:");

                    //BillingNumber = "";
                    //for (var i = 0; i < 4; i++)
                    //{
                    //    var str = Conver16To2Left2(glCdr.BillingNumber[i]);

                    //    BillingNumber += ConvertBCDToInt2(str.Substring(0, 4), 2).ToString();
                    //    BillingNumber += ConvertBCDToInt2(str.Substring(3, 4), 2).ToString();

                    //}
                    //Console.WriteLine(BillingNumber);

                    //Console.WriteLine("CallerSpecialNumber:" + BitConverter.ToString(glCdr.CallerSpecialNumber));
                    //Console.WriteLine("CalleeSpecialNumber:" + BitConverter.ToString(glCdr.CalleeSpecialNumber));
                    //Console.WriteLine("ConnectionNumberType:" + Conver16To10(glCdr.ConnectionNumberType).ToString());

                    //Console.Write("TakeLength:");
                    //foreach (byte b in glCdr.TakeLength)
                    //{
                    //    // 需要确认这个是取3个的和还是？
                    //    var bstr = b.ToString("X2");

                    //    Console.Write(Convert.ToInt32(bstr, 16).ToString() + " ");
                    //}
                    //Console.WriteLine();

                    //Console.WriteLine("ConnectionNnumber:" + BitConverter.ToString(glCdr.ConnectionNnumber));

                    //Console.WriteLine("CIC:" + BitConverter.ToString(glCdr.CIC));

                    //Console.WriteLine("Other:" + BitConverter.ToString(glCdr.Other));





                }
                br.Close();
                #endregion

                #region 中兴话单读取
                var zxbr = new BinaryReader(new FileStream("JF201804.B21", FileMode.Open));
                Console.WriteLine("Total Records:" + (zxbr.BaseStream.Length / 123).ToString());
                while (zxbr.BaseStream.Position < zxbr.BaseStream.Length)
                {
                    Console.WriteLine("Remain Bytes:"+ (zxbr.BaseStream.Length - zxbr.BaseStream.Position).ToString());
                    var zxCdr = new ZXJX10CDR()
                    {
                        RecordType = zxbr.ReadBytes(1),
                        PartRecordID = zxbr.ReadBytes(1),
                        NatureAddressOfCallerNumber = zxbr.ReadBytes(1),
                        CallerNumber = zxbr.ReadBytes(20),
                        NatureAddressOfCalleeNumber = zxbr.ReadBytes(1),
                        CalleeNumber = zxbr.ReadBytes(20),
                        StartTime = zxbr.ReadBytes(4),
                        StartTicks = zxbr.ReadBytes(1),
                        ServiceCategory = zxbr.ReadBytes(1),
                        EndTime = zxbr.ReadBytes(4),
                        EndTicks = zxbr.ReadBytes(1),
                        ReleaseReason = zxbr.ReadBytes(1),
                        CallerType = zxbr.ReadBytes(1),
                        CallProperties = zxbr.ReadBytes(1),
                        IncomingTrunkGroup = zxbr.ReadBytes(2),
                        OutgoingTrunkGroup = zxbr.ReadBytes(2),
                        SupplementServicee = zxbr.ReadBytes(7),
                        ChargePartyID = zxbr.ReadBytes(1),
                        NatureAddressOfLinkNumber = zxbr.ReadBytes(1),
                        LinkNumber = zxbr.ReadBytes(20),
                        Fee = zxbr.ReadBytes(4),
                        BearerServices = zxbr.ReadBytes(1),
                        TerminalServices = zxbr.ReadBytes(1),
                        UUS1 = zxbr.ReadBytes(1),
                        UUS3 = zxbr.ReadBytes(1),
                        CallerSpecialNumber = zxbr.ReadBytes(5),
                        CalleeSpecialNumber = zxbr.ReadBytes(5),
                        CentrexGroupID = zxbr.ReadBytes(2),
                        NatureAddressOfBilledNumber = zxbr.ReadBytes(1),
                        BilledNumber = zxbr.ReadBytes(11)
                    };

                   Console.Write(" CallerNumer:");

                    var CallerNumber = "";
                    for (var i = 0; i < 4; i++)
                    {
                        var str = Conver16To2Left2(zxCdr.CallerNumber[i],'0');

                        CallerNumber += ConvertBCDToInt2(str.Substring(0, 4), 2).ToString();
                        CallerNumber += ConvertBCDToInt2(str.Substring(3, 4), 2).ToString();

                    }
                    Console.Write(CallerNumber);



                    Console.Write(" CalleeNumber:");

                    var CalleeNumber = "";
                    for (var i = 0; i < 4; i++)
                    {
                        var str = Conver16To2Left2(zxCdr.CalleeNumber[i],'0');

                        CalleeNumber += ConvertBCDToInt2(str.Substring(0, 4), 2).ToString();
                        CalleeNumber += ConvertBCDToInt2(str.Substring(3, 4), 2).ToString();

                    }
                    Console.Write(CalleeNumber);
                    Console.WriteLine();


                }
                zxbr.Close();
                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey(true);
        }



    }
}
