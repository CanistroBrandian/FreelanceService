using System.ComponentModel.DataAnnotations;

namespace FreelanceService.BLL.Enum
{

    public enum City
    {
        [Display(Name = "Минск")]
        Minsk = 1,
        [Display(Name = "Брест")]
        Brest,
        [Display(Name = "Могилев")]
        Mogilev,
        [Display(Name = "Гродно")]
        Grodno,
        [Display(Name = "Витебск")]
        Vitebsk,
        [Display(Name = "Гомель")]
        Gomel
    }
}
