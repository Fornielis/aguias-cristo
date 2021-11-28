using System;
using System.Data;
using DAL;
using DTO;

namespace BLL
{
    public class Usuario
    {
        // INSTANCIA CONECÇÃO SQL
        SQL_AcessoBancoDados sql_AcessoBancoDados = new SQL_AcessoBancoDados();

        public UsuarioAutenticado usuarioAutenticacao(UsuarioLogin usuarioLogin)
        {
            var usuarioAutenticado = new UsuarioAutenticado();

            sql_AcessoBancoDados.LimparParametros();

            sql_AcessoBancoDados.AdicionarParametro("varCPF", usuarioLogin.Usuario);
            sql_AcessoBancoDados.AdicionarParametro("varCarteirinha", usuarioLogin.Senha);

            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "UsuarioAutenticacao");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                usuarioAutenticado.CPF = Convert.ToString(dataRow["CPF"]);
                usuarioAutenticado.Nome = Convert.ToString(dataRow["Nome"]);
                usuarioAutenticado.PerfilAcesso = Convert.ToString(dataRow["PerfilAcesso"]);
                usuarioAutenticado.Unidade = Convert.ToString(dataRow["Unidade"]);
            }

            return usuarioAutenticado;
        }
        public UsuarioModuloAcessoLista UsuarioModulos(string CPF)
        {
            var usuarioModuloAcessoLista = new UsuarioModuloAcessoLista();

            sql_AcessoBancoDados.LimparParametros();

            sql_AcessoBancoDados.AdicionarParametro("varCPF", CPF);

            DataTable dataTable = sql_AcessoBancoDados.Consultar(CommandType.StoredProcedure, "UsuarioModulos");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                var usuarioModuloAcesso = new UsuarioModuloAcesso();
                usuarioModuloAcesso.Modulo = Convert.ToString(dataRow["Modulo"]);
                usuarioModuloAcessoLista.Add(usuarioModuloAcesso);
            }

            return usuarioModuloAcessoLista;
        }

    }
}
