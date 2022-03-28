using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Projeto_Lab.Models
{
    [Table("Restaurante")]
    public partial class Restaurante
    {
        public Restaurante()
        {
            ListaFavRestaurantes = new HashSet<ListaFavRestaurante>();
            PratoDia = new HashSet<PratoDium>();
            RestTipos = new HashSet<RestTipo>();
        }

        [Key]
        [Column("Id_R")]
        public int IdR { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(9, ErrorMessage = ("Insira um número válido."), MinimumLength = 9)]
        public int Telefone { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(100)]
        public string Foto { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(100, ErrorMessage = ("Este campo necessita de ter no minimo 10 carateres"), MinimumLength = 10)]
        public string Localizacao { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        public TimeSpan HoraAbertura { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        public TimeSpan HoraFecho { get; set; }

        [Column("Dia_Desc")]
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        public string DiaDesc { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [Column("Tipo_Servico")]
        [StringLength(50)]
        public string TipoServico { get; set; }
        [Column("Registado_por")]
        public int? RegistadoPor { get; set; }

        [ForeignKey(nameof(IdR))]
        [InverseProperty(nameof(Utilizador.Restaurante))]
        public virtual Utilizador IdRNavigation { get; set; }
        [ForeignKey(nameof(RegistadoPor))]
        [InverseProperty(nameof(Administrador.Restaurantes))]
        public virtual Administrador RegistadoPorNavigation { get; set; }
        [InverseProperty(nameof(ListaFavRestaurante.IdRNavigation))]
        public virtual ICollection<ListaFavRestaurante> ListaFavRestaurantes { get; set; }
        [InverseProperty(nameof(PratoDium.IdPd1))]
        public virtual ICollection<PratoDium> PratoDia { get; set; }
        [InverseProperty(nameof(RestTipo.IdRNavigation))]
        public virtual ICollection<RestTipo> RestTipos { get; set; }

        [InverseProperty(nameof(Notificaçao.IdRNavigation))]
        public virtual ICollection<Notificaçao> Notificaçaos { get; set; }

       // public TipoServico TipoServico { get; set; }

    }
}
