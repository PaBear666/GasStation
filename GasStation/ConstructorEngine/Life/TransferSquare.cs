using Newtonsoft.Json;

namespace GasStation.ConstructorEngine.Life
{
    [JsonObject]
    public class TransferSquare
    {
        public int Id
        {
            get; set;
        }

        public Surface Surface
        {
            get; set;
        }


        public LifeAppliance LifeAppliance
        {
            get; set;
        }

        public TransferSquare()
        {

        }

        public TransferSquare(int id, Surface surface, LifeAppliance lifeAppliance)
        {
            Id = id;
            Surface = surface;
            LifeAppliance = lifeAppliance;
        }
    }
}
