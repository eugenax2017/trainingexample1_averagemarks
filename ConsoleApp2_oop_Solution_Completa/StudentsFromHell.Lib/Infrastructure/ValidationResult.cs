using Academy.Lib.Models;
using System.Collections.Generic;

namespace Academy.Lib.Infrastructure
{
    public class ValidationResult : ValidationResult<Entity>
    {

    }

    public class ValidationResult<T> where T : Entity
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public string AllErrors
        {
            get
            {
                var output = string.Empty;

                foreach (var error in Errors)
                    output += error + "\n\r";

                return output;
            }
        }

        public ValidationResult<TOut> Cast<TOut>() where TOut : Entity
        {
            var output  = new ValidationResult<TOut>()
            {
                IsSuccess = this.IsSuccess,
                Errors = this.Errors
            };

            return output;
        }
    }
}
