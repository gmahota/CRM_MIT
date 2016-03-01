using MIT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIT.CRM.Services.Primavera
{
    public interface IPrimavera
    {
        Task <FuncInfFerias> funcionario_Informacao_Ferias(int tipoPlataforma, string codEmpresa, string codUtilizador,
            string password, string codigo, short ano);

        Task<bool> fazMarcacaoFerias(int tipoPlataforma, string codEmpresa, string codUtilizador,
            string password, string funcionario_Codigo, short ano, DateTime dataFeria);

        Task<bool> fazMarcacaoFeriasColecao(int tipoPlataforma, string codEmpresa, string codUtilizador,
            string password, List<Ferias_Itens> listFerias);
    }
}
