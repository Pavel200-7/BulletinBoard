using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Errors.ErrorsList
{
    public class ErrorsDictionaryValidating
    {
        public Dictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();

        public ErrorsDictionaryValidating()
        {
            Errors = new Dictionary<string, List<string>>();
        }

        public bool IsEmpty() => Errors.Count == 0;
    }
}
