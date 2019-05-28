using System.Collections.Generic;

namespace Bookify.Auth.Infrastructure.Models
{
    public class ResponseErrorModel
    {
        public List<string> ErrorMessages { get; set; }

        public ResponseErrorModel()
        {
            ErrorMessages = new List<string>();
        }

        public ResponseErrorModel(params string[] errorMessages)
        {
            ErrorMessages = new List<string>();

            if (errorMessages != null)
            {
                ErrorMessages.AddRange(errorMessages);
            }
        }
    }
}
