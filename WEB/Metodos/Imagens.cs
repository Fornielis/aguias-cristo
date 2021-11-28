using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace WEB.Metodos
{
    public class Imagens
    {
        public byte[] byteImage(string stringBase64)
        {
            return Encoding.UTF8.GetBytes(stringBase64);
        }
        public string stringBase64(byte[] byteImage)
        {
            return Encoding.UTF8.GetString(byteImage);
        }
        public Image ArrayParaImagem(object Arrray)
        {
            byte[] ArrayImagem = (byte[])Arrray;
            MemoryStream memoryStream = new MemoryStream(ArrayImagem);
            Image image = Image.FromStream(memoryStream);
            return image;
        }
    }
}