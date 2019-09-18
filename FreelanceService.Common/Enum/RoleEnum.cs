using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Common.Enum
{
    public enum Role
        {
        [Display(Name = "Исполнитель")]
        Executor = 1,
        [Display(Name = "Заказчик")]
        Customer = 2,
        [Display(Name = "Админ")]
        Admin = 3
    }
}
