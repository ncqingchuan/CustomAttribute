using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace CustomAttribute
{
    public class ValidateObject<T> : IValidateObject<T>, IDisposable
    {
        public List<ValidateData<T>> ValidateDataList { get; set; } = new List<ValidateData<T>>();

        private class PropertyAttrubute
        {
            public PropertyInfo PropertyInfo { get; set; }
            public object[] Attributes { get; set; }
        }

        private List<PropertyAttrubute> PropertyAttrubuteList;

        public ValidateObject() => PropertyAttrubuteList = typeof(T).GetProperties().Select(t => new PropertyAttrubute
        {
            PropertyInfo = t,
            Attributes = t.GetCustomAttributes(true)
        }).ToList();
        public void Validate(T obj)
        {
            ValidateData<T> validatedata = new ValidateData<T>(obj);

            ValidateDataList.Add(validatedata);

            foreach (var pr in PropertyAttrubuteList)
            {
                var value = pr.PropertyInfo.GetValue(obj);

                foreach (var attr in pr.Attributes)
                {
                    if (attr is CustomAttribute customAttribute) customAttribute.CheckObject(validatedata, pr.PropertyInfo, value);
                }

            }
        }
        public void Dispose()
        {
            if (PropertyAttrubuteList != null)
            {
                PropertyAttrubuteList.Clear();
                PropertyAttrubuteList = null;
            }
        }
    }
}
