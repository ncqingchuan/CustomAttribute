using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAttribute
{
    class Program
    {
        static void Main(string[] args)
        {

            Model[] items = new Model[] { new Model() { Name = "" }, new Model { } };

            IValidateObject<Model> check = new ValidateObject<Model>();

            for (int i = 0; i < items.Length; i++)
            {
                check.Validate(items[i]);
            }

            foreach (var item in check.ValidateDataList.Where(t => !t.IsValidated))
            {
                Console.WriteLine(string.Join(",", item.Errors.Select(t => t.ErrorMessage)));
            }

        }
    }
}
