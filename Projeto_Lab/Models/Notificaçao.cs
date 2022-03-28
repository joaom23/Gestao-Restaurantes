using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Lab.Models
{
    [Table("Notificaçao")]
    public class Notificaçao
    {
        [Key]
        public int IdNoti { get; set; }
        [Column("Id_c")]
        public int IdC { get; set; }
  
        [Column("Id_r")]
        public int IdR { get; set; }

        [Required]
        public string Mensagem { get; set; }

        [Required]
        public DateTime Data { get; set; }

        public bool isRead { get; set; }

        [ForeignKey(nameof(IdC))]
        [InverseProperty(nameof(Cliente.Notificaçaos))]
        public virtual Cliente IdCNavigation { get; set; }
        [ForeignKey(nameof(IdR))]
        [InverseProperty(nameof(Restaurante.Notificaçaos))]
        public virtual Restaurante IdRNavigation { get; set; }
    }
}
