using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Contenedores.Models.ViewModels
{
    public class FileViewModel
    {
        
        [DisplayName("imagen de un contenedor")]
        public HttpPostedFileBase File { get; set; }
        
        [Required]
        [DisplayName("Limpio, Sucio, Muy Sucio.")]
        public int IsClean { get; set; }

        [Required]
        [DisplayName("Abierto, cerrado")]
        public int IsOpen { get; set; }

        [Required]
        [DisplayName("Esta roto, o sano")]
        public int IsBroken { get; set; }

        public String NombreArchivo { get; set; }
    }
}