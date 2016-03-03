using Microsoft.Data.Entity;
using Microsoft.Extensions.OptionsModel;
using MIT.CRM.Models;
using MIT.CRM.Models.Helper;
using MIT.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MIT.CRM.Services
{
    public class AppServices
    {
        private readonly ApplicationDbContext _context;
        private IOptions<AppSettings> _config;

        public AppServices(ApplicationDbContext context, IOptions<AppSettings> config)
        {
            _config = config;
            _context = context;
        }

        public async Task<string> getServerUrlSignalR ()
        {
            return await Task.FromResult(_config.Value.signalRConfig.server_Url);
        }

        public async Task<string> getJavascriptUrlSignalR()
        {
            return await Task.FromResult(_config.Value.signalRConfig.javaScript_Url);
        }

        #region Funcionario Services

        public async Task<Int32> getferias_item(int funcionario, int ano, string [] estado)
        {
            var fe = _context.Ferias_Itens.Where(f => f.funcionarioId == funcionario && f.ano == ano && estado.Contains(f.estado));

            if (fe.Count() > 0)
                return await Task.FromResult(fe.Count());
            else
                return await Task.FromResult(0);
        }
        #endregion

        #region User Services
        public async Task<String> getCurrentDisplayUserName(string userName)
        {
            ApplicationUser user = _context.Users.Include(f => f.funcionario).Single(c => c.UserName == userName);

            //Funcionario funcionario = _context.Funcionarios.Include(f => f.departamento).Single(f => f.id == id);

            return await Task.FromResult(user.funcionario.nomeAbreviado);
        }

        public async Task<String> getCurrentCargo(string userName)
        {
            ApplicationUser user = _context.Users.Include(f => f.funcionario).Single(c => c.UserName == userName);

            //Funcionario funcionario = _context.Funcionarios.Include(f => f.departamento).Single(f => f.id == id);

            return await Task.FromResult(user.funcionario.cargo);
        }

        public async Task<String> getCurrentAvatar(string userName)
        {
            ApplicationUser user = _context.Users.Include(f => f.funcionario).Single(c => c.UserName == userName);

            try
            {
                user.funcionario.avatar = _context.FileDescription.Single(f => f.Id == user.funcionario.avatarId);
                var uploads= "/uploads/funcionarios/"+ user.funcionario.codigo+"/"+user.funcionario.avatar.FileName;
                //uploads = Path.Combine(uploads, "funcionarios");
                //uploads = Path.Combine(uploads, user.funcionario.codigo);

                return await Task.FromResult(uploads);
            }
            catch
            {
                var uploads = "/images/default_avatar.jpg";
                return await Task.FromResult(uploads);
            }
            
        }
        #endregion
    }
}
