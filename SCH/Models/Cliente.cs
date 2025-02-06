using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCH.Models
{
    [Table("cliente")]
    public class Cliente
    {
        [Key]
        [Display(Name = "Código")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "O nome do cliente deve ser informado")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2} caracteres")]
        [Display(Name = "Nome do Cliente")]
        public string NomeCliente { get; set; }

        [Required(ErrorMessage = "Informe o endereço")]
        [StringLength(100)]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Informe a cidade")]
        [StringLength(100)]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Informe a sigla do estado")]
        [StringLength(2)]
        [Display(Name = "Estado")]
        public string Uf { get; set; }

        [Required(ErrorMessage = "Informe o CEP")]
        [StringLength(20)]
        [Display(Name = "Cep")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Informe o Telefone")]
        [StringLength(20)]
        [Display(Name = "Telefone")]
        public string Fone { get; set; }

        [Required(ErrorMessage = "Informe o Cpf")]
        [StringLength(14)]
        [Display(Name = "Cpf")]
        public string Cpf { get; set; }

    }
}
