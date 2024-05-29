using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;

using MODELS;


namespace DaoMySql
{
    public class UsuarioPermissaoDaoMySql
    {
        FuncionalidadeDaoMySql Funcionalidade = new FuncionalidadeDaoMySql();
        
        public UsuarioPermissaoViewModel Registro(string pUsuario, string pTela)
        {
            IEnumerable<UsuarioPermissaoViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                try
                {
                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine("SELECT A.CODUSUPER CODIGO ");

                    strSql.AppendLine("  FROM USUARIO_PERMISSAO   AS A ");

                    strSql.AppendLine(" where A.CODUSU = " + pUsuario);
                    strSql.AppendLine("   AND A.CODTEL = " + pTela);
                    
                    model = conexao.Query<UsuarioPermissaoViewModel>(strSql.ToString());
                }
                catch (Exception ex)
                {
                }
            }

            return model.FirstOrDefault();
        }
    }
}
