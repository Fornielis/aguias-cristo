﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DAL.Properties;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class SQL_AcessoBancoDados
    {
        // CRIA CONEXAO 
        private MySqlConnection CriarConexao()
        {
            // RESGATA INFORMAÇÃO DO SERVIDOR PARA DEPLOY
            string servidor = ConfigurationManager.AppSettings["servidor"];

            if (servidor.Equals("local"))
            {
                return new MySqlConnection(Settings.Default.StringConexao_Localhost);
            }
            else
            {
                return new MySqlConnection(Settings.Default.StringConexao_Producao);
            }

        }

        // PARAMETROS
        private MySqlParameterCollection sqlParameterCollection = new MySqlCommand().Parameters;

        // LIMPA PARAMETROS
        public void LimparParametros()
        {
            sqlParameterCollection.Clear();
        }

        // ADICIONA PARAMETROS
        public void AdicionarParametro(string NomeParametro, object ValorParametro)
        {
            sqlParameterCollection.Add(new MySqlParameter(NomeParametro, ValorParametro));
        }

        // PERSISTIR -- INSERIR / ALTERAR / EXCLUIR
        public object Persistir(CommandType commandType, string NomeProcidureOuComandoSql)
        {
            //CRIA CONEXAO SQL SERVER
            MySqlConnection sqlConnection = CriarConexao();

            try
            {
                //ABRE CONEXAO SQL SERVER
                sqlConnection.Open();

                //COMANDO QUE LEVA INFORMAÇÃO AO SQL SERVER
                MySqlCommand sqlCommand = sqlConnection.CreateCommand();

                //DEFINI ITENS A SEREM TRANSFERIDOS PELO COMANDO AO SQL SERVER
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = NomeProcidureOuComandoSql;
                sqlCommand.CommandTimeout = 5000;

                //ADICIONA PARAMETROS AO COMANDO
                foreach (MySqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new MySqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
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
            MySqlConnection sqlConnection = CriarConexao();

            try
            {
                //ABRE CONEXAO SQL SERVER
                sqlConnection.Open();

                //COMANDO QUE LEVA INFORMAÇÃO AO SQL SERVER
                MySqlCommand sqlCommand = sqlConnection.CreateCommand();

                //DEFINI ITENS A SEREM TRANSFERIDOS PELO COMANDO AO SQL SERVER
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = NomeProcidureOuComandoSql;
                sqlCommand.CommandTimeout = 5000;

                //ADICIONA PARAMETROS AO COMANDO
                foreach (MySqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new MySqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                //CRIAR ADAPTADOR
                MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(sqlCommand);

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
