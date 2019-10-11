using FreelanceService.BLL.Interfaces.ValidationServices;
using FreelanceService.DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FreelanceService.Web.Validations
{


    public class ValidateUser : IValidaterUser 
    {
        IUnitOfWork _uow;
        ILogger _logger;

        public ValidateUser(IUnitOfWork uow, ILoggerFactory logger)
        {
            _uow = uow;
            _logger = logger.CreateLogger("UserLogger");

        }
        public async Task<bool> ValidateNewUser(string email, string phone, string firstName, string lastName)
        {

            var user = await _uow.UserRepos.FindByEmail(email);
 

            if (String.IsNullOrWhiteSpace(email))
            {
             
                _logger.LogError("Email не был введен");
                return false;
            }

            if (String.IsNullOrWhiteSpace(firstName))
            {
                _logger.LogError("Имя не было введено");
                return false;
            }

            if (String.IsNullOrWhiteSpace(lastName))
            {
                _logger.LogError("Фамилия не была введена");
                return false;
            }

            if (user.Email == email)
            {
               
                _logger.LogError("Такой Email уже существует");
                return false;

            }

            if (user.Phone == phone)
            {
               
                _logger.LogError("Такой телефон уже существует");
                return false;

            }

            if (user.Phone.Length <= 12 && user.Phone.Length >= 8)
            {
               
                _logger.LogError("Длинна символов должна быть от 8-ми до 12-ти");
                return false;

            }

            return true;
        }

        public async Task<bool> ValidateEditUser(string phone, string firstName, string lastName)
        {
            var user = await _uow.UserRepos.FindByPhone(phone);

            if (String.IsNullOrWhiteSpace(firstName))
            {
                _logger.LogError("Имя не было введено");
                return false;
            }

            if (String.IsNullOrWhiteSpace(lastName))
            {
                _logger.LogError("Фамилия не была введена");
                return false;
            }

            if (user.Phone == phone)
            {
                
                _logger.LogError("Такой телефон уже существует");
                return false;

            }

            if (user.Phone.Length <= 12 && user.Phone.Length >= 8)
            {
                
                _logger.LogError("Длинна символов должна быть от 8-ми до 12-ти");
                return false;

            }

            return true;
        }

    }
}
