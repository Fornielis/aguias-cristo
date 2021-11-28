using System;
using System.Data;
using DTO;
using DAL;

namespace BLL
{
    public class Sistema
    {
        // INSTANCIA CONECÇÃO SQL
        SQL_AcessoBancoDados sql_AcessoBancoDados = new SQL_AcessoBancoDados();

        public int GravarErro(SistemaErro sistemaErro)
        {
            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varUsuario", sistemaErro.Usuario);
            sql_AcessoBancoDados.AdicionarParametro("varProcedimento", sistemaErro.Procedimento);
            sql_AcessoBancoDados.AdicionarParametro("varController", sistemaErro.Controller);
            sql_AcessoBancoDados.AdicionarParametro("varAcao", sistemaErro.Acao);
            sql_AcessoBancoDados.AdicionarParametro("varErro", sistemaErro.Erro);
            int IdErro = Convert.ToInt32(sql_AcessoBancoDados.Persistir(CommandType.StoredProcedure, "SistemaGravarErro"));

            return IdErro;
        }
        public SistemaEmail siatemaEmail(int idEmail)
        {
            var email = new SistemaEmail();

            sql_AcessoBancoDados.LimparParametros();
            sql_AcessoBancoDados.AdicionarParametro("varIdEmail", idEmail);
            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "SistemaRecuperarEmail");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                email.Perfil = Convert.ToString(dataRow["IdEmail"]);
                email.Perfil = Convert.ToString(dataRow["Perfil"]);
                email.Email = Convert.ToString(dataRow["Email"]);
                email.Senha = Convert.ToString(dataRow["Senha"]);
            }

            return email;
        }
    }
}
