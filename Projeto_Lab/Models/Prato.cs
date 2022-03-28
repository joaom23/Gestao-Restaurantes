using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Projeto_Lab.Models
{
    [Table("Prato")]
    public partial class Prato
    {
        public Prato()
        {
            ListaFavPratos = new HashSet<ListaFavPrato>();
            PratoDia = new HashSet<PratoDium>();
        }

        [Key]
        [Column("Id_P")]
        public int IdP { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(50)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(50)]
        public string Tipo { get; set; }

        [InverseProperty(nameof(ListaFavPrato.Prato))]
        public virtual ICollection<ListaFavPrato> ListaFavPratos { get; set; }
        [InverseProperty(nameof(PratoDium.IdPdNavigation))]
        public virtual ICollection<PratoDium> PratoDia { get; set; }
    }
}
