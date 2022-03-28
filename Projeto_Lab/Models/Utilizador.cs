using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Projeto_Lab.Models
{
    [Table("Utilizador")]
    public partial class Utilizador
    {
        public Utilizador()
        {
            Suspensos = new HashSet<Suspenso>();
        }

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(20)]
        public string Pass { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(20)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(50)]
        public string Email { get; set; }
        public int Estado { get; set; }

        [InverseProperty("IdANavigation")]
        public virtual Administrador Administrador { get; set; }
        [InverseProperty("IdCNavigation")]
        public virtual Cliente Cliente { get; set; }
        [InverseProperty("IdRNavigation")]
        public virtual Restaurante Restaurante { get; set; }
        [InverseProperty(nameof(Suspenso.IdUNavigation))]
        public virtual ICollection<Suspenso> Suspensos { get; set; }

    }
}
