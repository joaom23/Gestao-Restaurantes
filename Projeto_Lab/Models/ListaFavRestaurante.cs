using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Projeto_Lab.Models
{
    [Table("Lista_Fav_Restaurantes")]
    public partial class ListaFavRestaurante
    {
      
        [Column("Id_c")]
        public int IdC { get; set; }
        [Key]
        [Column("Id_r")]
        public int IdR { get; set; }
        public bool? Notifica { get; set; }

        [ForeignKey(nameof(IdC))]
        [InverseProperty(nameof(Cliente.ListaFavRestaurantes))]
        public virtual Cliente IdCNavigation { get; set; }
        [ForeignKey(nameof(IdR))]
        [InverseProperty(nameof(Restaurante.ListaFavRestaurantes))]
        public virtual Restaurante IdRNavigation { get; set; }
    }
}
