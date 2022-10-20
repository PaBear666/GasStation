using GasStation.GraphicEngine.Common;
using System.Drawing;
using System.Linq;

namespace GasStation.LifeEngine
{
    public class SurfaceProvider
    {
        readonly Surface[] _viewComponents;
        public SurfaceProvider()
        {
            _viewComponents = new Surface[]{
                new Surface(SurfaceType.Earth, new ViewComponent(Color.Green)),
                new Surface(SurfaceType.GasStation, new ViewComponent(Color.Red, new Bitmap("C:\\Users\\NIKITA\\OneDrive\\Рабочий стол\\00_B9AkuG0.jpg.740x555_q85_box-314,0,1918,1200_crop_detail_upscale.jpg")))
            };
        }

        public Surface GetSurface(SurfaceType surfaceType)
        {
            return _viewComponents.Single(x => x.Type == surfaceType);
        }
    }
}
