using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DTO
{
    public class Banner
    {
        public int IdBanner { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string Nome { get; set; }
        
        public byte[] ImagemByte { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string base64imagem { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string Estatus { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string Unidade { get; set; }
    }
}
