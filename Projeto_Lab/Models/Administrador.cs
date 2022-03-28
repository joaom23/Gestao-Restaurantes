using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Projeto_Lab.Models
{
    [Table("Administrador")]
    public partial class Administrador
    {
        public Administrador()
        {
            Clientes = new HashSet<Cliente>();
            InverseCriadoPorNavigation = new HashSet<Administrador>();
            Restaurantes = new HashSet<Restaurante>();
            Suspensos = new HashSet<Suspenso>();
        }

        [Key]
        [Column("Id_A")]
        public int IdA { get; set; }
        [Column("criado_por")]
        public int? CriadoPor { get; set; }

        [ForeignKey(nameof(CriadoPor))]
        [InverseProperty(nameof(Administrador.InverseCriadoPorNavigation))]
        public virtual Administrador CriadoPorNavigation { get; set; }
        [ForeignKey(nameof(IdA))]
        [InverseProperty(nameof(Utilizador.Administrador))]
        public virtual Utilizador IdANavigation { get; set; }
        [InverseProperty(nameof(Cliente.SuspensoPorNavigation))]
        public virtual ICollection<Cliente> Clientes { get; set; }
        [InverseProperty(nameof(Administrador.CriadoPorNavigation))]
        public virtual ICollection<Administrador> InverseCriadoPorNavigation { get; set; }
        [InverseProperty(nameof(Restaurante.RegistadoPorNavigation))]
        public virtual ICollection<Restaurante> Restaurantes { get; set; }
        [InverseProperty(nameof(Suspenso.IdAdmNavigation))]
        public virtual ICollection<Suspenso> Suspensos { get; set; }
    }
}
