using System.ComponentModel.DataAnnotations;

namespace b3.api.DTO.Model
{
    public class TarefaDto
    {
        [Key]
        public int? id { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Descricao { get; set; }
        public string responsavel { get; set; }

        [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$", ErrorMessage = "Número de telefone inválido")]

        public string celular { get; set; }

        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido")]
        public string cpf { get; set; }
        public bool status { get; set; }
        public DateTime data { get; set; }
    }
}
