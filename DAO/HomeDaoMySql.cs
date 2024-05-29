using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;

using BEAN;
using MODELS;

namespace DaoMySql
{
    public class HomeDaoMySql
    {
        public HomeViewModel Contas(string pEmpresa, string pAno, string pMes)
        {
            IEnumerable<HomeViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("  SELECT SUM(T.ValorCpPagar)     AS  TotalCpPagar    ");
                strSql.AppendLine("       , SUM(T.ValorCpPago)      AS  TotalCpPago     ");
                strSql.AppendLine("       , SUM(T.ValorCrReceber)   AS  TotalCrReceber  ");
                strSql.AppendLine("       , SUM(T.ValorCrRecebido)  AS  TotalCrRecebido ");

                strSql.AppendLine("  FROM ( ");

                strSql.AppendLine("         SELECT SUM(CP.VLRCONPAG)  AS ValorCpPagar    ");
                strSql.AppendLine("              , 0                  AS ValorCpPago     ");
                strSql.AppendLine("              , 0                  AS ValorCrReceber  ");
                strSql.AppendLine("              , 0                  AS ValorCrRecebido ");
                strSql.AppendLine("           FROM CONTAS_PAGAR AS CP                             ");
                strSql.AppendLine("          where CP.STAREG <> 'D'                               ");
                strSql.AppendLine("            AND (CP.INDCONPAG = 'N' OR CP.INDCONPAG IS NULL)    ");
                strSql.AppendLine("            AND CP.CODEMP = " + pEmpresa);
                strSql.AppendLine("            AND DATE_FORMAT(CP.VENCONPAG, '%Y') = " + pAno);
                strSql.AppendLine("            AND DATE_FORMAT(CP.VENCONPAG, '%m') = " + pMes);

                strSql.AppendLine("      UNION ALL ");

                strSql.AppendLine("         SELECT 0                  AS ValorCpPagar    ");
                strSql.AppendLine("              , SUM(CP.VLRCONPAGA) AS ValorCpPago     ");
                strSql.AppendLine("              , 0                  AS ValorCrReceber  ");
                strSql.AppendLine("              , 0                  AS ValorCrRecebido ");
                strSql.AppendLine("           FROM CONTAS_PAGAR AS CP                          ");
                strSql.AppendLine("          where CP.STAREG <> 'D'                            ");
                strSql.AppendLine("            AND CP.INDCONPAG = 'S'                          ");
                strSql.AppendLine("            AND CP.CODEMP = " + pEmpresa);
                strSql.AppendLine("            AND DATE_FORMAT(CP.VENCONPAG, '%Y') = " + pAno);
                strSql.AppendLine("            AND DATE_FORMAT(CP.VENCONPAG, '%m') = " + pMes);

                strSql.AppendLine("      UNION ALL ");

                strSql.AppendLine("         SELECT 0                  AS ValorCpPagar    ");
                strSql.AppendLine("              , 0                  AS ValorCpPago     ");
                strSql.AppendLine("              , SUM(CP.VLRCONREC)  AS ValorCrReceber  ");
                strSql.AppendLine("              , 0                  AS ValorCrRecebido ");
                strSql.AppendLine("           FROM CONTAS_RECEBER     AS CP                       ");
                strSql.AppendLine("          where CP.STAREG <> 'D'                               ");
                strSql.AppendLine("            AND (CP.INDCONREC = 'N' OR CP.INDCONREC IS NULL)   ");
                strSql.AppendLine("            AND CP.CODEMP = " + pEmpresa);
                strSql.AppendLine("            AND DATE_FORMAT(CP.VENCONREC, '%Y') = " + pAno);
                strSql.AppendLine("            AND DATE_FORMAT(CP.VENCONREC, '%m') = " + pMes);

                strSql.AppendLine("      UNION ALL ");

                strSql.AppendLine("         SELECT 0                    AS ValorCpPagar    ");
                strSql.AppendLine("              , 0                    AS ValorCpPago     ");
                strSql.AppendLine("              , 0                    AS ValorCrReceber  ");
                strSql.AppendLine("              , SUM(CP.VLRRECCONREC) AS ValorCrRecebido ");
                strSql.AppendLine("           FROM CONTAS_RECEBER AS CP                    ");
                strSql.AppendLine("          where CP.STAREG <> 'D'                        ");
                strSql.AppendLine("            AND CP.INDCONREC = 'S'                      ");
                strSql.AppendLine("            AND CP.CODEMP = " + pEmpresa);
                strSql.AppendLine("            AND DATE_FORMAT(CP.DTRECCONREC, '%Y') = " + pAno);
                strSql.AppendLine("            AND DATE_FORMAT(CP.DTRECCONREC, '%m') = " + pMes);




                strSql.AppendLine("        ) T ");

                model = conexao.Query<HomeViewModel>(strSql.ToString());
            }

            return model.FirstOrDefault();
        }

		public HomeViewModel Vendas(string pEmpresa, string pAno, string pMes)
		{
			IEnumerable<HomeViewModel> model = null;

			using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
			{

				StringBuilder strSql = new StringBuilder();


				strSql.AppendLine("  SELECT sum(V.faturamento_peca + V.faturamento_pneu)   as Totalfaturamento_peca   ");
				strSql.AppendLine("       , sum(V.faturamento_maoobra)   as Totalfaturamento_maoobra   ");
				strSql.AppendLine("       , sum(V.faturamento_peca + V.faturamento_pneu + V.faturamento_maoobra) / sum(V.qtd_veiculo)    as Totalticketmedio  ");
                strSql.AppendLine("       , sum(V.qtd_veiculo) as    Totalqtd_veiculo ");
                strSql.AppendLine("       , ((sum(V.qtd_veiculo) - P.INDICEVENDAS)*100) / P.INDICEVENDAS as    PorIndicador ");
				strSql.AppendLine("       , ((sum(V.faturamento_peca + V.faturamento_pneu + V.faturamento_maoobra) / sum(V.qtd_veiculo) - P.INDICETICKETMEDIO) * 100) / P.INDICETICKETMEDIO   as Porticketmedio  ");
				strSql.AppendLine("       ,  ((sum(V.faturamento_maoobra) - P.RECEITASERVICO) * 100) / P.RECEITASERVICO   as PorFatServico ");
                strSql.AppendLine("       ,  ((sum(V.faturamento_peca + V.faturamento_pneu)  - P.TOTALFATPECA ) * 100) / P.TOTALFATPECA  as PorFatPeca ");
                strSql.AppendLine("       ,  sum(V.faturamento_peca + V.faturamento_pneu) + sum(V.faturamento_maoobra) as TOTALFATURAMENTO ");
				strSql.AppendLine("       ,  ((sum(V.faturamento_peca + V.faturamento_pneu + V.faturamento_maoobra)  - P.TOTALRECEITA ) * 100) / P.TOTALRECEITA as PorFat ");
				strSql.AppendLine("   FROM VENDAS as V, PESSOA as P ");

                strSql.AppendLine("   Where   V.CODPES = P.CODPES AND V.CODPES = " + pEmpresa);
				strSql.AppendLine("            AND DATE_FORMAT(V.DATA, '%Y') = " + pAno);
				strSql.AppendLine("            AND DATE_FORMAT(V.DATA, '%m') = " + pMes);

				model = conexao.Query<HomeViewModel>(strSql.ToString());
			}

			return model.FirstOrDefault();
		}
	}




}
