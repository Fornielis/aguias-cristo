using System;
using DAL;
using DTO;
using System.Data;
using System.Text;

namespace BLL
{
    public class Evento
    {
        // INSTANCIA CONECÇÃO SQL
        SQL_AcessoBancoDados sql_AcessoBancoDados = new SQL_AcessoBancoDados();

        // VERIFICAÇÃO PREENCHIMENTO DTO
        public void eventoVerificada(DTO.Evento evento)
        {
            if (evento.Localidade == "" || evento.Localidade == null)
            {
                evento.Localidade = "N/A";
            }
            if (evento.Endereco == "" || evento.Endereco == null)
            {
                evento.Endereco = "N/A";
            }
            if (evento.UF == "" || evento.UF == null)
            {
                evento.UF = "N/A";
            }
            if (evento.Pais == "" || evento.Pais == null)
            {
                evento.Pais = "N/A";
            }
            if (evento.Contato == "" || evento.Contato == null)
            {
                evento.Contato = "N/A";
            }
            if (evento.Anotacao == "" || evento.Anotacao == null)
            {
                evento.Anotacao = "N/A";
            }

        }

        // MÉTODOS
        public int EventoGravar(DTO.Evento evento)
        {
            // VERIDICA UNIDAADE
            eventoVerificada(evento);

            // SALVA UNIDADE NO BANCO
            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varDescricao", evento.Descricao.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varUnidade", evento.Unidade.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varLocalidade", evento.Localidade.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varEndereco", evento.Endereco.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varUF", evento.UF.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varPais", evento.Pais.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varContato", evento.Contato.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varAnotacao", evento.Anotacao.ToUpper());
            int retorno = Convert.ToInt32(sql_AcessoBancoDados.Persistir(CommandType.StoredProcedure, "EventoGravar"));

            return retorno;
        }
        public void EventoAlterar(DTO.Evento evento)
        {
            // VERIDICA UNIDAADE
            eventoVerificada(evento);

            // SALVA UNIDADE NO BANCO
            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varIdEvento", evento.IdEvento);
            sql_AcessoBancoDados.AdicionarParametro("varDescricao", evento.Descricao.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varUnidade", evento.Unidade.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varLocalidade", evento.Localidade.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varEndereco", evento.Endereco.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varUF", evento.UF.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varPais", evento.Pais.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varContato", evento.Contato.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varAnotacao", evento.Anotacao.ToUpper());
            sql_AcessoBancoDados.Persistir(CommandType.StoredProcedure, "EventoAlterar");
        }
        public DTO.Evento EventoPorId(int idEvento)
        {
            var evento = new DTO.Evento();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varIdEvento", idEvento);
            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "EventoPorId");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                evento.IdEvento = Convert.ToInt32(dataRow["IdEvento"]);
                evento.Descricao = Convert.ToString(dataRow["Descricao"]);
                evento.Unidade = Convert.ToString(dataRow["Unidade"]);
                evento.Localidade = Convert.ToString(dataRow["Localidade"]);
                evento.Endereco = Convert.ToString(dataRow["Endereco"]);
                evento.UF = Convert.ToString(dataRow["UF"]);
                evento.Pais = Convert.ToString(dataRow["Pais"]);
                evento.Contato = Convert.ToString(dataRow["Contato"]);
                evento.Anotacao = Convert.ToString(dataRow["Anotacao"]);
            }

