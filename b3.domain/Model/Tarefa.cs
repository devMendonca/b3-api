using System.ComponentModel.DataAnnotations;

namespace b3_domain.Model
{
    public class Tarefa
    {


        [Key]
        public int? id { get; set; }
        public string descricao { get; set; }
        public string responsavel { get; set; }
        public string celular { get; set; }
        public string cpf { get; set; }
        public bool status { get; set; }
        public DateTime data { get; set; }
    }


}
