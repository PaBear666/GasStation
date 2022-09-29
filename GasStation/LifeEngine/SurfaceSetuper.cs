using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.LifeEngine
{
    public class SurfaceSetuper
    {
        public SurfaceType CurrentSurfase { get; private set; }
        public SurfaceSetuper(SurfaceType currentSurfase)
        {
            CurrentSurfase = currentSurfase;
        }
        public void ChooseSurface(SurfaceType surface)
        {
            CurrentSurfase = surface;
        }
    }
}
