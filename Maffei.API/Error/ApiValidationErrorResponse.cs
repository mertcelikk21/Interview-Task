using System.Collections.Generic;

namespace Maffei.API.Error
{
    public class ApiValidationErrorResponse:ApiResponse
    {
        public ApiValidationErrorResponse() : base(400)
        {

        }
        public string Errors { get; set; }
    }
}
