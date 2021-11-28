using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DTO
{
    public class EventoData
    {
        public int IdData { get; set; }   
        public int IdEvento { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "FORMATO DATA = dd/mm/aaaa")]
        public string DataEvento { get; set; }


        [Required(ErrorMessage = "OBRIGATÓRIO")]
        public string Reservado { get; set; }
    }
}
