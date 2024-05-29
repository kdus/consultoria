using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoMySql;
using MODELS;

namespace BLL
{
    public class MesBLL
    {
        public IEnumerable<MesViewModel> GetMeses()
        {
            MesDaoMySql objDao = new MesDaoMySql();
            return objDao.GetMeses();
        }

        public static List<MesViewModel> RetornarMeses()
        {
            MesDaoMySql objDao = new MesDaoMySql();
            return objDao.RetornarMeses();
        }
    }
}
