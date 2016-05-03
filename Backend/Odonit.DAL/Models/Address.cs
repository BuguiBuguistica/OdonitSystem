using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.DAL.Models
{
    public class Address
    {
        public Address()
        {
            People = new HashSet<Person>();
        }
        public int Id { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public int Number { get; set; }
        [JsonIgnore]
        public virtual ICollection<Person> People { get; private set; }
    }
}
