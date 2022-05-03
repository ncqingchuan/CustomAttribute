using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAttribute
{
    public interface IValidateObject<T>
    {
        void Validate(T obj);

        List<ValidateData<T>> ValidateDataList { get; set; }
    }


    public class ValidateData<T>
    {
        public ValidateData(T data)
        {
            Data = data;
        }
        public T Data { get; private set; }

        public List<ErrorInfo> Errors { get; private set; } = new List<ErrorInfo>();

        public bool IsValidated
        {
            get
            {
                return Errors.Count == 0;
            }
        }

    }

    public class ErrorInfo
    {
        public ErrorInfo(string errorMessage, string propertyName, string errorType)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
            ErrorType = errorType;
        }
        public string PropertyName { get; private set; }

        public string ErrorType { get; private set; }

        public string ErrorMessage { get; private set; }
    }
}
