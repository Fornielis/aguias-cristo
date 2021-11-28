using System;
using DAL;
using DTO;
using System.Data;

namespace BLL
{
    public class Unidade
    {
        // INSTANCIA CONECÇÃO SQL
        SQL_AcessoBancoDados sql_AcessoBancoDados = new SQL_AcessoBancoDados();

        // VERIFICAÇÃO PREENCHIMENTO DTO
        public void unidadeVerificada(DTO.Unidade unidade)
        {
            if (unidade.Telefone == "" || unidade.Telefone == null)
            {
                unidade.Telefone = "N/A";
            }
            if (unidade.Email == "" || unidade.Email == null)
            {
                unidade.Email = "N/A";
            }
            if (unidade.UrlSite == "" || unidade.UrlSite == null)
            {
                unidade.UrlSite = "N/A";
            }
            if (unidade.Regional == "" || unidade.Regional == null)
            {
                unidade.Regional = "N/A";
            }
            if (unidade.UF == "" || unidade.UF == null)
            {
                unidade.UF = "N/A";
            }
        }

        // MÉTODOS
        public int GravarUnidade(DTO.Unidade unidade)
        {
            // VERIDICA UNIDAADE
            unidadeVerificada(unidade);

            // SALVA UNIDADE NO BANCO
            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varNome", unidade.Nome.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varEstatus", unidade.Estatus.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varEndereco", unidade.Endereco.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varPais", unidade.Pais.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varUF", unidade.UF.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varCidade", unidade.Cidade.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varTelefone", unidade.Telefone);
            sql_AcessoBancoDados.AdicionarParametro("varEmail", unidade.Email);
            sql_AcessoBancoDados.AdicionarParametro("varUrlSite", unidade.UrlSite);
            sql_AcessoBancoDados.AdicionarParametro("varUrlGoogleMaps", unidade.UrlGoogleMaps);
            sql_AcessoBancoDados.AdicionarParametro("varReunioes", unidade.Reunioes.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varRegional", unidade.Regional.ToUpper());

            int retorno = Convert.ToInt32(sql_AcessoBancoDados.Persistir(CommandType.StoredProcedure, "UnidadeGravar"));

            return retorno;
        }
        public DTO.UnidadeLista UnidadeListar()
        {
            var unidadeLista = new UnidadeLista();

            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "UnidadeListar");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var unidade = new DTO.Unidade();
                unidade.IdUnidade = Convert.ToInt32(dataRow["IdUnidade"]);
                unidade.Nome = Convert.ToString(dataRow["Nome"]);
                unidade.Estatus = Convert.ToString(dataRow["Estatus"]);
                unidade.Pais = Convert.ToString(dataRow["Pais"]);
                unidade.UF = Convert.ToString(dataRow["UF"]);
                unidade.Cidade = Convert.ToString(dataRow["Cidade"]);
                unidade.Endereco = Convert.ToString(dataRow["Endereco"]);
                unidadeLista.Add(unidade);
            }

