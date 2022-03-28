using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Projeto_Lab.Models
{
    [Table("Rest_Tipo")]
    public partial class RestTipo
    {
        [Key]
        [Column("Id_R")]
        public int IdR { get; set; }
       
        [Column("Id_Tp")]
        public int IdTp { get; set; }

        [ForeignKey(nameof(IdR))]
        [InverseProperty(nameof(Restaurante.RestTipos))]
        public virtual Restaurante IdRNavigation { get; set; }
        [ForeignKey(nameof(IdTp))]
        [InverseProperty(nameof(TipoServico.RestTipos))]
        public virtual TipoServico IdTpNavigation { get; set; }
    }
}
