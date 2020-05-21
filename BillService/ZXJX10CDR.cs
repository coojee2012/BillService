using System;
namespace BillService
{
    public class ZXJX10CDR
    {
        public byte[] RecordType { get; set; } //记录类型   1
        public byte[] PartRecordID { get; set; } // Part record ID(部分记录标志) 1
        public byte[] NatureAddressOfCallerNumber { get; set; } // Nature address of caller number(主叫号码地址性质) 1
        public byte[] CallerNumber { get; set; } // Caller number(主叫号码) 20
        public byte[] NatureAddressOfCalleeNumber { get; set; } // Nature address of called number(被叫号码地址性质) 1
        public byte[] CalleeNumber { get; set; } // Called number(被叫号码) 20
        public byte[] StartTime { get; set; } //Start time(开始时间) 4 秒
        public byte[] StartTicks { get; set; } //Start ticks(开始时刻) 1 毫秒
        public byte[] ServiceCategory { get; set; } // Service category(业务类别) 1 
        public byte[] EndTime { get; set; } // End time(结束时间) 4 
        public byte[] EndTicks { get; set; } // End ticks(结束时刻) 1 
        public byte[] ReleaseReason { get; set; } // Release reason(终止原因) 1
        public byte[] CallerType { get; set; } // Caller type(主叫用户类型) 1
        public byte[] CallProperties { get; set; } // Record valid ID(记录用效性标志) 1/8低     取1位 里面包含数个属性 参考文档
        public byte[] IncomingTrunkGroup { get; set; } // Incoming trunk group(入中继群)  2
        public byte[] OutgoingTrunkGroup { get; set; } // Outgoing trunk group(出中继群) 2
        public byte[] SupplementServicee { get; set; } // Supplement servicee(补充业务) 7
        public byte[] ChargePartyID { get; set; } // Charge party ID(计费方标识) 1
        public byte[] NatureAddressOfLinkNumber { get; set; } // Nature address of link number(连接号码地址性质) 1
        public byte[] LinkNumber { get; set; } // Link number(连接号码) 20 BCD,左对齐，以0xF作为结束符
        public byte[] Fee { get; set; } // Fee(费用) 4

        public byte[] BearerServices { get; set; } // Bearer services(承载业务) 1
        public byte[] TerminalServices { get; set; } // Terminal services(终端业务) 1

        public byte[] UUS1 { get; set; } // UUS1 1
        public byte[] UUS3 { get; set; } // UUS3 1
        public byte[] CallerSpecialNumber { get; set; } // Caller special number(主叫专用号码) 5
        public byte[] CalleeSpecialNumber { get; set; } // Called special number(被叫专用号码) 5
        public byte[] CentrexGroupID { get; set; }  // Centrex group ID(CTX群标识) 2  
        public byte[] NatureAddressOfBilledNumber { get; set; } // Nature address of billed number(计费号码地址性质) 1
        public byte[] BilledNumber { get; set; } // Billed number(计费号码) 11
       

        public ZXJX10CDR()
        {
        }
    }
}