            return unidadeLista;
        }
        public DTO.UnidadeLista UnidadePesquisa(string valorInformado)
        {
            // SETA VARIÁVEL ENTRADA PARA OPERADOR LIKE
            string valorConsulta = "%" + valorInformado + "%";

            var unidadeLista = new UnidadeLista();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varvalorInformado", valorConsulta.ToUpper());

            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "UnidadePesquisa");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var unidade = new DTO.Unidade();
                unidade.IdUnidade = Convert.ToInt32(dataRow["IdUnidade"]);
                unidade.Nome = Convert.ToString(dataRow["Nome"]);
                unidade.Estatus = Convert.ToString(dataRow["Estatus"]);               
                unidade.Pais = Convert.ToString(dataRow["Pais"]);
                unidade.UF = Convert.ToString(dataRow["UF"]);
                unidade.Cidade = Convert.ToString(dataRow["Cidade"]);
                unidade.Endereco = Convert.ToString(dataRow["Endereco"]);
                unidadeLista.Add(unidade);
            }

            return unidadeLista;
        }
        public DTO.Unidade UnidadePorId(int idUnidade)
        {
            var unidade = new DTO.Unidade();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varidUnidade", idUnidade);

            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "UnidadePorId");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                unidade.IdUnidade = Convert.ToInt32(dataRow["IdUnidade"]);
                unidade.Nome = Convert.ToString(dataRow["Nome"]);
                unidade.Estatus = Convert.ToString(dataRow["Estatus"]);
                unidade.Endereco = Convert.ToString(dataRow["Endereco"]);
                unidade.Pais = Convert.ToString(dataRow["Pais"]);
                unidade.UF = Convert.ToString(dataRow["UF"]);
                unidade.Cidade = Convert.ToString(dataRow["Cidade"]);
                unidade.Telefone = Convert.ToString(dataRow["Telefone"]);
                unidade.Email = Convert.ToString(dataRow["Email"]);
                unidade.UrlSite = Convert.ToString(dataRow["UrlSite"]);
                unidade.UrlGoogleMaps = Convert.ToString(dataRow["UrlGoogleMaps"]);
                unidade.Reunioes = Convert.ToString(dataRow["Reunioes"]);
                unidade.Regional = Convert.ToString(dataRow["Regional"]);
            }

            return unidade;
        }
        public void UnidadeAlterar(DTO.Unidade unidade)
        {
            // VERIDICA UNIDAADE
            unidadeVerificada(unidade);

            // ALTERA UNIDADE
            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varIdUnidade", unidade.IdUnidade);
            sql_AcessoBancoDados.AdicionarParametro("varNome", unidade.Nome.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varEstatus", unidade.Estatus.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varEndereco", unidade.Endereco.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varPais", unidade.Pais.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varUF", unidade.UF.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varCidade", unidade.Cidade.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varTelefone", unidade.Telefone);
            sql_AcessoBancoDados.AdicionarParametro("varEmail", unidade.Email);
            sql_AcessoBancoDados.AdicionarParametro("varUrlSite", unidade.UrlSite);
            sql_AcessoBancoDados.AdicionarParametro("varUrlGoogleMaps", unidade.UrlGoogleMaps);
            sql_AcessoBancoDados.AdicionarParametro("varReunioes", unidade.Reunioes.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varRegional", unidade.Regional.ToUpper());
            sql_AcessoBancoDados.Persistir(CommandType.StoredProcedure, "UnidadeAlterar");
        }
        public void UnidadeDeletar(int idUnidade)
        {
            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varIdunidade", idUnidade);
            sql_AcessoBancoDados.Persistir(CommandType.StoredProcedure, "UnidadeDeletar");
        }
        public DTO.UnidadeLista UnidadePorRegional(string regional)
        {
            var unidadeLista = new UnidadeLista();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varRegional", regional);
            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "UnidadePorRegional");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var unidade = new DTO.Unidade();
                unidade.IdUnidade = Convert.ToInt32(dataRow["IdUnidade"]);
                unidade.Nome = Convert.ToString(dataRow["Nome"]);
                unidadeLista.Add(unidade);
            }

            return unidadeLista;
        }
        public DTO.UnidadeLista UnidadeOutrosPaises()
        {
            var unidadeLista = new UnidadeLista();

            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "UnidadeOutrosPaises");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var unidade = new DTO.Unidade();
                unidade.Pais = Convert.ToString(dataRow["Pais"]);
                unidadeLista.Add(unidade);
            }

            return unidadeLista;
        }
        public DTO.UnidadeLista UnidadePorEstado()
        {
            var unidadeLista = new UnidadeLista();

            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "UnidadePorEstado");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var unidade = new DTO.Unidade();
                unidade.UF = Convert.ToString(dataRow["UF"]);
                unidadeLista.Add(unidade);
            }

            return unidadeLista;
        }
        public DTO.UnidadeLista UnidadeEstado(string uf)
        {
            var unidadeLista = new UnidadeLista();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varUF", uf);
            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "UnidadeEstado");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var unidade = new DTO.Unidade();
                unidade.IdUnidade = Convert.ToInt32(dataRow["IdUnidade"]);
                unidade.Nome = Convert.ToString(dataRow["Nome"]);
                unidadeLista.Add(unidade);
            }

            return unidadeLista;
        }
        public DTO.UnidadeLista UnidadePais(string pais)
        {
            var unidadeLista = new UnidadeLista();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varPais", pais);
            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "UnidadePais");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var unidade = new DTO.Unidade();
                unidade.IdUnidade = Convert.ToInt32(dataRow["IdUnidade"]);
                unidade.Nome = Convert.ToString(dataRow["Nome"]);
                unidadeLista.Add(unidade);
            }

            return unidadeLista;
        }
        public DTO.UnidadeRegionalLista UnidadesRegional()
        {
            var unidadesLista = new UnidadeRegionalLista();

            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "UnidadesRegional");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var unidade = new DTO.UnidadeRegional();
                unidade.Unidade = Convert.ToString(dataRow["Unidade"]);
                unidadesLista.Add(unidade);
            }

            return unidadesLista;
        }
    }
}
