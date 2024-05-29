using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using System.Data;
using MySql.Data.MySqlClient;

using BEAN;

namespace DaoMySql
{
    public class EmpresaDaoMySql
    {
        FuncionalidadeDaoMySql Funcionalidade = new FuncionalidadeDaoMySql();

        #region AtribuiValores - atribui valores aos parâmetros
        private void AtribuiValores(MySqlCommand cmd, EmpresaBEAN pObjBean)
        {              
            Funcionalidade.AtribuiValorCampo(cmd, "@NOMEMP", pObjBean.Nome);
            Funcionalidade.AtribuiValorCampo(cmd, "@FANEMP", pObjBean.Fantasia);
            Funcionalidade.AtribuiValorCampo(cmd, "@ALIIPI", pObjBean.AliquotaIpi);

            Funcionalidade.AtribuiValorCampo(cmd, "@ALICOFINS", pObjBean.AliquotaCofins);
            Funcionalidade.AtribuiValorCampo(cmd, "@ALIPIS", pObjBean.AliquotaPis);
            Funcionalidade.AtribuiValorCampo(cmd, "@TELEFONE", pObjBean.Telefone);
 
            Funcionalidade.AtribuiValorCampo(cmd, "@CNPJEMP", pObjBean.Cnpj);

            Funcionalidade.AtribuiValorCampo(cmd, "@IEEMP", pObjBean.Ie);
            Funcionalidade.AtribuiValorCampo(cmd, "@IMEMP", pObjBean.Im);
            Funcionalidade.AtribuiValorCampo(cmd, "@CNAE", pObjBean.Cnae);

            Funcionalidade.AtribuiValorCampo(cmd, "@ENDERECO", pObjBean.Endereco);
            Funcionalidade.AtribuiValorCampo(cmd, "@NUMERO", pObjBean.Numero);
            Funcionalidade.AtribuiValorCampo(cmd, "@COMPLEMENTO", pObjBean.Complemento);
            Funcionalidade.AtribuiValorCampo(cmd, "@BAIRRO", pObjBean.Bairro);
            Funcionalidade.AtribuiValorCampo(cmd, "@CIDADE", pObjBean.Cidade);
            Funcionalidade.AtribuiValorCampo(cmd, "@UF", pObjBean.Uf);
            Funcionalidade.AtribuiValorCampo(cmd, "@CEP", pObjBean.Cep);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODSERVRPS", pObjBean.CodigoServicoRps);
            Funcionalidade.AtribuiValorCampo(cmd, "@CAMRPS", pObjBean.CaminhoRps);

            Funcionalidade.AtribuiValorCampo(cmd, "@DSCRPSPDR", pObjBean.DescricaoRpsNf);


            Funcionalidade.AtribuiValorCampo(cmd, "@ISS", pObjBean.AliquotaIss);
            
            

            Funcionalidade.AtribuiValorCampo(cmd, "@USUARIO", pObjBean.CodigoUsuario);
            Funcionalidade.AtribuiValorCampo(cmd, "@DATA", DateTime.Now);
              
            Funcionalidade.AtribuiValorCampo(cmd, "@CODIGO", pObjBean.Codigo);
        }
        #endregion

        #region Dml
        public string Dml(EmpresaBEAN pObjBean)
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
                        strSql.Append("Insert Into  ");//
                         
                        strSql.Append("EMPRESA (");
                        
                        strSql.Append("   NOMEMP   ");
                        strSql.Append(" , FANEMP ");
                        strSql.Append(" , ALIIPI ");
                        strSql.Append(" , ALICOFINS ");
                        strSql.Append(" , ALIPIS ");
                        strSql.Append(" , TELEFONE ");

                        strSql.Append(" , CNPJEMP ");

                        strSql.Append(" , IEEMP ");
                        strSql.Append(" , IMEMP ");
                        strSql.Append(" , CNAE ");

                        strSql.Append(", ENDERECO ");
	                    strSql.Append(", NUMERO");
	                    strSql.Append(", COMPLEMENTO	"); 
	                    strSql.Append(", BAIRRO");
	                    strSql.Append(", CIDADE");
	                    strSql.Append(", UF	 ");
                        strSql.Append(", CEP");

                        strSql.Append(", CODSERVRPS");
                        strSql.Append(", CAMRPS");

                        strSql.Append(", DSCRPSPDR");

                        strSql.Append(", ISS");
                         
                        strSql.Append(", stareg, codusuinc, dtinc, codusualt, dtalt ) ");
                         
                        strSql.Append("values(");

                        strSql.Append("    @NOMEMP ");
                        strSql.Append("  , @NOMFAN ");
                        strSql.Append("  , @ALIIPI ");

                        strSql.Append(" , @ALICOFINS ");
                        strSql.Append(" , @ALIPIS ");
                        strSql.Append(" , @TELEFONE ");

                        strSql.Append("  , @CNPJEMP ");

                        strSql.Append(" , @IEEMP ");
                        strSql.Append(" , @IMEMP ");
                        strSql.Append(" , @CNAE ");

                        strSql.Append(", @ENDERECO ");
                        strSql.Append(", @NUMERO");
                        strSql.Append(", @COMPLEMENTO	");
                        strSql.Append(", @BAIRRO");
                        strSql.Append(", @CIDADE");
                        strSql.Append(", @UF	 ");
                        strSql.Append(", @CEP");

                        strSql.Append(", @CODSERVRPS");              
                        strSql.Append(", @CAMRPS");
                        strSql.Append(", @DSCRPSPDR");

                        strSql.Append(", @ISS");

                          // Não alterar - parâmetros dos campos fixos
                        strSql.Append(", 'I', @usuario, @data, @usuario, @data)");

                        strSequence = "Select @@Identity";

