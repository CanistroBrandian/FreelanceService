using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace FreelanceService.DAL
{
    public sealed class DalSession : IDisposable
    {
        public DalSession()
        {
            string con = "Data Source=DESKTOP-5A20RVG;Initial Catalog=FreelanceServiceDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            _connection = new SqlConnection(con);
            _connection.Open();
            _unitOfWork = new UnitOfWork(_connection);
        }

        IDbConnection _connection = null;
        UnitOfWork _unitOfWork = null;

        public UnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
            _connection.Dispose();
        }
    }
}
