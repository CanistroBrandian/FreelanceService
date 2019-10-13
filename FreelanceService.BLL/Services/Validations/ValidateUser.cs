﻿using FreelanceService.BLL.Interfaces.ValidationServices;
using FreelanceService.DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FreelanceService.BLL.Services.Validations
{


    public class ValidateUser : IValidaterUser //проборосить логгер
    {
        IUnitOfWork _uow;
        ILogger _logger;

        public ValidateUser(IUnitOfWork uow, ILoggerFactory logger)
        {
            _uow = uow;
            _logger = logger.CreateLogger("UserLogger");

        }
        public async Task<bool> ValidateNewUser(string email, string phone)
        {

            var user = await _uow.UserRepos.FindByEmail(email);

            if (user.Email == email)
            {
                //добавить строчку логера об ошибке чо такой пльзователь есть
                _logger.LogError("Такой Email уже существует");
                return false;

            }

            if (user.Phone == phone)
            {
                //добавить строчку логера об ошибке чо такой пльзователь есть
                _logger.LogError("Такой телефон уже существует");
                return false;

            }
            return true;
        }

        public async Task<bool> ValidateEditUser(string phone)
        {
            var user = await _uow.UserRepos.FindByPhone(phone);


            if (user.Phone == phone)
            {
                //добавить строчку логера об ошибке чо такой пльзователь есть
                _logger.LogError("Такой телефон уже существует");
                return false;

            }
            return true;
        }

    }
}
