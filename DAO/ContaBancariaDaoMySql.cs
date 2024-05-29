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
    public class ContaBancariaDaoMySql
    {      

        public List<ContaBancariaViewModel> PopulaCombo()
        {
            IEnumerable<ContaBancariaViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("SELECT M.CODCONBAN AS CODIGO, M.NOMCONBAN AS NOME ");
                strSql.AppendLine("  FROM CONTA_BANCARIA M");
                strSql.AppendLine(" WHERE 1 = 1 ");                

                model = conexao.Query<ContaBancariaViewModel>(strSql.ToString());

            }

            return model.ToList();
        }

        public List<ContaBancariaViewModel> PopulaGrid(ContaBancariaViewModel pModel)
        {
            IEnumerable<ContaBancariaViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("SELECT CP.CODCONBAN AS Codigo        ");
                strSql.AppendLine("     , CP.NOMCONBAN AS Nome    ");

                strSql.AppendLine("  FROM CONTA_BANCARIA AS CP ");

                //strSql.AppendLine(" where CP.STAREG <> 'D' ");              

                strSql.AppendLine("  order by CP.NOMCONBAN DESC ");


                model = conexao.Query<ContaBancariaViewModel>(strSql.ToString());
            }

            return model.ToList();
        }

        public ContaBancariaViewModel Registro(string pCodigo)
        {
            IEnumerable<ContaBancariaViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                try
                {
                    StringBuilder strSql = new StringBuilder();


                    strSql.AppendLine("SELECT CP.CODCONBAN    AS Codigo        ");
                    strSql.AppendLine("     , CP.NOMCONBAN    AS Nome    ");
                    strSql.AppendLine("     , CP.AGECONBAN    AS Agencia    ");
                    strSql.AppendLine("     , CP.NUMCONBAN    AS Numero    ");
                    strSql.AppendLine("     , CP.DIGNUMCONBAN AS Digito    ");

                    strSql.AppendLine("  FROM CONTA_BANCARIA AS CP ");

                    strSql.AppendLine(" WHERE CP.STAREG <> 'D' ");
                    strSql.AppendLine("   AND CP.CODCONBAN = " + pCodigo);    

                    strSql.AppendLine(" ORDER BY CP.ORDEM ");


                    model = conexao.Query<ContaBancariaViewModel>(strSql.ToString());
                }
                catch (Exception ex)
                {
                }

            }

            return model.FirstOrDefault();
        }

        public List<ContaBancariaExtratoViewModel> PopulaGridExtrato(string pContaBancaria)
        {
            IEnumerable<ContaBancariaExtratoViewModel> model = null;

            try
            {
                using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
                {

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine("SELECT T.* ");
                    strSql.AppendLine("  FROM (   ");

                    strSql.AppendLine("         SELECT  'SALDO INICIAL' Historico");
                    strSql.AppendLine("              , A.DTINICIAL      Data ");
                    strSql.AppendLine("              , A.VLRCONBANSAL   Valor");
                    strSql.AppendLine("              , ''               Favorecido");

                    strSql.AppendLine("           FROM CONTA_BANCARIA_SALDO A");
                    strSql.AppendLine("          WHERE A.CODCONBAN = " + pContaBancaria);
                    strSql.AppendLine("            AND A.DTFINAL IS NULL ");

                    strSql.AppendLine("         UNION ALL");

                    strSql.AppendLine("         SELECT 'DEBITO'           Historico ");
                    strSql.AppendLine("              , CP.DTPAGTO         Data ");
                    strSql.AppendLine("              , CP.VLRCONPAGA * -1 Valor ");
                    strSql.AppendLine("              , P.NOMPES           Favorecido");
                    strSql.AppendLine("          FROM CONTAS_PAGAR CP");
                    strSql.AppendLine("          LEFT JOIN PESSOA         AS P  ON CP.CODPES    = P.CODPES");
                    strSql.AppendLine("         WHERE CP.CODCONBAN = " + pContaBancaria);
                    strSql.AppendLine("           AND CP.DTPAGTO >= (SELECT AI.DTINICIAL FROM CONTA_BANCARIA_SALDO AI WHERE AI.CODCONBAN = CP.CODCONBAN AND AI.DTFINAL IS NULL)");

                    strSql.AppendLine("         UNION ALL");

                    strSql.AppendLine("         SELECT 'CREDITO'       Historico ");
                    strSql.AppendLine("              , CP.DTRECCONREC  Data ");
                    strSql.AppendLine("              , CP.VLRRECCONREC Valor ");
                    strSql.AppendLine("              , P.NOMPES        Favorecido");
                    strSql.AppendLine("          FROM CONTAS_RECEBER CP");
                    strSql.AppendLine("          LEFT JOIN PESSOA         AS P  ON CP.CODPES    = P.CODPES");
                    strSql.AppendLine("         WHERE CP.CODCONBAN = " + pContaBancaria);
                    strSql.AppendLine("           AND CP.DTRECCONREC >= (SELECT AI.DTINICIAL FROM CONTA_BANCARIA_SALDO AI WHERE AI.CODCONBAN = CP.CODCONBAN AND AI.DTFINAL IS NULL)");

                    strSql.AppendLine("      ) T ");

                    strSql.AppendLine("  ORDER BY T.DATA ");


                    model = conexao.Query<ContaBancariaExtratoViewModel>(strSql.ToString());
                    return model.ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
