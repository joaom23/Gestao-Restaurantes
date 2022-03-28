using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Projeto_Lab.Models
{
    [Table("Lista_Fav_Pratos")]
    public partial class ListaFavPrato
    {
   
        [Column("Id_Cl")]
        public int IdCl { get; set; }
       [Key]
        [Column("Prato_ID")]
        public int PratoId { get; set; }
        public bool? Notifica { get; set; }

        [ForeignKey(nameof(IdCl))]
        [InverseProperty(nameof(Cliente.ListaFavPratos))]
        public virtual Cliente IdClNavigation { get; set; }
        [ForeignKey(nameof(PratoId))]
        [InverseProperty("ListaFavPratos")]
        public virtual Prato Prato { get; set; }
    }
}
