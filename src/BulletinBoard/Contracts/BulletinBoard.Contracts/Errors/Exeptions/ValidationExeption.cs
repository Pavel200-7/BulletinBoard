using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Errors.Exeptions
{
    public class ValidationExeption : Exception
    {
        public IDictionary<string, string[]> ValidationErrors { get; }

        public ValidationExeption(IDictionary<string, string[]> errors)
            : base("Validation errors occurred.")
        {
            ValidationErrors = errors;
        }
    }
}
