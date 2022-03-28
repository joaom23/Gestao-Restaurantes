using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Projeto_Lab.Models
{
    [Table("Prato_Dia")]
    public partial class PratoDium
    {
        [Key]
        [Column("Id_Pd")]
        public int IdPd { get; set; }
     
        [Column("Id_R")]
        public int IdR { get; set; }
        public int Preco { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(20)]
        public string Tipo { get; set; }

        //[Required]
        //[StringLength(20)]
        //public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(100)]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [StringLength(100, ErrorMessage = ("Este campo necessita de ter no minimo 5 carateres"), MinimumLength = 5)]
        public string Foto { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório!", AllowEmptyStrings = false)]
        [Column("Data_Prato", TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DataPrato { get; set; }

        [ForeignKey(nameof(IdPd))]
        [InverseProperty(nameof(Restaurante.PratoDia))]
        public virtual Restaurante IdPd1 { get; set; }
        [ForeignKey(nameof(IdPd))]
        [InverseProperty(nameof(Prato.PratoDia))]
        public virtual Prato IdPdNavigation { get; set; }
    }
}
