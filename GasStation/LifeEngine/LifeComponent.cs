using GasStation.GraphicEngine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.LifeEngine
{
    public abstract class LifeComponent
    {
        public ViewComponent ViewComponent { get; set; }
        public LifeComponent(ViewComponent viewComponent)
        {
            ViewComponent = viewComponent;
        }
    }
}
