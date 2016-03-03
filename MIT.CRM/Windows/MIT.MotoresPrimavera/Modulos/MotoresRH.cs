using Interop.ErpBS900;
using Interop.RhpBE900;
using MIT.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIT.MotoresPrimavera.Modulos
{
    public class MotoresRH
    {
        private ErpBS _erpBs;

        public MotoresRH(ErpBS erpBS)
        {
            _erpBs = erpBS;
        }
        
        public List<Funcionario> daListaFuncionarios(string resticoes="")
        {
            string sql = "";

            sql = "select f.Codigo, f.Nome , f.Email, isnull(f.Telemovel,'') as Telemovel,isnull(f.Telefone,'') as Telefone, d.Descricao as Departamento from funcionarios f ";
            sql += "inner join Situacoes s on s.Situacao = f.Situacao ";
            sql += "left join Departamentos d on d.Departamento = f.CodDepartamento ";
            sql += "where s.Tipo = 0";

            if (resticoes != "")
                sql += " and " + resticoes; 

            List<Funcionario> listFuncionario = new List<Funcionario>();
            

            var objLista=_erpBs.Consulta (sql);

            while (!(objLista.NoInicio() || objLista.NoFim()))
            {
                Funcionario funcionario = new Funcionario()
                {
                    codigo = (string)objLista.Valor("Codigo"),
                    nome = (string)objLista.Valor("Nome"),
                    departamentoId = (string)objLista.Valor("Departamento"),
                    email = (string)objLista.Valor("Email"),
                    telemovel = (string)objLista.Valor("Telemovel"),
                    telefone = (string)objLista.Valor("Telefone")

                };

                listFuncionario.Add(funcionario);

                objLista.Seguinte();
            }

            return listFuncionario;

        }

        public Funcionario daFuncionario(string codigo)
        {
            Funcionario funcionario;

            if (_erpBs.RecursosHumanos.Funcionarios.Existe(codigo))
            {
                var func = _erpBs.RecursosHumanos.Funcionarios.Edita(codigo);
                funcionario = new Funcionario()
                {
                    codigo = func.get_Funcionario(),
                    nome = func.get_Nome(),
                    email = func.get_Email()
                };
            }
            else
            {
                funcionario = new Funcionario();
            }

            return funcionario;

        }

        public FuncInfFerias daInfFeriasFuncionario(string codigo, short ano)
        {
            FuncInfFerias funcInfFerias;

            if (_erpBs.RecursosHumanos.FuncInfFerias.Existe(ano, codigo))
            {
                var infFerias = _erpBs.RecursosHumanos.FuncInfFerias.Edita(ano, codigo);
                funcInfFerias = new FuncInfFerias
                {
                    ano = infFerias.get_Ano(),
                    funcionarioId = infFerias.get_Funcionario(),
                    diasDireito = infFerias.get_DiasDireito(),
                    diasAdicionais = infFerias.get_DiasAdicionais(),
                    diasAnoAnterior = infFerias.get_DiasAnoAnterior(),
                    totalDias =infFerias.get_TotalDias(),
                    diasPorGozar = infFerias.get_DiasPorGozar(),
                    diasJaGozados = infFerias.get_DiasJaGozados(),
                    diasPorMarcar = infFerias.get_DiasPorMarcar(),
                    funcSemFerias = infFerias.get_FuncSemFerias()
                };
            }
            else
            {
                funcInfFerias = new FuncInfFerias();
            }

            return funcInfFerias;

        }

        public List<Departamento> daListaDepartamentos(string resticoes = "")
        {
            List<Departamento> list = new List<Departamento>();

            string sql = "";

            sql = "select * from Departamentos ";
            
            var objLista = _erpBs.Consulta(sql);

            while (!(objLista.NoInicio() || objLista.NoFim()))
            {
                Departamento dep = new Departamento()
                {
                    departamento = (string)objLista.Valor("Departamento"),
                    descricao = (string)objLista.Valor("Descricao"),
                };

                list.Add(dep);

                objLista.Seguinte();
            }

            return list;
        }

        public void fazMacacaoFerias(Ferias_Itens _feria)
        {
            RhpBEFeria feria = new RhpBEFeria();

            feria.set_Ano(_feria.ano);
            feria.set_DataFeria(_feria.dataFeria);
            feria.set_EstadoGozo(_feria.estadoGozo);
            feria.set_Funcionario(_feria.funcionario_Codigo);
            feria.set_TipoMarcacao(1);
            _erpBs.RecursosHumanos.Ferias.Actualiza(feria);

            
        }

    }
}
