using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Common.Enum
{
    public enum RoleEnum
        {
        [Display(Name = "Исполнитель")]
        Executor = 1,
        [Display(Name = "Заказчик")]
        Customer = 2,
        [ScaffoldColumn(false)]
        Admin = 3
    }
}
