using GasStation.GraphicEngine.Common;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GasStation.LifeEngine
{
    public class EditorProvider
    {
        public Dictionary<SurfaceType, Surface> Surfaces { get; private set; }
        public Dictionary<ApplianceType, Appliance> Appliance { get; private set; }
        public EditorProvider()
        {
            Surfaces = new Dictionary<SurfaceType, Surface>()
            {
                { SurfaceType.Road, new Surface(SurfaceType.Road, new ViewComponent(Control.DefaultBackColor, Resource.road))},
                { SurfaceType.GasStation, new Surface(SurfaceType.GasStation, new ViewComponent(Control.DefaultBackColor, Resource.gasStation_2))},
                { SurfaceType.Service, new Surface(SurfaceType.Service, new ViewComponent(Control.DefaultBackColor, Resource.gasStationService)) }
            };

            Appliance = new Dictionary<ApplianceType, Appliance>()
            {
                { ApplianceType.Shop, new Appliance(ApplianceType.Shop, Side.Right, new ViewComponent(Control.DefaultBackColor, Resource.shop)) },
                { ApplianceType.GasStation, new Appliance(ApplianceType.GasStation, Side.Right, new ViewComponent(Control.DefaultBackColor, Resource.fuelgiver)) },
                { ApplianceType.Tanker, new Appliance(ApplianceType.Tanker, Side.Right, new ViewComponent(Control.DefaultBackColor, Resource.tanker)) },
                { ApplianceType.OutBridge, new Appliance(ApplianceType.OutBridge, Side.Top, new ViewComponent(Control.DefaultBackColor, Resource.outer)) },
                { ApplianceType.InBridge, new Appliance(ApplianceType.InBridge, Side.Bottom, new ViewComponent(Control.DefaultBackColor, Resource.entry)) },
            };
        }
    }
}
