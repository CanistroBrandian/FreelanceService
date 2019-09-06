using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceService.DAL.Enum
{
   public class StatusEnum
    {
        enum Status
        {
            Поиск = 1,
            Неопубликованно,
            Отклик,
            Сделка,
            Выполнено,
            Просрочено,
            Отказался
        }
    }
}
