using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Projeto_Lab.Models
{
    [Table("Tipo_Servico")]
    public partial class TipoServico
    {
        public TipoServico()
        {
            RestTipos = new HashSet<RestTipo>();
        }

        [Key]
        [Column("Id_Tp")]
        public int IdTp { get; set; }
        [Required]
        [StringLength(20)]
        public string Nome { get; set; }

        [InverseProperty(nameof(RestTipo.IdTpNavigation))]
        public virtual ICollection<RestTipo> RestTipos { get; set; }

        //public virtual ICollection<Restaurante> Restaurantes { get; set; }

    }
}
