using GasStation.GraphicEngine.Common;
using Newtonsoft.Json;

namespace GasStation.LifeEngine
{
    [JsonObject]
    public class TopologyTransfer<T>
        where T: Square
    {
        public string Name { get; set; }
        public int WidthLength { get; set; }
        public int HeightLength { get; set; }
        public T[] Squares { get; set; }
    }
}
