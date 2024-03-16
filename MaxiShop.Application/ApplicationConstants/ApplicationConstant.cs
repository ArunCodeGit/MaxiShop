using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.ApplicationConstants
{
    public class ApplicationConstant
    {

    }

    public class CommonMessage
    {
        public const string CreateOperationSuccess = "Record Created Successfully.";
        public const string UpdateOperationSuccess = "Record Updated Successfully.";
        public const string DeleteOperationSuccess = "Record Deleted Successfully.";

        public const string CreateOperationFailed = "Createion operation Failed!";
        public const string UpdateOperationFailed = "Updation operation Failed!";
        public const string DeleteOperationFailed = "Delete Operation Failed!";

        public const string RecordNotFound = "Record not found!";
        public const string SystemError = "Something went wrong!";
    }
}
