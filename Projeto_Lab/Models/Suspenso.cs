using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Projeto_Lab.Models
{
    [Table("Suspenso")]
    public partial class Suspenso
    {
        [Column("Data_bloqueio", TypeName = "date")]
        public DateTime? DataBloqueio { get; set; }
        [Column("Data_desbloqueio", TypeName = "date")]
        public DateTime? DataDesbloqueio { get; set; }
        [Required]
        [StringLength(50)]
        public string Motivo { get; set; }
        [Key]
        [Column("Id_U")]
        public int IdU { get; set; }
    
        [Column("Id_Adm")]
        public int IdAdm { get; set; }

        [ForeignKey(nameof(IdAdm))]
        [InverseProperty(nameof(Administrador.Suspensos))]
        public virtual Administrador IdAdmNavigation { get; set; }
        [ForeignKey(nameof(IdU))]
        [InverseProperty(nameof(Utilizador.Suspensos))]
        public virtual Utilizador IdUNavigation { get; set; }
    }
}
