﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MIT.CRM.Models;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MIT.CRM.Controllers
{
    public class DepartamentoController : Controller
    {
        [FromServices]
        public ApplicationDbContext _applicationDbContext { get; set; }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Create(string empresaId,string departamento, string descricao)
        {
            Departamento dep = new Departamento
            {
                departamento = departamento,
                descricao = descricao,
                empresaId = empresaId
            };
            try
            {
                var temp = _applicationDbContext.Departamentos.Where(d => d.departamento == departamento && d.empresaId == empresaId);
                if (temp.Count() == 0)
                {
                    _applicationDbContext.Departamentos.Add(dep);
                    _applicationDbContext.SaveChanges();

                    return "ok";
                }
                else
                {
                    return "null";
                }
            }
            catch (Exception e)
            {
                return "error " + e.Message;
            }
            
            
        }
    }
}