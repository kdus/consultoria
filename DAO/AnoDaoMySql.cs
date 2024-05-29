using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

using MODELS;


namespace DaoMySql
{
    public class AnoDaoMySql
    {      

        public List<AnoViewModel> PopulaCombo()
        {
            IEnumerable<AnoViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("SELECT M.ANO AS CODIGO, M.ANO AS NOME ");
                strSql.AppendLine("  FROM ANO M");
                strSql.AppendLine(" WHERE 1 = 1 ");

                strSql.AppendLine(" ORDER BY M.ANO DESC");

                model = conexao.Query<AnoViewModel>(strSql.ToString());

            }

            return model.ToList();
        }
    }
}
