using System.Collections.Generic;
using Newtonsoft.Json;

namespace Azure.Models
{
    public class User : Model
    {
        public User()
        {
            Teams = new List<Team>();
        }

        public string Name { get; set; }

        public List<Team> Teams { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
