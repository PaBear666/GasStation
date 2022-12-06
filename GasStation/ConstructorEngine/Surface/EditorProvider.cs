using GasStation.GraphicEngine.Common;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GasStation.ConstructorEngine
{
    public class EditorProvider
    {
        public IDictionary<SurfaceType, Surface> Surfaces { get; private set; }
        public IDictionary<Appliance, LifeAppliance> Appliance { get; private set; }
        public IDictionary<ApplianceType, int> MaxAplianceOnMap { get; private set; }
        public EditorProvider()
        {
            Surfaces = new Dictionary<SurfaceType, Surface>()
                {
                    { SurfaceType.Road, new Surface(SurfaceType.Road, new ViewComponent(Control.DefaultBackColor, Resource.road))},
                    { SurfaceType.GasStation, new Surface(SurfaceType.GasStation, new ViewComponent(Control.DefaultBackColor, Resource.gasStation))},
                    { SurfaceType.Service, new Surface(SurfaceType.Service, new ViewComponent(Control.DefaultBackColor, Resource.gasStationService)) }
                };

            MaxAplianceOnMap = new Dictionary<ApplianceType, int>()
            {
                { ApplianceType.Bridge, 4 },
                { ApplianceType.GasStation, 10 },
                { ApplianceType.Shop, 1 },
                { ApplianceType.Tanker, 10 }
            };

            Appliance = new Dictionary<Appliance, LifeAppliance>();

            AddAppliance(ApplianceType.Shop,
                new ViewComponent(Control.DefaultBackColor, Resource.shop_top),
                new ViewComponent(Control.DefaultBackColor, Resource.shop_right),
                new ViewComponent(Control.DefaultBackColor, Resource.shop_bottom),
                new ViewComponent(Control.DefaultBackColor, Resource.shop_left));
            AddAppliance(ApplianceType.GasStation,
                new ViewComponent(Control.DefaultBackColor, Resource.gasstation_top),
                new ViewComponent(Control.DefaultBackColor, Resource.gasstation_right),
                new ViewComponent(Control.DefaultBackColor, Resource.gasstation_bottom),
                new ViewComponent(Control.DefaultBackColor, Resource.gasstation_left));
            AddAppliance(ApplianceType.Tanker,
                new ViewComponent(Control.DefaultBackColor, Resource.tanker_top),
                new ViewComponent(Control.DefaultBackColor, Resource.tanker_right),
                new ViewComponent(Control.DefaultBackColor, Resource.tanker_bottom),
                new ViewComponent(Control.DefaultBackColor, Resource.tanker_left));
            AddAppliance(ApplianceType.Bridge,
                new ViewComponent(Control.DefaultBackColor, Resource.arrow_top),
                new ViewComponent(Control.DefaultBackColor, Resource.arrow_right),
                new ViewComponent(Control.DefaultBackColor, Resource.arrow_bottom),
                new ViewComponent(Control.DefaultBackColor, Resource.arrow_left));
        }

        private void AddAppliance(ApplianceType type, ViewComponent top, ViewComponent right, ViewComponent bottom, ViewComponent left)
        {
            Appliance.Add(new Appliance(type, Side.Top), new LifeAppliance(new Appliance(type, Side.Top), top));
            Appliance.Add(new Appliance(type, Side.Bottom), new LifeAppliance(new Appliance(type, Side.Bottom), bottom));
            Appliance.Add(new Appliance(type, Side.Left), new LifeAppliance(new Appliance(type, Side.Left), left));
            Appliance.Add(new Appliance(type, Side.Right), new LifeAppliance(new Appliance(type, Side.Right), right));
        }

    }
}
