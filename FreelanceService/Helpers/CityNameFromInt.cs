using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceService.Web.Helpers
{
    public class CityNameFromInt
    {
        public static string GetName(int city)
        {
            switch (city)
            {
                case 1:
                    return "Минск";
                case 2:
                    return "Брест";
                case 3:
                    return "Могилев";
                case 4:
                    return "Гродно";
                case 5:
                    return "Витебск";
                case 6:
                    return "Гомель";
                default:
                    throw new Exception("Такого города не существует");

            }
        }
    }
}
