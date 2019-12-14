using System;
using System.Collections.Generic;

namespace WebApiGateway.Entities
{
    public partial class TblInterfaceLog
    {
        public int InterfaceLogId { get; set; }
        public int? TransactionId { get; set; }
        public string Webapi { get; set; }
        public string WebapiMethod { get; set; }
        public string InterfaceData { get; set; }
        public string Status { get; set; }
        public string Datatype { get; set; }
        public string AjaxScript { get; set; }
        public string Message { get; set; }
    }
}
