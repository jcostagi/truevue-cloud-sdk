using System;

namespace TrueVUE.Cloud.SDK.API.Responses
{
    public class POSInvalidTransactionEpcItemResponse
    {
        public string CustomerTransaction { get; set; }
        public string Epc { get; set; }
        public string EventTime { get; set; }
        public string PosId { get; set; }
        public string TransactionCode { get; set; }
        public Guid? ZoneId { get; set; }
        public int? TransactionType { get; set; }
        public string Price { get; set; }
    }
}
