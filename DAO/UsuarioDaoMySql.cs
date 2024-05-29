using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;



using BEAN;

namespace DaoMySql
{
    public class UsuarioDaoMySql
    {
        private readonly Contexto contexto;

        public UsuarioDaoMySql()
        {
            contexto = new Contexto();
        }

        #region Popula Grid
        public DataTable PopulaGrid(string pFiltro)
        {
            StringBuilder strSql = new StringBuilder();
            DataTable tabela = new DataTable();

            try
            {

                strSql.Append("select U.CODUSU AS CODIGO, U.NOMUSU AS NOME, U.FOTO AS FOTO ");

                strSql.Append(" from USUARIO U");

                strSql.Append(" WHERE 1 = 1 ");

                strSql.Append(pFiltro);
 
                MySqlDataAdapter orcDa = new MySqlDataAdapter(strSql.ToString(), ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN());
                orcDa.Fill(tabela);
                return tabela;
            }
            catch (MySqlException orcE)
            {
                orcE.GetHashCode();
                throw;
            }
        }

        public UsuarioBEAN ListarPorId(int pCodigo)
        {
            var pessoas = new List<UsuarioBEAN>();
            const string strQuery = "SELECT U.CODUSU AS CODIGO, U.NOMUSU AS NOME FROM USUARIO U WHERE U.CODUSU = @CODIGO";
            var parametros = new Dictionary<string, object>
            {
                {"CODIGO", pCodigo}
            };
            var rows = contexto.ExecutaComandoComRetorno(strQuery, parametros);
            foreach (var row in rows)
            {
                var tempPessoa = new UsuarioBEAN
                {
                    Codigo = int.Parse(!string.IsNullOrEmpty(row["CODIGO"]) ? row["CODIGO"] : "0"),
                    Nome = row["NOME"]
                };
                pessoas.Add(tempPessoa);
            }

            return pessoas.FirstOrDefault();
        }

        public UsuarioBEAN ListarPorNome(string pNome, string pSenha)
        {
            var objBean = new List<UsuarioBEAN>();
            const string strQuery = "SELECT U.CODUSU AS CODIGO, U.NOMUSU AS NOME, U.FOTO AS FOTO, U.CODEMP as MatrizFilial, NIVUSU as Nivel, CODASS AS Assessoria FROM USUARIO U WHERE U.EMAUSU = @NOMUSU AND U.SENUSU = @SENUSU";
            var parametros = new Dictionary<string, object>
            {
                {"NOMUSU", pNome},
                {"SENUSU", pSenha }
            };
                        
            var rows = contexto.ExecutaComandoComRetorno(strQuery, parametros);
            
            foreach (var row in rows)
            {
                var obj = new UsuarioBEAN
                {
                    Codigo = int.Parse(!string.IsNullOrEmpty(row["CODIGO"]) ? row["CODIGO"] : "0"),
                    Nome = row["NOME"],
                    Foto = row["FOTO"],
                    MatrizFilial = int.Parse(!string.IsNullOrEmpty(row["MatrizFilial"]) ? row["MatrizFilial"] : "0"),
                    Nivel = row["Nivel"],
                    Assessoria = int.Parse(!string.IsNullOrEmpty(row["Assessoria"]) ? row["Assessoria"] : "0"),
                };
                objBean.Add(obj);
            }

            return objBean.FirstOrDefault();
        }

        #endregion

