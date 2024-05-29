using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;


using MODELS;
using BEAN;

namespace DaoMySql
{

	public class PedidoVendaDaoMySql
	{
		FuncionalidadeDaoMySql Funcionalidade = new FuncionalidadeDaoMySql();

		#region Atribuir Valores
		void AtribuirValores(MySqlCommand cmd, PedidoVendaViewModel pObjBean)
		{
			//Funcionalidade.AtribuiValorCampo(cmd, "@CODSISLEG", pObjBean.CodigoLegado);

			Funcionalidade.AtribuiValorCampo(cmd, "@Id", pObjBean.Id);
			Funcionalidade.AtribuiValorCampo(cmd, "@Data", string.Format("{0:yyyy-MM-dd}", pObjBean.Data));
			Funcionalidade.AtribuiValorCampo(cmd, "@caixa_n", pObjBean.caixa_n);
			Funcionalidade.AtribuiValorCampo(cmd, "@cmv_peca", pObjBean.cmv_peca);
			Funcionalidade.AtribuiValorCampo(cmd, "@cmv_pneu", pObjBean.cmv_pneu);
			Funcionalidade.AtribuiValorCampo(cmd, "@servico_terceiro", pObjBean.servico_terceiro);
			Funcionalidade.AtribuiValorCampo(cmd, "@faturamento_peca", pObjBean.faturamento_peca);
			Funcionalidade.AtribuiValorCampo(cmd, "@faturamento_pneu", pObjBean.faturamento_pneu);
			Funcionalidade.AtribuiValorCampo(cmd, "@faturamento_maoobra", pObjBean.faturamento_maoobra);
			Funcionalidade.AtribuiValorCampo(cmd, "@qtd_veiculo", pObjBean.qtd_veiculo);
			Funcionalidade.AtribuiValorCampo(cmd, "@Nomecliente", pObjBean.Nomecliente);
			Funcionalidade.AtribuiValorCampo(cmd, "@CODUSU", pObjBean.CODUSU);
			Funcionalidade.AtribuiValorCampo(cmd, "@CODPES", pObjBean.CODPES);
			//Funcionalidade.AtribuiValorCampo(cmd, "@BAIPES", pObjBean.Bairro);
			//Funcionalidade.AtribuiValorCampo(cmd, "@CODCID", pObjBean.Cidade);

		}
		#endregion

		#region Dml
		public string Gravar(PedidoVendaViewModel pObjBean)
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
						strSql.Append("Insert Into VENDAS (");

						strSql.Append("    id");
						strSql.Append("  , Data ");
						strSql.Append("  , caixa_n ");
						strSql.Append("  , cmv_pneu "); 
						strSql.Append("  , cmv_peca ");
						strSql.Append("  , servico_terceiro ");

						strSql.Append("  , faturamento_peca ");
						strSql.Append("  , faturamento_pneu ");
						strSql.Append("  , faturamento_maoobra ");
						strSql.Append("  , qtd_veiculo ");

						strSql.Append("  , nomecliente  ");
						strSql.Append("  , CODUSU ");
						strSql.Append("  , CODPES ");

						//strSql.Append(")");

						strSql.Append(")  values (");

						strSql.Append("    @id");
						strSql.Append("  , @Data ");
						strSql.Append("  , @caixa_n ");
						strSql.Append("  , @cmv_pneu ");
						strSql.Append("  , @cmv_peca "); 
						strSql.Append("  , @servico_terceiro ");

						strSql.Append("  , @faturamento_peca ");
						strSql.Append("  , @faturamento_pneu ");
						strSql.Append("  , @faturamento_maoobra ");
						strSql.Append("  , @qtd_veiculo ");

						strSql.Append("  , @nomecliente  ");
						strSql.Append("  , @CODUSU ");
						strSql.Append("  , @CODPES ");

						strSql.Append(")");

						cmd = new MySqlCommand(strSql.ToString(), con);

						AtribuirValores(cmd, pObjBean);

						con.Open();

						cmd.ExecuteNonQuery();

