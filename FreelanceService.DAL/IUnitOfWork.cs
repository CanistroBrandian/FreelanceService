using FreelanceService.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceService.DAL
{
   public interface IUnitOfWork:IDisposable
    {
        IUserRepository UserRepository { get; }
        void Commit();
    }
}