        #region Método que retorna a quantidade de acesso do usuario, parametro codigo
        public int QtdeAcesso(int codigo)
        {
            string strSql = null;
            int vQtdeAcesso = 0;
            string vQtde = null;

            using (MySqlConnection conexaoOracleDSN = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {

                if (codigo == 0)
                {
                    return 0;
                }
                try
                {
                    conexaoOracleDSN.Open();

                    strSql = "SELECT QTDEACESSO FROM USUARIO WHERE CODUSU = " + codigo;

                    MySqlCommand cmd = new MySqlCommand(strSql, conexaoOracleDSN);
                    vQtde = cmd.ExecuteScalar().ToString();
                    vQtdeAcesso = int.Parse(vQtde);

                    return vQtdeAcesso;
                }
                catch (Exception erro0)
                {
                    throw new Exception(erro0.Message);
                }
                finally
                {
                    conexaoOracleDSN.Close();
                }
            }
        }
        #endregion

        #region Método que retorna o nivel do usuario, parametro codigo
        public string Nivel(int codigo)
        {
            string strSql = null;
            string vnivel = null;


            using (MySqlConnection conexaoOracleDSN = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {

                if (codigo == 0)
                {
                    return "N";
                }
                try
                {
                    conexaoOracleDSN.Open();

                    strSql = "SELECT NIVUSU FROM USUARIO WHERE CODUSU = " + codigo;

                    MySqlCommand cmd = new MySqlCommand(strSql, conexaoOracleDSN);
                    vnivel = cmd.ExecuteScalar().ToString();

                    return vnivel;
                }
                catch (MySqlException erro0)
                {
                    throw new Exception(erro0.Message);
                }
                finally
                {
                    conexaoOracleDSN.Close();
                }
            }
        }
        #endregion

        #region Metodo que retorna a permissao de: Visualizar, Gravar e Leitura do usuario
        public void PermissaoTela(int codigo, string tela, out bool visivel, out bool escreve, out bool leitura)
        {

            DataSet orcDsUsuario = new DataSet();
            StringBuilder strSql = new StringBuilder();

            leitura = false;
            visivel = false;
            escreve = false;


            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {

                try
                {
                    con.Open();

                    strSql.Append("SELECT P.CODPER, P.LEIPER, P.ESCPER, P.VISPER ");
                    strSql.Append("  FROM USUARIO_PERMISSAO P ");
                    strSql.Append("     , TELA T ");
                    strSql.Append(" WHERE P.CODTEL = T.CODTEL ");
                    strSql.Append("   and P.CODUSU = " + codigo + " AND T.DSCTEL = '" + tela + "'");

                    MySqlDataAdapter orcDa = new MySqlDataAdapter(strSql.ToString(), con);
                    orcDa.SelectCommand.CommandText = strSql.ToString();

                    MySqlCommandBuilder orcCb = new MySqlCommandBuilder(orcDa);
                    
                    orcDa.Fill(orcDsUsuario, "tblUsuario");

                    if (orcDsUsuario.Tables["tblUsuario"].Rows.Count > 0)
                    {
                        if (orcDsUsuario.Tables["tblUsuario"].Rows[0]["VISPER"].ToString() == "S")
                        {
                            leitura = true;
                            visivel = true;
                            escreve = true;
                        }
                        //else if (orcDsUsuario.Tables["tblUsuario"].Rows[0]["VISPER"].ToString() == "S")
                        //{
                        //    escreve = false;
                        //    visivel = false;
                        //}
                        //else if (orcDsUsuario.Tables["tblUsuario"].Rows[0]["VISPER"].ToString() == "S")
                        //{
                        //    visivel = true;
                        //    escreve = true;
                        //}

                    }
                    else
                    {
                        leitura = false;
                        escreve = false;
                        visivel = false;
                    }

                }
                catch (MySqlException erro0)
                {
                    throw new Exception(erro0.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        #endregion

        #region GetUsuarioFuncionario
        public DataTable GetUsuarioFuncionario(string pFiltro)
        {
            StringBuilder strSql = new StringBuilder();
            DataTable tabela = new DataTable();

            try
            {

                strSql.Append("select P.CODPES AS CODIGO, P.NOMPES AS NOME ");

                strSql.Append(" from USUARIO U");
                strSql.Append(" LEFT JOIN PESSOA P ON U.CODFUN = P.CODPES");

                strSql.Append(" WHERE U.STAREG <> 'D'");                

                strSql.Append(pFiltro);                

                MySqlDataAdapter orcDa = new MySqlDataAdapter(strSql.ToString(), ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN());
                orcDa.Fill(tabela);
                return tabela;
            }
            catch (MySqlException orcE)
            {
                orcE.GetHashCode();
                throw;
            }
        }
        #endregion
    }
}
