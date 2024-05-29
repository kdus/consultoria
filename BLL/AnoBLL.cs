using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoMySql;
using MODELS;

namespace BLL
{
    public class AnoBLL
    {
       

        public static List<AnoViewModel> PopulaCombo()
        {
            AnoDaoMySql objDao = new AnoDaoMySql();
            return objDao.PopulaCombo();
        }
    }
}