            return evento;
        }
        public void EventoDataGravar(DTO.EventoData eventoData) 
        {
            // VARIÁVEL QUE RECEBE APENAS O MÊS DO EVENTO
            string mes = eventoData.DataEvento.Substring(3,2);

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varIdEvento", eventoData.IdEvento);
            sql_AcessoBancoDados.AdicionarParametro("varDataEvento", eventoData.DataEvento);
            sql_AcessoBancoDados.AdicionarParametro("varReservado", eventoData.Reservado);
            sql_AcessoBancoDados.AdicionarParametro("varMesEvento", mes);
            sql_AcessoBancoDados.Persistir(CommandType.StoredProcedure, "EventoDataGravar");
        }
        public DTO.EventoDataLista EventoDatasPorEvento(int idEvento)
        {
            var eventoDataLista = new DTO.EventoDataLista();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varIdEvento", idEvento);
            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "EventoDatasPorEvento");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var data = new DTO.EventoData();
                data.IdData = Convert.ToInt32(dataRow["IdData"]);
                data.IdEvento = Convert.ToInt32(dataRow["IdEvento"]);
                data.DataEvento = Convert.ToString(dataRow["DataEvento"]);
                data.Reservado = Convert.ToString(dataRow["Reservado"]);
                eventoDataLista.Add(data);
            }

            return eventoDataLista;
        }
        public void EventoDataDeletar(int idData, int idEvento)
        {
            // VERIFICA SE EXISTE APENAS UMA DATA PARA O EVENTO
            // CASO EXISTE APENAS UMA DATA O CAMPO "MesEvento" NA TABELA "eventos" É APAGADO
            int quantidadeDatas = EventoDatasPorEvento(idEvento).Count;
            if (quantidadeDatas == 1)
            {
                sql_AcessoBancoDados.LimparParametros();
                sql_AcessoBancoDados.AdicionarParametro("varIdEvento", idEvento);
                sql_AcessoBancoDados.Persistir(CommandType.StoredProcedure, "EventoAlteraMesEventoEventos");
            }

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varIdData", idData);
            sql_AcessoBancoDados.Persistir(CommandType.StoredProcedure, "EventoDataDeletar");
        }
        public DTO.EventoData EventoDataPorId(int idData)
        {
            var eventoData = new DTO.EventoData();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varIdData", idData);
            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "EventoDataPorId");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                eventoData.IdData = Convert.ToInt32(dataRow["IdData"]);
                eventoData.IdEvento = Convert.ToInt32(dataRow["IdEvento"]);
                eventoData.DataEvento = Convert.ToString(dataRow["DataEvento"]);
                eventoData.Reservado = Convert.ToString(dataRow["Reservado"]);
            }

            return eventoData;
        }
        public DTO.EventoData EventoDataExistente(string dataEvento)
        {
            var eventoData = new DTO.EventoData();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varDataEvento", dataEvento);
            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "EventoDataExistente");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                eventoData.IdData = Convert.ToInt32(dataRow["IdData"]);
            }

            return eventoData;
        }
        public DTO.EventoData EventoDataReservadaExistente(string dataEvento)
        {
            var eventoData = new DTO.EventoData();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varDataEvento", dataEvento);
            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "EventoDataReservadaExistente");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                eventoData.IdData = Convert.ToInt32(dataRow["IdData"]);
            }

            return eventoData;
        }
        public void EventoDataDivulgacaoGravar(DTO.EventosMesDivulgacao eventosMesDivulgacao)
        {
            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varIdEvento", eventosMesDivulgacao.IdEvento);
            sql_AcessoBancoDados.AdicionarParametro("MesDivulgacao", eventosMesDivulgacao.MesDivulgacao);
            sql_AcessoBancoDados.Persistir(CommandType.StoredProcedure, "EventoDataDivulgacaoGravar");
        }
        public DTO.EventosMesDivulgacaoLista EventoMesDivulgacaoPorEvento(int idEvento)
        {
            var eventosMesDivulgacaoLista = new DTO.EventosMesDivulgacaoLista();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varIdEvento", idEvento);
            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "EventoMesDivulgacaoPorEvento");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var eventosMesDivulgacao = new DTO.EventosMesDivulgacao();
                eventosMesDivulgacao.IdMes = Convert.ToInt32(dataRow["IdMes"]);
                eventosMesDivulgacao.IdEvento = Convert.ToInt32(dataRow["IdEvento"]);
                eventosMesDivulgacao.MesDivulgacao = Convert.ToString(dataRow["MesDivulgacao"]);
                eventosMesDivulgacaoLista.Add(eventosMesDivulgacao);
            }

            return eventosMesDivulgacaoLista;
        }
        public void EventoMesDivulgacaoDeletar(int idMes)
        {
            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varIdMes", idMes);
            sql_AcessoBancoDados.Persistir(CommandType.StoredProcedure, "EventoMesDivulgacaoDeletar");
        }
        public DTO.EventoLista EventoPorUnidade(string unidade)
        {
            var eventoLista = new DTO.EventoLista();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varUnidade", unidade);
            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "EventoPorUnidade");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var evento = new DTO.Evento();
                evento.IdEvento = Convert.ToInt32(dataRow["IdEvento"]);
                evento.Descricao = Convert.ToString(dataRow["Descricao"]);
                evento.Unidade = Convert.ToString(dataRow["Unidade"]);
                evento.Localidade = Convert.ToString(dataRow["Localidade"]);
                evento.Endereco = Convert.ToString(dataRow["Endereco"]);
                evento.UF = Convert.ToString(dataRow["UF"]);
                evento.Pais = Convert.ToString(dataRow["Pais"]);
                evento.Contato = Convert.ToString(dataRow["Contato"]);
                evento.Anotacao = Convert.ToString(dataRow["Anotacao"]);
                eventoLista.Add(evento);
            }

            return eventoLista;
        }
        public DTO.EventoLista EventoPesquisa(string unidade, string valorInformado)
        {
            // SETA VARIÁVEL ENTRADA PARA OPERADOR LIKE
            string valorConsulta = "%" + valorInformado + "%";

            var eventoLista = new DTO.EventoLista();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varUnidade", unidade);
            sql_AcessoBancoDados.AdicionarParametro("varvalorInformado", valorConsulta);
            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "EventoPesquisa");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var evento = new DTO.Evento();
                evento.Descricao = Convert.ToString(dataRow["Descricao"]);
                eventoLista.Add(evento);
            }

            return eventoLista;
        }
        public void EventoDeletar(int idMes)
        {
            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varIdEvento", idMes);
            sql_AcessoBancoDados.Persistir(CommandType.StoredProcedure, "EventoDeletar");
        }
        public DTO.EventoLista EventoPorMesDivulgacao(string unidade, string mesDivulgacao, string mesAtualNumero)
        {
            var eventoLista = new DTO.EventoLista();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varUnidade", unidade);
            sql_AcessoBancoDados.AdicionarParametro("varMesDivulgacao", mesDivulgacao);
            sql_AcessoBancoDados.AdicionarParametro("varMesAtual", mesAtualNumero);
            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "EventoPorMesDivulgacao");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var evento = new DTO.Evento();
                evento.IdEvento = Convert.ToInt32(dataRow["IdEvento"]);
                evento.Descricao = Convert.ToString(dataRow["Descricao"]);
                evento.Unidade = Convert.ToString(dataRow["Unidade"]);
                evento.Localidade = Convert.ToString(dataRow["Localidade"]);
                evento.Endereco = Convert.ToString(dataRow["Endereco"]);
                evento.UF = Convert.ToString(dataRow["UF"]);
                evento.Pais = Convert.ToString(dataRow["Pais"]);
                evento.Contato = Convert.ToString(dataRow["Contato"]);
                evento.Anotacao = Convert.ToString(dataRow["Anotacao"]);
                eventoLista.Add(evento);

                // RESGATAS DATAS DE UM EVENTO
                // CONCATENA EM UMA MESMA VARIÁVEL
                var datas = EventoDatasPorEvento(evento.IdEvento);
                string datasConcatenadas = "";
                foreach (var item in datas)
                {
                    datasConcatenadas = datasConcatenadas + " | " + item.DataEvento;
                }

                // ELIMINA OS 3 PRIMEIROS CARACTERES
                int caracteresFinal = datasConcatenadas.Length - 3;
                evento.Datas = datasConcatenadas.Substring(3, caracteresFinal);
            }

            return eventoLista;
        }
        public DTO.EventoLista EventoPorMes(string unidade, string mesEvento)
        {
            var eventoLista = new DTO.EventoLista();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varUnidade", unidade);
            sql_AcessoBancoDados.AdicionarParametro("varMesEvento", mesEvento);
            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "EventoPorMes");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var evento = new DTO.Evento();
                evento.IdEvento = Convert.ToInt32(dataRow["IdEvento"]);
                evento.Descricao = Convert.ToString(dataRow["Descricao"]);
                evento.Unidade = Convert.ToString(dataRow["Unidade"]);
                evento.Localidade = Convert.ToString(dataRow["Localidade"]);
                evento.Endereco = Convert.ToString(dataRow["Endereco"]);
                evento.UF = Convert.ToString(dataRow["UF"]);
                evento.Pais = Convert.ToString(dataRow["Pais"]);
                evento.Contato = Convert.ToString(dataRow["Contato"]);
                evento.Anotacao = Convert.ToString(dataRow["Anotacao"]);
                eventoLista.Add(evento);

                // RESGATAS DATAS DE UM EVENTO
                // CONCATENA EM UMA MESMA VARIÁVEL
                var datas = EventoDatasPorEvento(evento.IdEvento);
                string datasConcatenadas = "";
                foreach (var item in datas)
                {
                    datasConcatenadas = datasConcatenadas + " | " + item.DataEvento;
                }

                // ELIMINA OS 3 PRIMEIROS CARACTERES
                int caracteresFinal = datasConcatenadas.Length - 3;
                evento.Datas = datasConcatenadas.Substring(3, caracteresFinal);
            }

            return eventoLista;
        }
    }
}
