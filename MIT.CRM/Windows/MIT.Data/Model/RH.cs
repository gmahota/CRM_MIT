using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MIT.Data.Model
{
    public class Funcionario
    {
        [Key]
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

        public string email { get; set; }

        public string telemovel { get; set; }
        public string telefoneAlternativo { get; set; }

        public string departamentoId { get; set; }


        public string empresaId { get; set; }

        public string utilizadorId { get; set; }

        public virtual Departamento departamento { get; set; }

        public virtual Empresa empresa { get; set; }

        public List<FuncInfFerias> funcInfFerias { get; set; }

        

    }

    public class FuncInfFerias
    {
        public int id { get; set; }

        public short ano { get; set; }

        public string funcionarioId { get; set; }

        public double diasDireito { get; set; }

        public double diasAdicionais { get; set; }

        public double diasAnoAnterior { get; set; }

        public double totalDias { get; set; }

        public double diasPorGozar { get; set; }

        public double diasJaGozados { get; set; }

        public double diasPorMarcar { get; set; }

        public double diasFeriasJaPagas { get; set; }

        public bool funcSemFerias { get; set; }

        public virtual Funcionario funcionario { get; set; }
    }

    public class FuncFerias
    {
        public int id { get; set; }

        public short ano { get; set; }


        public string funcionarioId { get; set; }

        [Required]
        public DateTime dataFeria { get; set; }
        public bool estadoGozo { get; set; }
        public bool originouFalta { get; set; }
        public bool tipoMarcacao { get; set; }
        public bool originouFaltaSubAlim { get; set; }

        public virtual Funcionario funcionario { get; set; }
    }

    public class Departamento
    {
        public string Id { get; set; }

        public string departamento { get; set; }

        public string empresaId { get; set; }

        public string descricao { get; set; }

        public string responsavelId { get; set; }
        
        public virtual Empresa empresa { get; set; }

        public virtual List<Funcionario> listaFuncionarios { get; set; }
    }

    public class Ferias
    {
        public int id { get; set; }
        public DateTime dataInicio { get; set; }
        public DateTime dataFim { get; set; }
        public int funcionarioId { get; set; }

        public virtual List<Ferias_Itens> itens { get; set; }

        public virtual Funcionario funcionario { get; set; }

    }

    public class Ferias_Itens
    {
        public int id { get; set; }

        public short ano { get; set; }

        public int funcionarioId { get; set; }

        public string funcionario_Codigo { get; set; }

        public int? feriasId { get; set; }

        public Ferias ferias { get; set; }

        [Required]
        public DateTime dataFeria { get; set; }
        public bool estadoGozo { get; set; }
        public bool originouFalta { get; set; }
        public int tipoMarcacao { get; set; }
        public bool originouFaltaSubAlim { get; set; }

        public virtual Funcionario funcionario { get; set; }

        public string daString_Data_Feria()
        {
            return dataFeria.ToString("dd/MM/yyyy");
        }
    }

}
