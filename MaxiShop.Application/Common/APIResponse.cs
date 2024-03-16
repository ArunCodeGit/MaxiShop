using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Common
{
    public class APIResponse
    {
        public HttpStatusCode statusCode { get; set; }
        public bool IsSuccess { get; set; }
        public object Result {  get; set; }
        public string DisplayMessage { get; set; }

        public List<APIError> Errors { get; set; } = new();

        public List<APIWarning> Warnings { get; set; } = new();

        public void AddErrors(string errorMessage)
        {
            APIError error = new APIError(errorMessage);
            Errors.Add(error);
        }

        public void AddWarnings(string errorMessage)
        {
            APIWarning warning = new APIWarning(errorMessage);
            Warnings.Add(warning);
        }
    }
}
