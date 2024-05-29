using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MySql.Data.MySqlClient;

namespace DaoMySql
{
    public class ConexaoDaoMySql
    {
        public class sqlServerDaoClientDSN
        {

            static string vStringConexao = "server=imconsultoria.cmproject.com.br;User Id=imconsultoria;database=imconsultoria; password=Im@2024#01";
            

            private static readonly sqlServerDaoClientDSN bcoSqlServer = new sqlServerDaoClientDSN();

            public static sqlServerDaoClientDSN getBcoSqlServer()
            {
                return bcoSqlServer;
            }

            public MySqlConnection getConexaoSqlServerDaoDSN()
            {
                return new MySqlConnection(vStringConexao);
            }

            public static string StringDeConexaoClient
            {
                get
                {
                    return vStringConexao;
                }
            }
        }

        public class MySqlClientDSN
        {
            private static readonly MySqlClientDSN bcoMySql = new MySqlClientDSN();

            public static MySqlClientDSN getBcoMySql()
            {
                return bcoMySql;
            }
            public MySqlConnection getConexaoMySqlDaoDSN()
            {
                string connectionString = string.Empty;
                connectionString = "server=imconsultoria.cmproject.com.br;User Id=imconsultoria;database=imconsultoria; password=Im@2024#01";
                

                return new MySqlConnection(connectionString);
            }
        }

        
    }

    public class Contexto : IDisposable
    {
        //http://cleytonferrari.com/mysql-com-asp-net-mvc/

        private MySqlConnection conexao;

        public Contexto()
        {
            var conexaoString = "server=imconsultoria.cmproject.com.br;User Id=imconsultoria;database=imconsultoria; password=Im@2024#01";
            conexao = new MySqlConnection(conexaoString);
        }

        public int ExecutaComando(string comandoSQL, Dictionary<string, object> parametros)
        {
            var resultado = 0;
            if (string.IsNullOrEmpty(comandoSQL))
            {
                throw new ArgumentException("O comando SQL não pode ser nulo ou vazio");
            }
            try
            {
                AbrirConexao();
                var cmdComando = CriarComando(comandoSQL, parametros);
                resultado = cmdComando.ExecuteNonQuery();
            }
            finally
            {
                FecharConexao();
            }

            return resultado;
        }

        public List<Dictionary<string, string>> ExecutaComandoComRetorno(string comandoSQL, Dictionary<string, object> parametros = null)
        {
            List<Dictionary<string, string>> linhas = null;

            if (string.IsNullOrEmpty(comandoSQL))
            {
                throw new ArgumentException("O comando SQL não pode ser nulo ou vazio");
            }
            try
            {
                AbrirConexao();
                var cmdComando = CriarComando(comandoSQL, parametros);
                using (var reader = cmdComando.ExecuteReader())
                {
                    linhas = new List<Dictionary<string, string>>();
                    while (reader.Read())
                    {
                        var linha = new Dictionary<string, string>();

                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            var nomeDaColuna = reader.GetName(i);
                            var valorDaColuna = reader.IsDBNull(i) ? null : reader.GetString(i);
                            linha.Add(nomeDaColuna, valorDaColuna);
                        }
                        linhas.Add(linha);
                    }
                }
            }
            finally
            {
                FecharConexao();
            }

            return linhas;
        }

        private MySqlCommand CriarComando(string comandoSQL, Dictionary<string, object> parametros)
        {
            var cmdComando = conexao.CreateCommand();
            cmdComando.CommandText = comandoSQL;
            AdicionarParamatros(cmdComando, parametros);
            return cmdComando;
        }

        private static void AdicionarParamatros(MySqlCommand cmdComando, Dictionary<string, object> parametros)
        {
            if (parametros == null)
                return;

            foreach (var item in parametros)
            {
                var parametro = cmdComando.CreateParameter();
                parametro.ParameterName = item.Key;
                parametro.Value = item.Value ?? DBNull.Value;
                cmdComando.Parameters.Add(parametro);
            }
        }

        private void AbrirConexao()
        {
            if (conexao.State == ConnectionState.Open) return;

            conexao.Open();
        }

        private void FecharConexao()
        {
            if (conexao.State == ConnectionState.Open)
                conexao.Close();
        }

        public void Dispose()
        {
            if (conexao == null) return;

            conexao.Dispose();
            conexao = null;
        }
    }
}
