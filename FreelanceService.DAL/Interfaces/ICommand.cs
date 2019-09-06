using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceService.DAL.Interfaces
{
    public interface ICommand
    {
        Task Execute(IDbTransaction transaction);
    }
}
