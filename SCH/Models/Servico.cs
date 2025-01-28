using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCH.Models
{
    [Table("servico")]
    public class Servico
    {
        [Key]
        [Display(Name = "Código")]
        public int ServicoId { get; set; }

        [Required(ErrorMessage = "A Descrição deve ser informado")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2} caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A Descrição deve ser informado")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2} caracteres")]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "A Empresa deve ser informado")]
        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }
        public virtual Empresa empresa { get; set; }
    }
}
