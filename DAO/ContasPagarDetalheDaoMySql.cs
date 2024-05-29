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
    public class ContasPagarDetalheDaoMySql
    {
        FuncionalidadeDaoMySql Funcionalidade = new FuncionalidadeDaoMySql();

        void AtribuirValores(MySqlCommand cmd, ContasPagarDetalheViewModel pObjBean)
        {

            Funcionalidade.AtribuiValorCampo(cmd, "@CODACA", pObjBean.CodigoAcao);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODCONPAG", pObjBean.CodigoContasPagar);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODCONREC", pObjBean.CodigoContasReceber);

            Funcionalidade.AtribuiValorCampo(cmd, "@USUARIO", pObjBean.CodigoUsuario);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODIGO", pObjBean.Codigo);

        }

        public string Gravar(ContasPagarDetalheViewModel pObjBean)
        {
            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                #region Incluir
                MySqlCommand cmd = null;
                StringBuilder strSql = new StringBuilder();
                string strRetorno = string.Empty;


                try
                {
                    if (pObjBean.Codigo == 0)
                    {
                        strSql.AppendLine("Insert Into CONTAS_PAGAR_DETALHE (");

                        strSql.AppendLine("    CODACA");
                        strSql.AppendLine("  , CODCONPAG ");
                        strSql.AppendLine("  , CODCONREC ");

                        strSql.AppendLine("  , STAREG");
                        strSql.AppendLine("  , DTINC");
                        strSql.AppendLine("  , DTALT");
                        strSql.AppendLine("  , CODUSUINC");
                        strSql.AppendLine("  , CODUSUALT");


                        strSql.AppendLine("  )  values (  ");

                        strSql.AppendLine("    @CODACA");
                        strSql.AppendLine("  , @CODCONPAG ");
                        strSql.AppendLine("  , @CODCONREC ");

                        strSql.AppendLine("  , 'I'");
                        strSql.AppendLine("  , NOW()");
                        strSql.AppendLine("  , NOW()");
                        strSql.AppendLine("  , @USUARIO");
                        strSql.AppendLine("  , @USUARIO");


                        strSql.AppendLine("  )         ");

                        cmd = new MySqlCommand(strSql.ToString(), con);

                        AtribuirValores(cmd, pObjBean);

                        con.Open();

                        cmd.ExecuteNonQuery();

                        return Funcionalidade.Sequence(con);
                    }
                    else
                    {
                        strSql.AppendLine("UPDATE ACAO  ");
                        strSql.AppendLine("   SET STAREG = 'U'");

                        strSql.AppendLine("    CODACA = @CODACA");
                        strSql.AppendLine("  , CODCONPAG = @CODCONPAG ");
                        strSql.AppendLine("  , CODCONREC = @CODCONREC ");

                        strSql.AppendLine("     , DTALT = NOW()");
 
                        strSql.AppendLine(" WHERE CODCONPAGDET = @CODIGO");

                        cmd = new MySqlCommand(strSql.ToString(), con);

                        AtribuirValores(cmd, pObjBean);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        return pObjBean.Codigo.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
                #endregion Incluir
            }
        }

        public List<ContasPagarDetalheViewModel> PopulaGrid(ContasPagarDetalheViewModel pModel)
        {
            IEnumerable<ContasPagarDetalheViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("SELECT CP.CODCONPAGDET AS Codigo        ");

                strSql.AppendLine("  FROM CONTAS_PAGAR_DETALHE AS CP ");
     
                strSql.AppendLine(" WHERE CP.STAREG <> 'D' ");

                if (pModel.CodigoAcao != null)
                    strSql.AppendLine(" AND CP.CODACA = " + pModel.CodigoAcao);       
                
                strSql.AppendLine("  order by CP.CODACA ");


                model = conexao.Query<ContasPagarDetalheViewModel>(strSql.ToString());
            }

            return model.ToList();
        }
    }
}
