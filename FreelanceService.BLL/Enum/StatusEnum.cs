using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Enum
{
        enum Status
        {
        [Display(Name = "В Поиске заказчика")]
        Search = 1,
        [Display(Name = "Неопубликовано")]
        Unpublished,
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