						return Funcionalidade.Sequence(con);
					}
					else
					{
						strSql.Append("UPDATE VENDAS  ");
						 //strSql.Append("   SET Id = @Id");

						strSql.Append("      SET Data  = @Data ");
						strSql.Append("     , caixa_n  = @caixa_n");
						strSql.Append("     , cmv_pneu  = @cmv_pneu ");
						strSql.Append("     , caixa_n  = @caixa_n");
						strSql.Append("     , cmv_peca  = @cmv_peca ");
						strSql.Append("     , servico_terceiro = @servico_terceiro ");

						strSql.Append("     , faturamento_peca = @faturamento_peca");

						strSql.Append("     , faturamento_pneu = @faturamento_pneu");

						strSql.Append("     , faturamento_maoobra = @faturamento_maoobra");

						strSql.Append("     , qtd_veiculo = @qtd_veiculo");


						strSql.Append("     , nomecliente = @nomecliente");
						strSql.Append("  , CODUSU = @CODUSU ");
						strSql.Append("  , CODPES = @CODPES ");


						strSql.Append(" WHERE Id = @Id");

						cmd = new MySqlCommand(strSql.ToString(), con);

						AtribuirValores(cmd, pObjBean);

						con.Open();
						cmd.ExecuteNonQuery();

						return pObjBean.Id.ToString();
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

		//public void Excluir(PedidoVendaViewModel pObjBean)
		//{
		//	using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
		//	{
		//		#region Incluir
		//		MySqlCommand cmd = null;
		//		StringBuilder strSql = new StringBuilder();
		//		string strRetorno = string.Empty;


		//		try
		//		{
		//			strSql.Append("UPDATE PESSOA  ");
		//			strSql.Append("   SET STAREG = 'D'");

		//			strSql.Append(" WHERE CODPES = @CODIGO");

		//			cmd = new MySqlCommand(strSql.ToString(), con);

		//			cmd.Parameters.Add(new MySqlParameter("@CODIGO", pObjBean.Id));

		//			con.Open();

		//			cmd.ExecuteNonQuery();

		//		}
		//		catch (Exception ex)
		//		{
		//			throw;
		//		}
		//		finally
		//		{
		//			con.Close();
		//		}
		//		#endregion Incluir
		//	}
		//}
		#endregion

		#region Popula DataGrid
		public DataTable PopulaGrid(string pFiltro)
		{
			DataTable dtTabela = new DataTable();
			StringBuilder strSql = new StringBuilder();

			try
			{

				strSql.Append("select ");

				strSql.Append("    p.id			AS ID");
				strSql.Append("  , p.Data		AS DATA ");
				strSql.Append("  , p.caixa_n	AS CAIXA NUMERO");
				strSql.Append("  , p.cmv_pneu	AS CMV PNEU");
				strSql.Append("  , p.servico_terceiro AS SERVICO TERCEIRO");

				strSql.Append("  , p.faturamento_peca AS FATURAMENTO PECA");
				strSql.Append("  , p.faturamento_pneu AS FATURAMENTO PNEU");
				strSql.Append("  , p.faturamento_maoobra AS FATURAMENTO MAO DE OBRA");
				strSql.Append("  , p.qtd_veiculo	AS QTD VEICULO");

				strSql.Append("  , p.nomecliente  AS NOME DO CLIENTE");
			
				strSql.Append("  FROM VENDAS AS P  ");
				//strSql.Append(" WHERE P.STAREG <> 'D' ");

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

		//#region Pesquisa Cliente
		//public DataTable PesquisaCliente(string pFiltro, string pEmpresa)
		//{
		//	MySqlCommand cmd = null;
		//	DataTable dtTabela = new DataTable();
		//	MySqlConnection conn = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN();

		//	try
		//	{
		//		String strSql = "SELECT P.CODPES, P.NOMPES FROM PESSOA P WHERE P.CODEMP = @EMPRESA AND P.NOMPES LIKE CONCAT('%',@NOMEMP, '%');";
		//		conn.Open();
		//		cmd = new MySqlCommand(strSql.ToString(), conn);

		//		Funcionalidade.AtribuiValorCampo(cmd, "@EMPRESA", pEmpresa);
		//		Funcionalidade.AtribuiValorCampo(cmd, "@NOMEMP", pFiltro.ToUpper());

		//		MySqlDataAdapter da = new MySqlDataAdapter(cmd);

		//		da.Fill(dtTabela);

		//		return dtTabela;
		//	}
		//	catch (MySqlException ex)
		//	{
		//		conn.Close();
		//		throw new Exception(ex.Message);
		//	}
		//	catch (Exception err0)
		//	{
		//		conn.Close();
		//		err0.GetHashCode();
		//		throw;
		//	}
		//	finally
		//	{
		//		conn.Close();
		//	}
		//}
		//#endregion

		public DataTable Registro(string pCodigo)
		{
			MySqlCommand cmd = null;
			DataTable dtTabela = new DataTable();
			MySqlConnection conn = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN();

			try
			{
				String strSql = "SELECT P.* FROM VENDAS P WHERE P.Id = @CODIGO";
				conn.Open();
				cmd = new MySqlCommand(strSql.ToString(), conn);

				Funcionalidade.AtribuiValorCampo(cmd, "@CODIGO", pCodigo);


				MySqlDataAdapter da = new MySqlDataAdapter(cmd);

				da.Fill(dtTabela);

				return dtTabela;
			}
			catch (MySqlException ex)
			{
				conn.Close();
				throw new Exception(ex.Message);
			}
			catch (Exception err0)
			{
				conn.Close();
				err0.GetHashCode();
				throw;
			}
			finally
			{
				conn.Close();
			}
		}

		//public void Excluir(string pCodigo)
		//{
		//	using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
		//	{
		//		try
		//		{
		//			con.Open();
		//			StringBuilder sql = new StringBuilder();

		//			sql.Append("UPDATE PESSOA SET STAREG = 'D' ");
		//			sql.Append(" WHERE CODPES = " + pCodigo);

		//			MySqlCommand cmd = new MySqlCommand(sql.ToString(), con);

		//			cmd.ExecuteNonQuery();


		//		}
		//		catch (Exception ex)
		//		{
		//			ex.GetHashCode();
		//			throw;
		//		}
		//		finally
		//		{
		//			con.Close();
		//		}
		//	}

		//}

		//#region Método que retorna o nivel do usuario, parametro codigo
		//public string GetNome(string pCodigo)
		//{
		//	string strSql = null;


		//	using (MySqlConnection conexaoOracleDSN = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
		//	{
		//		try
		//		{
		//			conexaoOracleDSN.Open();

		//			strSql = "SELECT NOMPES FROM PESSOA WHERE CODPES = @CODIGO";

		//			MySqlCommand cmd = new MySqlCommand(strSql, conexaoOracleDSN);

		//			cmd.Parameters.Add(new MySqlParameter("@CODIGO", pCodigo));

		//			return cmd.ExecuteScalar().ToString();


		//		}
		//		catch (MySqlException erro0)
		//		{
		//			throw new Exception(erro0.Message);
		//		}
		//		finally
		//		{
		//			conexaoOracleDSN.Close();
		//		}
		//	}
		//}
		//#endregion

		//public List<PedidoVendaViewModel> PopulaCombo()
		//{
		//	IEnumerable<PedidoVendaViewModel> model = null;

		//	using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
		//	{

		//		StringBuilder strSql = new StringBuilder();

		//		strSql.AppendLine("SELECT M.CODPES AS CODIGO, M.Data AS Data ");
		//		strSql.AppendLine("  FROM VENDAS M");
		//		strSql.AppendLine(" WHERE M.CODPES <> 0 ");

		//		strSql.AppendLine(" ORDER BY M.Data");

		//		model = conexao.Query<PedidoVendaViewModel>(strSql.ToString());

		//	}
		//	return model.ToList();

		//	}

		public List<PedidoVendaViewModel> PopulaGrid(PedidoVendaViewModel pedidoVenda)
		{
			IEnumerable<PedidoVendaViewModel> model = null;

			using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
			{

				StringBuilder strSql = new StringBuilder();

				strSql.AppendLine("SELECT *  ");

				strSql.AppendLine("  FROM VENDAS ");
				//strSql.AppendLine(" WHERE M.STAREG <> 'D' ");
				strSql.AppendLine(" WHERE CODPES = " +pedidoVenda.BuscaCliente);

				//if (pedidoVenda.BuscaData != null)
				//strSql.AppendLine(" AND p.Data LIKE '%" + pedidoVenda.BuscaData + "%'");


				//strSql.AppendLine(" ORDER BY p.Data");

				model = conexao.Query<PedidoVendaViewModel>(strSql.ToString());

			}

			return model.ToList();
		}

	}
}
