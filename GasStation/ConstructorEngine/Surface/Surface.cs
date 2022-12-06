using GasStation.GraphicEngine.Common;
using Newtonsoft.Json;
using System.Drawing;

namespace GasStation.ConstructorEngine
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Surface : LifeComponent
    {
        [JsonProperty]
        public SurfaceType Type { get; set; }

        public Surface()
        {

        }

        public Surface(SurfaceType type, ViewComponent viewComponent) : base(viewComponent)
        {
            Type = type;
        }
    }
}