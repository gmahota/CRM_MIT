using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MIT.Repository
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser, IDisposable
    {
        public virtual Departamento departamento { get; set; }
        public virtual Funcionario funcionario { get; set; }
        
        public ICollection<Connection> Connections { get; set; }

        //public ICollection<Departamento> listDepartamentos { get; set; }

        public void Dispose()
        {
            
        }
    }

    public class Connection
    {
        public string ConnectionID { get; set; }
        public string UserAgent { get; set; }
        public bool Connected { get; set; }
    }
}
