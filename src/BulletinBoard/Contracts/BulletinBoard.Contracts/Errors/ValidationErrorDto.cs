using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace BulletinBoard.Contracts.Errors
{
    public class ValidationErrorDto : Exception
    {
        public int StatusCode { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
        public string TraceId { get; set; }
    }
}
