using System;
using System.Data;
using System.Data.SqlClient;
using DAL.Properties;

namespace DAL
{
    public class SQL_AcessoSqlServer
    {
        // CRIA CONEXAO AO SQL SERVER
        private SqlConnection CriarConexao()
        {
            return new SqlConnection(Settings.Default.StringConexao_AC);
        }

        // PARAMETROS QUE VÃO PARA O SQL SERVER
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;

        // LIMPA PARAMETROS
        public void LimparParametros()
        {
            sqlParameterCollection.Clear();
        }

        // ADICIONA PARAMETROS
        public void AdicionarParametro(string NomeParametro, object ValorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(NomeParametro, ValorParametro));
        }

        // PERSISTIR -- INSERIR / ALTERAR / EXCLUIR
        public object Persistir(CommandType commandType, string NomeProcidureOuComandoSql)
        {
            //CRIA CONEXAO SQL SERVER
            SqlConnection sqlConnection = CriarConexao();

            try
            {
                //ABRE CONEXAO SQL SERVER
                sqlConnection.Open();

                //COMANDO QUE LEVA INFORMAÇÃO AO SQL SERVER
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                //DEFINI ITENS A SEREM TRANSFERIDOS PELO COMANDO AO SQL SERVER
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = NomeProcidureOuComandoSql;
                sqlCommand.CommandTimeout = 5000;

                //ADICIONA PARAMETROS AO COMANDO
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                //EXECUTA O CAMNADO NO SQL SERVER
                return sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        // SELECT SQL SERVER
        public DataTable Consultar(CommandType commandType, string NomeProcidureOuComandoSql)
        {
            //CRIA CONEXAO SQL SERVER
            SqlConnection sqlConnection = CriarConexao();

            try
            {
                //ABRE CONEXAO SQL SERVER
                sqlConnection.Open();

                //COMANDO QUE LEVA INFORMAÇÃO AO SQL SERVER
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                //DEFINI ITENS A SEREM TRANSFERIDOS PELO COMANDO AO SQL SERVER
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = NomeProcidureOuComandoSql;
                sqlCommand.CommandTimeout = 5000;

                //ADICIONA PARAMETROS AO COMANDO
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                //CRIAR ADAPTADOR
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                //CRIAR TABELA DADOS VAZIA QUE IRA RECEBER DADOS DO SQL SERVER
                DataTable dataTable = new DataTable();

                //EXECUTA COMANDO NO SQL SERVER / ADAPTER PREENCHE A TABELA DE DADOS
                sqlDataAdapter.Fill(dataTable);
                return dataTable;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
