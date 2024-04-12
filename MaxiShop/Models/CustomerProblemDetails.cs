using Microsoft.AspNetCore.Mvc;

namespace MaxiShop.Models
{
    public class CustomerProblemDetails: ProblemDetails
    {
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}
