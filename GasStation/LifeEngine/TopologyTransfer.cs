using GasStation.GraphicEngine.Common;
using GasStation.LifeEngine.Life;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GasStation.LifeEngine
{
    [JsonObject]
    public class TopologyTransfer
    {
        public string Name { get; set; }
        public int WidthLength { get; set; }
        public int HeightLength { get; set; }
        public IEnumerable<TransferSquare> Squares { get; set; }
    }
}
