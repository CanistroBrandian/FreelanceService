using System.Linq;
using System.Reflection;

namespace FreelanceService.Common.Helpers
{
    public static class EnumHelper
    {
       public static string GetDisplayName<T>(T value) where T: struct
        {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).FirstOrDefault();
        }
    }
}
