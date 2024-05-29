using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;

using BEAN;

namespace DaoMySql
{
    public class LogDaoMySql
    {
        FuncionalidadeDaoMySql Funcionalidade = new FuncionalidadeDaoMySql();

        string vTabela = "LOG";


        #region AtribuiValores - atribui valores aos parâmetros
        private void AtribuiValores(MySqlCommand cmd, LogBEAN pObjBean)
        {
            Funcionalidade.AtribuiValorCampo(cmd, "@NOMLOG", pObjBean.Nome);
            Funcionalidade.AtribuiValorCampo(cmd, "@DSCLOG", pObjBean.Descricao);
            Funcionalidade.AtribuiValorCampo(cmd, "@STACKTRACE", pObjBean.StackTrace);
            Funcionalidade.AtribuiValorCampo(cmd, "@INDTIPLOG", pObjBean.Tipo);
            Funcionalidade.AtribuiValorCampo(cmd, "@DSCORIGEM", pObjBean.Origem);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODORIGEM", pObjBean.OrigemCodigo);

            Funcionalidade.AtribuiValorCampo(cmd, "@USUARIO", pObjBean.CodigoUsuario);
            Funcionalidade.AtribuiValorCampo(cmd, "@DATA", DateTime.Now);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODIGO", pObjBean.Codigo);
        }
        #endregion


        #region Dml
        public void Dml(LogBEAN pObjBean)
        {           

            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                MySqlCommand cmd = null;
                StringBuilder strSql = new StringBuilder();
                string strRetorno = string.Empty;
                string strSequence = string.Empty;

                try
                {
                    if (pObjBean.Codigo == 0)
                    {
                        #region Insert
                        strSql.Append("Insert Into  ");

                        strSql.Append(" LOG   (  ");

                        strSql.Append("       CODLOG ");
                        strSql.Append("    	, DSCLOG ");
                        strSql.Append("     , NOMLOG ");
                        strSql.Append("     , INDTIPLOG ");
                        strSql.Append("     , STACKTRACE ");
                        strSql.Append("     , DSCORIGEM");
                        strSql.Append("     , CODORIGEM");

                        strSql.Append("     , STAREG, CODUSUINC, DTINC) ");

                        strSql.Append("values ( ");

                        strSql.Append("    	  NEXTVAL('LOG') ");
                        strSql.Append("    	, @DSCLOG ");
                        strSql.Append("     , @NOMLOG ");
                        strSql.Append("     , @INDTIPLOG ");
                        strSql.Append("     , @STACKTRACE ");
                        strSql.Append("     , @DSCORIGEM");
                        strSql.Append("     , @CODORIGEM");

                        strSql.Append("     , 'I', @USUARIO, @DATA )");


                        cmd = new MySqlCommand(strSql.ToString(), con);

                        AtribuiValores(cmd, pObjBean);

                        con.Open();

                        cmd.ExecuteNonQuery();
                        
                        #endregion Insert
                    }                   
                }
                catch (MySqlException ex)
                {
                    LogArquivo(ex.Message.ToString());                    
                    throw new Exception(ex.Message);                   
                }
                finally
                {
                    con.Close();
                }
            }
        }
        #endregion dml

        public void LogArquivo(string vtexto)
        {
            string vNomeAquivo = "c:\\SisGebin\\Log.txt";           

            if (!File.Exists(vNomeAquivo))
            {
                FileStream vArquivo = File.Open(vNomeAquivo, FileMode.OpenOrCreate);
                vArquivo.Close();
            }

            StreamWriter vEscreve = new StreamWriter(vNomeAquivo, true, Encoding.UTF8);
            vEscreve.Write("[" + DateTime.Now.ToString() +"] "  +  vtexto + "\r\n");
            
            vEscreve.Close();          

        }
    }
}
