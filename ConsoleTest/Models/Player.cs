using Newtonsoft.Json;

namespace Azure.Models
{
    public class Player : Model
    {                
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Stamina { get; set; }
        public int Creativity { get; set; }

        // todo: add Boost
        // todo: add Captain

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}