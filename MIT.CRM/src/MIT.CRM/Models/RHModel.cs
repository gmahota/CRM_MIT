using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIT.CRM.Models
{
    public class Funcionario
    {
        
        public string codigo { get; set; }

        public string nome { get; set; }

        public string localidade { get; set; }

        public string naturalidade { get; set; }

        public string distrito { get; set; }

        public string nacionalidade { get; set; }

        public string telefone { get; set; }

        public string estadoCivil { get; set; }

        public string sexo { get; set; }

        public DateTime? dataNascimento { get; set; }

        public DateTime? dataAdmissao { get; set; }

        public DateTime? dataReadmissao { get; set; }

        public DateTime? dataFimContrato { get; set; }

        public string categoria { get; set; }

        public string profissao { get; set; }

        public string classificacao { get; set; }

        public DateTime? dataClassificacao { get; set; }

        public string habilitacao { get; set; }

    }
}
