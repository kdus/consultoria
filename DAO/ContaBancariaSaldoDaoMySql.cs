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
    public class ContaBancariaSaldoDaoMySql
    {       

        public List<ContaBancariaSaldoViewModel> PopulaGrid(ContaBancariaSaldoViewModel pModel)
        {
            IEnumerable<ContaBancariaSaldoViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("SELECT CP.CODCONBANSAL AS Codigo       ");
                strSql.AppendLine("     , CP.DTINICIAL    AS DataInicial  ");
                strSql.AppendLine("     , CP.DTFINAL      AS DataFinal    ");
                strSql.AppendLine("     , CP.VLRCONBANSAL AS Valor        ");

                strSql.AppendLine("  FROM CONTA_BANCARIA_SALDO AS CP ");

                strSql.AppendLine(" WHERE CP.CODCONBAN = " + pModel.ContaBancaria);

                strSql.AppendLine("  order by CP.DTINICIAL DESC ");

                model = conexao.Query<ContaBancariaSaldoViewModel>(strSql.ToString());
            }

            return model.ToList();
        }

        public double Saldo(string pContaBancaria)
        {
            //IEnumerable<ContaBancariaSaldoViewModel> model = null;

            try
            {
                using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
                {

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine("SELECT SUM(T.VALOR) AS Valor ");
                    strSql.AppendLine("  FROM (" );

                    strSql.AppendLine("         SELECT  'SALDO INICIAL' ORIGEM");                    
                    strSql.AppendLine("              , A.VLRCONBANSAL   VALOR");                    
                    strSql.AppendLine("           FROM CONTA_BANCARIA_SALDO A");
                    strSql.AppendLine("          WHERE A.CODCONBAN = " + pContaBancaria );
                    strSql.AppendLine("            AND A.DTFINAL IS NULL ");
                                                
                    strSql.AppendLine("         UNION ALL" );
                                                
                    strSql.AppendLine("         SELECT 'DEBITO' ORIGEM ");                    
                    strSql.AppendLine("              , CP.VLRCONPAGA * -1 VALOR ");
                    strSql.AppendLine("          FROM CONTAS_PAGAR CP");
                    strSql.AppendLine("         WHERE CP.CODCONBAN = " + pContaBancaria);
                    strSql.AppendLine("           AND CP.DTPAGTO >= (SELECT AI.DTINICIAL FROM CONTA_BANCARIA_SALDO AI WHERE AI.CODCONBAN = CP.CODCONBAN AND AI.DTFINAL IS NULL)");

                    strSql.AppendLine("         UNION ALL");

                    strSql.AppendLine("         SELECT 'CREDITO' ORIGEM ");                    
                    strSql.AppendLine("              , CP.VLRRECCONREC VALOR ");
                    strSql.AppendLine("          FROM CONTAS_RECEBER CP");
                    strSql.AppendLine("         WHERE CP.CODCONBAN = " + pContaBancaria);
                    strSql.AppendLine("           AND CP.DTRECCONREC >= (SELECT AI.DTINICIAL FROM CONTA_BANCARIA_SALDO AI WHERE AI.CODCONBAN = CP.CODCONBAN AND AI.DTFINAL IS NULL)");

                    strSql.AppendLine("      ) T ");

                    return conexao.Query<double>(strSql.ToString()).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }
        
    }
}
