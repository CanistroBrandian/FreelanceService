using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Common.Enum
{
   public enum JobStatusEnum
    {
        [Display(Name = "Неопубликовано")]
        Unpublished=1,
        [Display(Name = "Опубликовано")]
        Published,
        [Display(Name = "Отклик")]
        Response,
        [Display(Name = "Сделка")]
        Deal,
        [Display(Name = "Выполнено")]
        Done,
        [Display(Name = "Просрочено")]
        Expired,
        [Display(Name = "Отказался Заказчик")]
        RefusedCustomer,
        [Display(Name = "Отказался Исполнитель")]
        RefusedExecuter
    }
}
