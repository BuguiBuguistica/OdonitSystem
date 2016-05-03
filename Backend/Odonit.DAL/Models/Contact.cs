using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.DAL.Models
{
    
    public class Contact
    {        
        public Contact()
        {
            People = new HashSet<Person>();
        }
        public int ContactId { get; set; }
        public string LandPhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public virtual ICollection<Person> People { get; private set; }
    }
}
