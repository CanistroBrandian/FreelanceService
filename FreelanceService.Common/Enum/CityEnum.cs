using System.ComponentModel.DataAnnotations;

namespace FreelanceService.Common.Enum
{

    public enum CityEnum
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
