﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueVUE.Cloud.SDK.API.Responses
{
    public class POSInvalidTransactionEpcResponse
    {
        public string Message { get; set; }
        public POSInvalidTransactionEpcItemResponse Postransaction { get; set; }
    }
}
