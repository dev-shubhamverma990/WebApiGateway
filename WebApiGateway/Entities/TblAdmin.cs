using System;
using System.Collections.Generic;

namespace WebApiGateway.Entities
{
    public partial class TblAdmin
    {
        public int FunctionId { get; set; }
        public string FunctionName { get; set; }
        public string TableName { get; set; }
        public string StoredProc { get; set; }
        public string Script { get; set; }
        public string Constr { get; set; }
    }
}
