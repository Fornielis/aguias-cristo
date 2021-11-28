using System;
using System.Text;
using DAL;
using DTO;
using System.Data;

namespace BLL
{
    public class Banner
    {
        // INSTANCIA CONECÇÃO SQL
        SQL_AcessoBancoDados sql_AcessoBancoDados = new SQL_AcessoBancoDados();

        // MÉTODOS
        public int BannerGravar(DTO.Banner banner)
        {
            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varNome", banner.Nome.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varImagemByte", banner.ImagemByte);
            sql_AcessoBancoDados.AdicionarParametro("varEstatus", banner.Estatus.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varUnidade", banner.Unidade.ToUpper());
            int retorno = Convert.ToInt32(sql_AcessoBancoDados.Persistir(CommandType.StoredProcedure, "BannerGravar"));
            return retorno;
        }
        public DTO.BannerLista bannerListarPorUnidade(string unidade)
        {
            var bannerLista = new BannerLista();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varUnidade", unidade);
            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "BannerListarPorUnidade");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var banner = new DTO.Banner();
                banner.IdBanner = Convert.ToInt32(dataRow["IdBanner"]);
                banner.Nome = Convert.ToString(dataRow["Nome"]);
                banner.Estatus = Convert.ToString(dataRow["Estatus"]);
                banner.Unidade = Convert.ToString(dataRow["Unidade"]);

                // RECEBE IMAGEM EM BYTE DO BANCO E DEVOLE BASE64
                banner.ImagemByte = (byte[])(dataRow["ImagemByte"]);
                banner.base64imagem = Encoding.UTF8.GetString(banner.ImagemByte);
                banner.ImagemByte = null;

                bannerLista.Add(banner);
            }

            return bannerLista;
        }
        public DTO.Banner ListarBannerPorId(int idBanner)
        {
            var banner = new DTO.Banner();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varIdBanner", idBanner);
            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "BannerListarPorId");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                banner.IdBanner = Convert.ToInt32(dataRow["IdBanner"]);
                banner.Nome = Convert.ToString(dataRow["Nome"]);
                banner.Estatus = Convert.ToString(dataRow["Estatus"]);
                banner.Unidade = Convert.ToString(dataRow["Unidade"]);

                // RECEBE IMAGEM EM BYTE DO BANCO E DEVOLE BASE64
                banner.ImagemByte = (byte[])(dataRow["ImagemByte"]);
                banner.base64imagem = Encoding.UTF8.GetString(banner.ImagemByte);
                banner.ImagemByte = null;
            }

            return banner;
        }
        public void AlterarBanner(DTO.Banner banner)
        {
            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varIdBanner", banner.IdBanner);
            sql_AcessoBancoDados.AdicionarParametro("varNome", banner.Nome.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varImagemByte", banner.ImagemByte);
            sql_AcessoBancoDados.AdicionarParametro("varEstatus", banner.Estatus.ToUpper());
            sql_AcessoBancoDados.AdicionarParametro("varUnidade", banner.Unidade.ToUpper());
            sql_AcessoBancoDados.Persistir(CommandType.StoredProcedure, "BannerALterar");
        }
        public void DeletarBanner(int idBanner)
        {
            var banner = new DTO.Banner();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varIdBanner", idBanner);
            sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "BannerDeletar");
        }
        public DTO.BannerLista PequisarBanner(string valorInformado, string unidade)
        {
            // SETA VARIÁVEL ENTRADA PARA OPERADOR LIKE
            string valorConsulta = "%" + valorInformado + "%";

            // INSTÂNCIA LISTA
            var bannerLista = new BannerLista();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varValorInformado", valorConsulta);
            sql_AcessoBancoDados.AdicionarParametro("varUnidade", unidade);
            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "BannerPesquisa");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var banner = new DTO.Banner();
                banner.IdBanner = Convert.ToInt32(dataRow["IdBanner"]);
                banner.Nome = Convert.ToString(dataRow["Nome"]);
                banner.Estatus = Convert.ToString(dataRow["Estatus"]);

                // RECEBE IMAGEM EM BYTE DO BANCO E DEVOLE BASE64
                banner.ImagemByte = (byte[])(dataRow["ImagemByte"]);
                banner.base64imagem = Encoding.UTF8.GetString(banner.ImagemByte);
                banner.ImagemByte = null;

                bannerLista.Add(banner);
            }

            return bannerLista;
        }
        public DTO.BannerLista ImagensBannerAtivo(string unidade)
        {
            var bannerLista = new BannerLista();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varUnidade", unidade);

            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "BannerAtivo");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var banner = new DTO.Banner();
                banner.IdBanner = Convert.ToInt32(dataRow["IdBanner"]);
                banner.Nome = Convert.ToString(dataRow["Nome"]);
                banner.Estatus = Convert.ToString(dataRow["Estatus"]);

                // RECEBE IMAGEM EM BYTE DO BANCO E DEVOLE BASE64
                banner.ImagemByte = (byte[])(dataRow["ImagemByte"]);
                banner.base64imagem = Encoding.UTF8.GetString(banner.ImagemByte);
                banner.ImagemByte = null;

                bannerLista.Add(banner);
            }

            return bannerLista;
        }
    }
}
