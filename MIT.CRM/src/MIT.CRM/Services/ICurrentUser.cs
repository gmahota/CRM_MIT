using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIT.CRM.Services
{
    interface ICurrentUser
    {
        Task User { get; set; }
    }
}
