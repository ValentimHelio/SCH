using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCH.Models
{
    [Table("movimento")]
    public class Movimento
    {
        [Key]
        [Display(Name = "Código")]
        public int MovimentoId { get; set; }

        [Required(ErrorMessage = "O Cliente deve ser informado")]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        [Display(Name = "Cliente")]
        public virtual Cliente cliente { get; set; }

        [Required(ErrorMessage = "O Serviço deve ser informado")]
        [Display(Name = "Serviço")]
        public int ServicoId { get; set; }
        [Display(Name = "Serviço")]
        public virtual Servico servico{ get; set; }

        [Display(Name = "Data")]
        public DateTime Data {  get; set; }

        [Required(ErrorMessage = "O Valor deve ser informado")]
        [Display(Name = "Valor")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor_Hora { get; set; }


    }
}