                        cmd = new MySqlCommand(strSql.ToString(), con);

                        AtribuiValores(cmd, pObjBean);

                        cmd.Parameters.AddWithValue("", strRetorno);

                        con.Open();

                        cmd.ExecuteNonQuery();
                        cmd.CommandText = strSequence;
                        strRetorno = cmd.ExecuteScalar().ToString();
                        return strRetorno;
                        #endregion Insert
                    }
                    else
                    {
                        #region update
                          
                        strSql.Append("UPDATE " );
                          
                        strSql.Append("EMPRESA");
                          
                        strSql.Append("   SET NOMEMP = @NOMEMP");
                        strSql.Append("     , FANEMP = @FANEMP");
                        strSql.Append("     , ALIIPI = @ALIIPI ");


                        strSql.Append(" , ALICOFINS = @ALICOFINS ");
                        strSql.Append(" , ALIPIS = @ALIPIS ");
                        strSql.Append(" , TELEFONE = @TELEFONE ");

                        strSql.Append(" , CNPJEMP = @CNPJEMP ");

                        strSql.Append(" , IEEMP = @IEEMP ");
                        strSql.Append(" , IMEMP = @IMEMP ");
                        strSql.Append(" , CNAE = @CNAE ");

                        strSql.Append(", ENDERECO = @ENDERECO ");
                        strSql.Append(", NUMERO = @NUMERO");
                        strSql.Append(", COMPLEMENTO = @COMPLEMENTO	");
                        strSql.Append(", BAIRRO = @BAIRRO");
                        strSql.Append(", CIDADE = @CIDADE");
                        strSql.Append(", UF = @UF	 ");
                        strSql.Append(", CEP = @CEP");

                        strSql.Append(", CODSERVRPS = @CODSERVRPS");

                        strSql.Append(", CAMRPS = @CAMRPS");

                        strSql.Append(", DSCRPSPDR = @DSCRPSPDR");

                        strSql.Append(", ISS = @ISS");

                        strSql.Append(", stareg = 'U', codusualt = @usuario, dtalt = @data");
                          
                        strSql.Append(" WHERE CODEMP = @codigo");

                        cmd = new MySqlCommand(strSql.ToString(), con);

                        AtribuiValores(cmd, pObjBean);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        return pObjBean.Codigo.ToString();
                        #endregion update
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        #endregion dml

        #region PopulaGrid
        public DataTable PopulaGrid(string pFiltro)
        {
            DataTable dtTabela = new DataTable();
            StringBuilder strSql = new StringBuilder();
            DmlBEAN pObjBean = new DmlBEAN();

            try
            {

                strSql.Append("select C.CODEMP as CODIGO");
                
                strSql.Append("	, C.NOMEMP as NOME");
                strSql.Append("	, C.FANEMP as FANTASIA");
                strSql.Append(" , C.CNPJEMP AS CNPJ");

                strSql.Append(", C.ENDERECO ");
                strSql.Append(", C.NUMERO");
                strSql.Append(", C.COMPLEMENTO	");
                strSql.Append(", C.BAIRRO");
                strSql.Append(", C.CIDADE");
                strSql.Append(", C.UF	 ");
                strSql.Append(", C.CEP");

                strSql.Append(", C.TELEFONE");

                strSql.Append(" , C.ALIIPI as IPI");
                strSql.Append(" , C.ALICOF as COFINS");
                strSql.Append(" , C.ALIPIS as PIS");

                strSql.Append(" , C.IEEMP as IE");
                strSql.Append(" , C.IMEMP as IM");
                strSql.Append(" , C.CNAE  AS CNAE");
                strSql.Append(" , C.CODSERVRPS AS SERVICO_RPS ");
                strSql.Append(" , C.CAMRPS   AS CAMINHO_RPS");

                strSql.Append(" , C.DSCRPSPDR   AS DSC_PADRAO_RPS_CONTRATO");
                strSql.Append(" , C.ISS         AS ISS");                           

                strSql.Append("	, C.STAREG ");

                strSql.Append("	, C.SENEMP AS SENHA ");
                strSql.Append("	, C.SMTPEMP AS SMTP ");
                strSql.Append("	, C.EMAEMP2 AS CONTA_EMAIL ");

                strSql.Append("  from EMPRESA C");
                strSql.Append(" where 1 = 1 ");

                strSql.Append(pFiltro);

                MySqlDataAdapter da = new MySqlDataAdapter(strSql.ToString(), ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN());

                da.Fill(dtTabela);

                return dtTabela;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception err0)
            {
                err0.GetHashCode();
                throw;
            }
        }
        #endregion

        public static string GetValorCampo(string pCampo, string pEmpresa)
        {
            StringBuilder strSql = new StringBuilder();
            string vRetorno = string.Empty;

            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                try
                {
                    con.Open();

                    strSql.Append("SELECT " + pCampo + " FROM EMPRESA WHERE CODEMP = " + pEmpresa);
                    
                    MySqlCommand cmd = new MySqlCommand(strSql.ToString(), con);

                    try
                    {
                        vRetorno = cmd.ExecuteScalar().ToString();

                        return vRetorno;
                    }
                    catch
                    {
                        return "";
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

        public static string GetValorCampo(string pCampo)
        {
            StringBuilder strSql = new StringBuilder();
            string vRetorno = string.Empty;

            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                try
                {
                    con.Open();

                    strSql.Append("SELECT " + pCampo + " FROM EMPRESA WHERE CODEMP = 1 ");

                    MySqlCommand cmd = new MySqlCommand(strSql.ToString(), con);

                    try
                    {
                        vRetorno = cmd.ExecuteScalar().ToString();

                        return vRetorno;
                    }
                    catch
                    {
                        return "";
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
    }
}
