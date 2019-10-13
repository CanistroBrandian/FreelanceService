using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Common.Enum
{
   public enum JobStatusEnum
    {
        //TODO: Исправить int на enum в соответствующих местах
        [Display(Name = "Опубликовано")]
        Published = 1,
        [Display(Name = "Неопубликовано")]
        Unpublished,
        [Display(Name = "Отклик")]
        Response,
        [Display(Name = "Сделка")]
        Deal,
        [Display(Name = "В ожидании подтверждения")]
        WaitingForConfirmation,
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
