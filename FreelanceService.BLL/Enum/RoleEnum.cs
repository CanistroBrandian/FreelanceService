using System.ComponentModel.DataAnnotations;

namespace FreelanceService.BLL.Enum
{
    public enum Role
        {
        [Display(Name = "Исполнитель")]
        Executor = 1,
        [Display(Name = "Заказчик")]
        Customer = 2
        }
}
