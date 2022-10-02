using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.LifeEngine
{
    public class SurfaceEditor
    {
        public SurfaceType CurrentSurfase { get; private set; }
        public SurfaceEditor(SurfaceType currentSurfase)
        {
            CurrentSurfase = currentSurfase;
        }
        public void ChooseSurface(SurfaceType surface)
        {
            CurrentSurfase = surface;
        }
    }
}
