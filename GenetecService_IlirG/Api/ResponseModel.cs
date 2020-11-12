using System;
using System.Collections.Generic;
using System.Text;

namespace GenetecService_IlirG
{
    public class ResponseModel<T> where T:class
    {
        public T Item { get; set; }
        public bool HasError { get; set; }
        public string Message { get; set; }
    }
}
