using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MIT.Data
{
    public class Entity
    {
        public int id { get; set; }
        public string type { get; set; }
        public string entidade { get; set; }
        public string nome { get; set; }
        public string fac_Mor { get; set; }
        public string fac_Local { get; set; }
        public string numContrib { get; set; }
        public string pais { get; set; }
        public string fac_Tel { get; set; }
        public string moeda { get; set; }
        public bool enviaCobranca { get; set; }
        public double valorPendente { get; set; }
        public double valorDebitoTotal { get; set; }
        public double valorCreditoTotal { get; set; }
        
    }

    public class Contact
    {
        public int id { get; set; }
        public string name { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string title { get; set; }
        public string email { get; set; }
        public string emailAlt { get; set; }
        public string type { get; set; }
        public string celphone { get; set; }
        public string telphone { get; set; }

        public List<Contact_Item> contact_Itens { get; set; }

    }

    public class Contact_Item
    {
        public int id { get; set; }

        public string contactId { get; set; }

        public string type { get; set; }
        public string name { get; set; }
        public string value { get; set; }

        [ForeignKey("contactId")]
        public Contact contact { get; set; }
    }

    public class Contact_Entity
    {
        public int id { get; set; }

        public string type { get; set; }
        public string name { get; set; }
        public string value { get; set; }


    }
    
    public class Report
    {
        public int id { get; set; }
        public string empresa { get; set; }
        public string nomeEmpresa { get; set; }
        public string tipoEntidade { get; set; }
        public string entidade { get; set; }
        public string caminho { get; set; }
        public string to { get; set; }
        public string cc { get; set; }

    }
}
