using System;

namespace SCADA.Common
{
    public class OperationResult
    {
        public string ErrMsg { get; set; }
        public Exception Exception { get; set; }
        public bool IsSuccess => string.IsNullOrWhiteSpace(ErrMsg) && Exception == null;
    }

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; set; }
    }
}