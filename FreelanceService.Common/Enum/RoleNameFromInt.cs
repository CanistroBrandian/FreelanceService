using System;

namespace FreelanceService.Common.Enum
{
    public class RoleNameFromInt
    {
        public static string GetName(int intRole)
        {
            switch (intRole)
            {
                case 1:
                    return "Исполнитель";
                case 2:
                    return "Заказчик";
                default:
                    throw new Exception("Такй роли не существует");

            }
           
        }
    }
}
