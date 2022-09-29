using System.Linq;

namespace GasStation.LifeEngine
{
    public class SurfaceProvider
    {
        readonly Surface[] _viewComponents;
        public SurfaceProvider(Surface[] viewComponents)
        {
            _viewComponents = viewComponents;
        }

        public Surface GetSurface(SurfaceType surfaceType)
        {
            return _viewComponents.Single(x => x.Type == surfaceType);
        }
    }
}
