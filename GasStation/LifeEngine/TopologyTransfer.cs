using GasStation.GraphicEngine.Common;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GasStation.LifeEngine
{
    [JsonObject]
    public class TopologyTransfer<S>
        where S: Square
    {
        public string Name { get; set; }
        public int WidthLength { get; set; }
        public int HeightLength { get; set; }
        public IEnumerable<S> Squares { get; set; }
    }
}
