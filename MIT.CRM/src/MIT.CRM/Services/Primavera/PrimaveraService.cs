using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MIT.Data;

namespace MIT.CRM.Services.Primavera
{
    public class PrimaveraService : IPrimavera
    {
        public Task<bool> fazMarcacaoFerias(int tipoPlataforma, string codEmpresa, string codUtilizador, string password, string funcionario_Codigo, short ano, DateTime dataFeria)
        {
            throw new NotImplementedException();
        }

        public Task<bool> fazMarcacaoFeriasColecao(int tipoPlataforma, string codEmpresa, string codUtilizador, string password, List<Ferias_Itens> listFerias)
        {
            throw new NotImplementedException();
        }

        public Task<FuncInfFerias> funcionario_Informacao_Ferias(int tipoPlataforma, string codEmpresa, string codUtilizador, string password, string codigo, short ano)
        {
            throw new NotImplementedException();
        }
    }
}
