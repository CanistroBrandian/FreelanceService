using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Common.Enum
{
   public enum ResponseStatusEnum
        {
        [Display(Name = "Уже откликнулся")]
        AlreadyResponded =1,
        [Display(Name = "Сделка")]
        Deal,
        [Display(Name = "Отмена отклика")]
        Cansel
    }
}
