using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Projeto_Lab.Models
{
    [Table("Cliente")]
    public partial class Cliente
    {
        public Cliente()
        {
            ListaFavPratos = new HashSet<ListaFavPrato>();
            ListaFavRestaurantes = new HashSet<ListaFavRestaurante>();
        }

        [Key]
        [Column("Id_C")]
        public int IdC { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(20)]
        public string Nome { get; set; }
        [Column("Suspenso_por")]
        public int? SuspensoPor { get; set; }

        [ForeignKey(nameof(IdC))]
        [InverseProperty(nameof(Utilizador.Cliente))]
        public virtual Utilizador IdCNavigation { get; set; }
        [ForeignKey(nameof(SuspensoPor))]
        [InverseProperty(nameof(Administrador.Clientes))]
        public virtual Administrador SuspensoPorNavigation { get; set; }
        [InverseProperty(nameof(ListaFavPrato.IdClNavigation))]
        public virtual ICollection<ListaFavPrato> ListaFavPratos { get; set; }
        [InverseProperty(nameof(ListaFavRestaurante.IdCNavigation))]
        public virtual ICollection<ListaFavRestaurante> ListaFavRestaurantes { get; set; }

        [InverseProperty(nameof(Notificaçao.IdCNavigation))]
        public virtual ICollection<Notificaçao> Notificaçaos { get; set; }
    }
}
