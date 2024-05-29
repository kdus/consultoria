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
    public class MenuDaoMySql
    {
        FuncionalidadeDaoMySql Funcionalidade = new FuncionalidadeDaoMySql();

        public List<MenuViewModel> PopulaGrid(string pUsuario)
        {
            IEnumerable<MenuViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine(" SELECT M.NOMMEN AS Menu                                       ");
                strSql.AppendLine("      , T.NOMTEL AS Tela                                       ");
                strSql.AppendLine("   FROM USUARIO_PERMISSAO UP                       ");
                strSql.AppendLine("  RIGHT JOIN TELA T ON UP.CODTEL = T.CODTEL        ");
                strSql.AppendLine("  RIGHT JOIN MENU M ON T.CODMEN = M.CODMEN         ");
                strSql.AppendLine("  WHERE UP.CODUSU = " + pUsuario);
                strSql.AppendLine("  ORDER BY M.ORDMEN, T.ORDTEL                      ");

                model = conexao.Query<MenuViewModel>(strSql.ToString());

            }

            return model.ToList();
        }

        public List<TelaViewModel> PopulaGridTelas(string pUsuario, string pNivel)
        {
            IEnumerable<TelaViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();                

                strSql.AppendLine(" SELECT M.NOMMEN AS Menu                                       ");
                strSql.AppendLine("      , T.NOMTEL AS Tela                                       ");
                strSql.AppendLine("      , T.ARQTEL AS Pagina                                     ");
                strSql.AppendLine("      , T.ICOTEL AS Icone                                    ");

                if (pNivel.Equals("A"))
                {
                    strSql.AppendLine("   FROM USUARIO_PERMISSAO UP                       ");
                    strSql.AppendLine("  RIGHT JOIN TELA T ON UP.CODTEL = T.CODTEL        ");
                }
                else
                {
                    strSql.AppendLine("   FROM TELA T                       ");
                }

                strSql.AppendLine("  RIGHT JOIN MENU M ON T.CODMEN = M.CODMEN         ");

                strSql.AppendLine("  WHERE T.INDATIVO = 'S' ");

                if (pNivel.Equals("A"))
                    strSql.AppendLine("  AND UP.CODUSU = " + pUsuario);

                strSql.AppendLine("  ORDER BY M.ORDMEN, T.ORDTEL                      ");                

                model = conexao.Query<TelaViewModel>(strSql.ToString());

            }

            return model.ToList();
        }

        public List<TelaViewModel> PopulaGridMenus(string pUsuario, string pNivel)
        {
            IEnumerable<TelaViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine(" SELECT Distinct M.NOMMEN AS Menu                                       ");
                strSql.AppendLine("      , M.ICOMEN AS Icone                                    ");

                if (pNivel.Equals("A"))
                {
                    strSql.AppendLine("   FROM USUARIO_PERMISSAO UP                       ");
                    strSql.AppendLine("  RIGHT JOIN TELA T ON UP.CODTEL = T.CODTEL        ");
                }
                else
                    strSql.AppendLine("   FROM TELA T                       ");

                strSql.AppendLine("  RIGHT JOIN MENU M ON T.CODMEN = M.CODMEN         ");

                strSql.AppendLine("  WHERE M.INDATIVO = 'S' ");

                if (pNivel.Equals("A"))
                    strSql.AppendLine("  AND UP.CODUSU = " + pUsuario);


                strSql.AppendLine("  ORDER BY M.ORDMEN       ");

                model = conexao.Query<TelaViewModel>(strSql.ToString());

            }

            return model.ToList();
        }
    }
}
