using GasStation.GraphicEngine.Common;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GasStation.LifeEngine
{
    public class SurfaceProvider
    {
        public Dictionary<SurfaceType, Surface> Surfaces { get; private set; }
        public SurfaceProvider()
        {
            Surfaces = new Dictionary<SurfaceType, Surface>()
            {
                { SurfaceType.Earth, new Surface(SurfaceType.Earth, new ViewComponent(Color.Yellow)) },
                { SurfaceType.GasStation, new Surface(SurfaceType.GasStation, new ViewComponent(Color.Red, new Bitmap("C:\\Users\\NIKITA\\OneDrive\\Рабочий стол\\00_B9AkuG0.jpg.740x555_q85_box-314,0,1918,1200_crop_detail_upscale.jpg"))) },
                { SurfaceType.ServiceGasStation, new Surface(SurfaceType.ServiceGasStation, new ViewComponent(Color.Blue)) }
            };
        }
    }
}
