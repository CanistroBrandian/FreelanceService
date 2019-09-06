using FreelanceService.BLL.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FreelanceService.BLL.Helpers
{



    public class GetConfigString:IGetConfigString
    {
        public GetConfigString(IConfiguration conf)
        {
            _config = conf;
        }
        private readonly IConfiguration _config;

        public string GetEmailOrPass(string str)
        {
            var res = _config.GetSection("EmailService:" + str).Value;         
            return res;
        }

    }
}
