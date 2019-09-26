using FreelanceService.BLL.DTO;
using System.Collections.Generic;

namespace FreelanceService.BLL.Models
{
    public class JobViewModel
    {
        IEnumerable<JobViewDTO> Jobs { get; set; }
      //  public PageViewModel PageViewModel { get; set; }
    }
}
