using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace CustomAttribute
{

    public class StringLengthAttribute : CustomAttribute
    {

        public int MinLength { get; private set; }
        public int MaxLength { get; private set; }
        public StringLengthAttribute(int minLength, int maxLength, string errorMessage)
            : base(errorMessage)
        {
            MinLength = minLength;

            MaxLength = maxLength;

            ErrorMessage = errorMessage;
        }

        public override string ToString()
        {
            return nameof(StringLengthAttribute);
        }

        public override void CheckObject<T>(ValidateData<T> validateData, PropertyInfo property, params object[] values)
        {
            
            if (validateData == null) throw new ArgumentNullException("Argument validateData is null");
            if (property == null) throw new ArgumentNullException("Argument property is null");
            if (values.Length != 1) throw new ArgumentNullException("Argument values Length gt 0");
            if (property.PropertyType == typeof(string) && values[0] != null)
            {
                if (values[0].ToString().Length < MinLength || values[0].ToString().Length > MaxLength)
                {
                    validateData.Errors.Add(new ErrorInfo(string.Format(ErrorMessage, property.Name, MinLength, MaxLength), property.Name, ToString()));
                }
            }
        }
    }

    public class RequiredAttribute : CustomAttribute
    {
        public RequiredAttribute(string errorMessasge)
            : base(errorMessasge)
        {

        }
        public override void CheckObject<T>(ValidateData<T> validateData, PropertyInfo property, params object[] values)
        {
            if (validateData == null) throw new ArgumentNullException("Argument validateData is null");
            if (property == null) throw new ArgumentNullException("Argument property is null");
            if (values.Length != 1) throw new ArgumentNullException("Argument Values Length gt 0");
            if (values[0] == null) validateData.Errors.Add(new ErrorInfo(string.Format(ErrorMessage, property.Name), property.Name, ToString()));
        }

        public override string ToString()
        {
            return nameof(RequiredAttribute);
        }
    }
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class CustomAttribute : Attribute
    {
        public string ErrorMessage { get; protected set; }

        public CustomAttribute(string errorMessage = null)
        {
            ErrorMessage = errorMessage;
        }

        public new abstract string ToString();

        public abstract void CheckObject<T>(ValidateData<T> validateData, PropertyInfo property, params object[] values);
    }

    public class ComapreAttribute : CustomAttribute
    {

        public ComapreAttribute(string errorMessage)
        : base(errorMessage)
        {

        }
        public override void CheckObject<T>(ValidateData<T> validateData, PropertyInfo property, params object[] values)
        {
            var x = (IComparable<T>)values[0];
            var y = (IComparable<T>)values[1];


        }

        public override string ToString()
        {
            return nameof(ComapreAttribute);
        }
    }

}
