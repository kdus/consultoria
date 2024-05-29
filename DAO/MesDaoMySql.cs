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
    public class MesDaoMySql
    {
        public IEnumerable<MesViewModel> GetMeses()
        {
            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {
                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("SELECT M.CODMES AS CODIGO, M.DSCMES AS NOME ");
                strSql.AppendLine("  FROM MES M");
                strSql.AppendLine(" WHERE 1 = 1 ");

                return conexao.Query<MesViewModel>(strSql.ToString());
            }
        }

        public List<MesViewModel> RetornarMeses()
        {
            IEnumerable<MesViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("SELECT M.CODMES AS CODIGO, M.DSCMES AS NOME ");
                strSql.AppendLine("  FROM MES M");
                strSql.AppendLine(" WHERE 1 = 1 ");

                model = conexao.Query<MesViewModel>(strSql.ToString());

            }

            return model.ToList();
        }
    }
}
