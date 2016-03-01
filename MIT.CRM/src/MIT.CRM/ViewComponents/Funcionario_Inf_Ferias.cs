using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using MIT.CRM.Models;
using MIT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIT.CRM.ViewComponents
{
    public class Funcionario_Inf_Ferias: ViewComponent
    {
        private readonly ApplicationDbContext db;

        public Funcionario_Inf_Ferias(ApplicationDbContext context)
        {
            db = context;
        }

        public IViewComponentResult Invoke(int? id, int ano)
        {
            try
            {
                var ferias = db.FuncInfFerias.Include(m => m.ferias_Itens).Single(m => m.funcionarioId == id && m.ano == ano);

                return View(ferias);
            }
            catch
            {
                var ferias =new FuncInfFerias()
                {
                    
                };

                return View(ferias);
            }
            
        }
    }
}
