using System;
using System.Collections.Generic;

namespace WebApiGateway.Entities
{
    public partial class TblExceptionLog
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public DateTime LogDateTime { get; set; }
        public int Id { get; set; }
    }
}
