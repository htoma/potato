using System;
using Newtonsoft.Json;

namespace Azure.Models
{
    public class Model
    {
        public Model()
        {
            Id = Guid.NewGuid();
        }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set;  }
    }
}