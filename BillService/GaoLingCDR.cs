using System;
namespace BillService
{
    public class GaoLingCDR
    {
        public byte[] LetterCode { get; set; }
        public byte[] ModuleNumber { get; set; }
        public byte[] CallCategory { get; set; }
        public byte[] CallerAreaCode { get; set; }
        public byte[] CalleeAreaCode { get; set; }
        public byte[] EnterDirectionSign { get; set; } // 入局局向号/路由号
        public byte[] BillingType { get; set; } //
        public byte[] FreeOCharge { get; set; }
        public byte[] StationOrNewServiceType { get; set; }
        public byte[] CallerAndCalleeAddress { get; set; }
        public byte[] CalleeNumber { get; set; }
        public byte[] CallerUserType { get; set; }
        public byte[] BillingSign { get; set; }
        public byte[] CallEndDate { get; set; }
        public byte[] OutTo { get; set; }
        public byte[] Duration { get; set; }
        public byte[] HangupReason { get; set; }
        public byte[] HangupTime { get; set; }
        public byte[] CallerNumer { get; set; }
        public byte[] CTXCommunityNo { get; set; }
        public byte[] CTXGroupNo { get; set; }

        public byte[] ISDNType { get; set; }
        public byte[] AlternateNumber { get; set; }
       
        public byte[] NewBusinesIdentity { get; set; }
        public byte[] UUS1 { get; set; }
        public byte[] UUS2 { get; set; }
        public byte[] BillingAddressNature { get; set; }
        public byte[] BillingNumber { get; set; }
        public byte[] CallerSpecialNumber { get; set; }
        public byte[] CalleeSpecialNumber { get; set; }
        public byte[] ConnectionNumberType { get; set; }

        public byte[] TakeLength { get; set; }
        public byte[] ConnectionNnumber { get; set; }
        public byte[] CIC { get; set; }
        public byte[] Other { get; set; }

        public GaoLingCDR()
        {
        }
    }
}
