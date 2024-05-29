using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using Dapper;

using MODELS;


using BEAN;

namespace DaoMySql
{
    public class FuncionalidadeDaoMySql
    {
        #region Popula Grid
        public void PopulaGrid(DataGridView pDataGrid, string pTabela, string pFiltro)
        {
            DataTable dtTabela = new DataTable();
            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.Append("select * ");
                strSql.Append("  from ").Append(pTabela);
                strSql.Append(" WHERE 1 = 1");

                strSql.Append(pFiltro);

                MySqlDataAdapter da = new MySqlDataAdapter(strSql.ToString(), ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN());

                da.Fill(dtTabela);

                pDataGrid.DataSource = dtTabela;

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

        #region PopulaCombo
        public DataTable PopulaCombo(string pTabela, string pCampos, string pFiltro)
        {
            DataTable dtTabela = new DataTable();
            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.Append("select ").Append(pCampos);
                strSql.Append("  from ").Append(pTabela);
                strSql.Append(" WHERE 1 = 1 ");

                strSql.Append(pFiltro);


                MySqlDataAdapter da = new MySqlDataAdapter(strSql.ToString(), ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN());

                da.Fill(dtTabela);

                return dtTabela;

            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch 
            {

                throw new Exception("Erro de conexão com o banco de dados.");
            }
        }
        #endregion

        public void AtribuiValorCampo(MySqlCommand cmd, string pCampo, bool pValor)
        {
            if (!pValor)
                cmd.Parameters.Add(new MySqlParameter(pCampo, DBNull.Value));
            else
                cmd.Parameters.Add(new MySqlParameter(pCampo, pValor));
        }

        public void AtribuiValorCampo(MySqlCommand cmd, string pCampo, string pValor)
        {
            if (string.IsNullOrEmpty(pValor))
                cmd.Parameters.Add(new MySqlParameter(pCampo, DBNull.Value));
            else
                cmd.Parameters.Add(new MySqlParameter(pCampo, pValor));
        }

        public void AtribuiValorCampo(MySqlCommand cmd, string pCampo, int? pValor)
        {
            if (pValor > 0)
                cmd.Parameters.Add(new MySqlParameter(pCampo, pValor));
            else
                cmd.Parameters.Add(new MySqlParameter(pCampo, DBNull.Value));
        }

        public void AtribuiValorCampo(MySqlCommand cmd, string pCampo, double? pValor)
        {
            if (pValor > 0)
                cmd.Parameters.Add(new MySqlParameter(pCampo, pValor));
            else
                cmd.Parameters.Add(new MySqlParameter(pCampo, DBNull.Value));
        }

        public void AtribuiValorCampo(MySqlCommand cmd, string pCampo, DateTime? pValor)
        {
            if (pValor != null)
                cmd.Parameters.Add(new MySqlParameter(pCampo, pValor));
            else
                cmd.Parameters.Add(new MySqlParameter(pCampo, DBNull.Value));
        }

        public string Sequence(MySqlConnection con)
        {
            object ores = MySqlHelper.ExecuteScalar(con, "SELECT LAST_INSERT_ID();");
            int id = 0;

            if (ores != null)
            {
                ulong qkwl = (ulong)ores;
                id = (int)qkwl;
            }

            return id.ToString();
        }

        public IEnumerable<T> PopulaCombos<T>(string pTabela, string pCampoCodigo, string pCampoDescricao, string pOrderBy, string pWhere)
        {
            IEnumerable<T> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("SELECT " + pCampoCodigo +  " AS CODIGO, " + pCampoDescricao + " AS NOME ");
                strSql.AppendLine("  FROM " + pTabela);
                strSql.AppendLine(" WHERE 1 = 1 ");

                strSql.AppendLine(pWhere);

                if (pOrderBy.Length > 0)
                    strSql.AppendLine(" ORDER BY " + pOrderBy);

                model = conexao.Query<T>(strSql.ToString());

            }

            return model;
        }

    }
}
