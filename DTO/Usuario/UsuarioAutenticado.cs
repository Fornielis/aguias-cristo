using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UsuarioAutenticado
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string PerfilAcesso { get; set; }
        public string Unidade { get; set; }
        public UsuarioModuloAcessoLista ModulosAcesso { get; set; }
    }
}
