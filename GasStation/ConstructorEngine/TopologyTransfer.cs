using GasStation.GraphicEngine.Common;
using GasStation.ConstructorEngine.Life;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GasStation.ConstructorEngine
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
