using System;
using System.Text;
using DAL;
using DTO;
using System.Data;


namespace BLL
{
    public class Home
    {
        // INSTANCIA CONECÇÃO SQL
        SQL_AcessoBancoDados sql_AcessoBancoDados = new SQL_AcessoBancoDados();

        // VERIFICAÇÃO PREENCHIMENTO DTO
        public void contatoVerificada(DTO.Contato contato)
        {
            if (contato.Mensagem== "" || contato.Mensagem == null)
            {
                contato.Mensagem = "N/A";
            }
        }

        // MÉTODOS
        public void ContatoGravar(DTO.Contato contato)
        {
            contatoVerificada(contato);
            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varNome", contato.Nome.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varEmail", contato.Email);
            sql_AcessoBancoDados.AdicionarParametro("varMensagem", contato.Mensagem);
            sql_AcessoBancoDados.Persistir(CommandType.StoredProcedure, "ContatoGravar");
        }
        public DTO.Home HomeInfomacoes(string URL)
        {
            var homeInfo = new DTO.Home();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varURL", URL);
            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "HomeInfomacoes");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                homeInfo.IdHome = Convert.ToInt32(dataRow["IdHome"]);
                homeInfo.IdEmail = Convert.ToInt32(dataRow["IdEmail"]);
                homeInfo.URL = Convert.ToString(dataRow["URL"]);
                homeInfo.TituloSite = Convert.ToString(dataRow["TituloSite"]);
                homeInfo.Cidade = Convert.ToString(dataRow["Cidade"]);
                homeInfo.Reuniao = Convert.ToString(dataRow["Reuniao"]);
                homeInfo.Endereco = Convert.ToString(dataRow["Endereco"]);
                homeInfo.UrlGoogleMaps = Convert.ToString(dataRow["UrlGoogleMaps"]);
                homeInfo.UrlFaceBook = Convert.ToString(dataRow["UrlFaceBook"]);
                homeInfo.UrlInstagran = Convert.ToString(dataRow["UrlInstagran"]);
                homeInfo.UrlYouTube = Convert.ToString(dataRow["UrlYouTube"]);
                homeInfo.UrlQrCode = Convert.ToString(dataRow["UrlQrCode"]);
                homeInfo.Unidade = Convert.ToString(dataRow["Unidade"]);
                homeInfo.NomeRedes = Convert.ToString(dataRow["NomeRedes"]);
                homeInfo.Email = Convert.ToString(dataRow["Email"]);
                homeInfo.Senha = Convert.ToString(dataRow["Senha"]);
            }

            return homeInfo;
        }
    }
}
