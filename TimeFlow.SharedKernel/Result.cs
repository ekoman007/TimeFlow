using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeFlow.SharedKernel
{
    public class Result
    {
        public bool IsSuccess { get;  set; }
        public string ErrorMessage { get;  set; }
        public object Data { get;  set; }

        public static Result Success(object data)
        {
            return new Result { IsSuccess = true, Data = data };
        }

        public static Result Failure(string errorMessage)
        {
            return new Result { IsSuccess = false, ErrorMessage = errorMessage };
        }
    }
}
