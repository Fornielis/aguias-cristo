﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace WEB.Metodos
{
    public class FTP
    {
        public void EnviarArquivoFTP(string arquivo)
        {
            string url = "ftp://ftp.mcaguiassp.com.br/wwwroot/public/banner";
            string usuario = "aguiascristo";
            string senha = "#McAc@0002~*";

            FileInfo arquivoInfo = new FileInfo(arquivo);
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(usuario, senha);
            request.UseBinary = true;
            request.ContentLength = arquivoInfo.Length;
            using (FileStream fs = arquivoInfo.OpenRead())
            {
                byte[] buffer = new byte[2048];
                int bytesSent = 0;
                int bytes = 0;
                using (Stream stream = request.GetRequestStream())
                {
                    while (bytesSent < arquivoInfo.Length)
                    {
                        bytes = fs.Read(buffer, 0, buffer.Length);
                        stream.Write(buffer, 0, bytes);
                        bytesSent += bytes;
                    }
                }
            }
        }
    }
}