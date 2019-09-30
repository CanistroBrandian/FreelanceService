using System;

namespace FreelanceService.Web.Helpers
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
                case 3:
                    return "Админ";
                default:
                    throw new Exception("Такй роли не существует");

            }
           
        }
    }
}
