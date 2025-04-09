using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeFlow.Application.Responses
{
    public class GeneralResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
    }

    public class GeneralResponse<T> : GeneralResponse
    {
        public T Result { get; set; }
    }
}
