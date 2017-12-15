using System.Collections.Generic;
using Newtonsoft.Json;

namespace Azure.Models
{
    public class Team : Model
    {
        public Team()
        {
            Players = new List<Player>();
        }

        public string Name { get; set; }
        public List<Player> Players { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}