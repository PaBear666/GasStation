using GasStation.ConstructorEngine;
using GasStation.SimulatorEngine.ApplianceSimulators;
using System.Collections.Generic;
using System.Text;

namespace GasStation.SimulatorEngine.ApplianceProviders
{
    public class ApplianceManager
    {
        private bool _bridgeIsCorrect;
        readonly ApplianceProvider<GasStationSimulator> _gasStationProvider;
        readonly ApplianceProvider<ShopSimulator> _shopProvider;
        readonly ApplianceProvider<TankerSimulator> _tankerProvider;
        readonly Side _rowSide;

        public IDictionary<BridgeWay, LifeSquare> Bridges { get; private set; }

        public ApplianceManager(Side rowSide)
        {
            _gasStationProvider = new GasStationProvider();
            _shopProvider = new ShopProvider();
            _tankerProvider = new TankerProvider();
            _bridgeIsCorrect = true;
            _rowSide = rowSide;

            Bridges = new Dictionary<BridgeWay, LifeSquare>();

            switch (rowSide)
            {
                case Side.Top:
                    Bridges.Add(new BridgeWay(SurfaceType.Road, SurfaceType.Service, Side.Bottom), null);
                    Bridges.Add(new BridgeWay(SurfaceType.Service, SurfaceType.Road, Side.Top), null);
                    Bridges.Add(new BridgeWay(SurfaceType.Road, SurfaceType.GasStation, Side.Bottom), null);
                    Bridges.Add(new BridgeWay(SurfaceType.GasStation, SurfaceType.Road, Side.Top), null);
                    break;
                case Side.Right:
                    Bridges.Add(new BridgeWay(SurfaceType.Road, SurfaceType.Service, Side.Left), null);
                    Bridges.Add(new BridgeWay(SurfaceType.Service, SurfaceType.Road, Side.Right), null);
                    Bridges.Add(new BridgeWay(SurfaceType.Road, SurfaceType.GasStation, Side.Left), null);
                    Bridges.Add(new BridgeWay(SurfaceType.GasStation, SurfaceType.Road, Side.Right), null);
                    break;
                case Side.Bottom:
                    Bridges.Add(new BridgeWay(SurfaceType.Road, SurfaceType.Service, Side.Top), null);
                    Bridges.Add(new BridgeWay(SurfaceType.Service, SurfaceType.Road, Side.Bottom), null);
                    Bridges.Add(new BridgeWay(SurfaceType.Road, SurfaceType.GasStation, Side.Top), null);
                    Bridges.Add(new BridgeWay(SurfaceType.GasStation, SurfaceType.Road, Side.Bottom), null);
                    break;
                case Side.Left:
                    Bridges.Add(new BridgeWay(SurfaceType.Road, SurfaceType.Service, Side.Right), null);
                    Bridges.Add(new BridgeWay(SurfaceType.Service, SurfaceType.Road, Side.Left), null);
                    Bridges.Add(new BridgeWay(SurfaceType.Road, SurfaceType.GasStation, Side.Right), null);
                    Bridges.Add(new BridgeWay(SurfaceType.GasStation, SurfaceType.Road, Side.Left), null);
                    break;

            }
        }

        public void AddAppliance(LifeSquare lifeSquare, SimulatorSquare usedSquare)
        {
            switch (lifeSquare.LifeAppliance.Appliance.Type)
            {
                case ApplianceType.Shop:
                    _shopProvider.Appliances.Add(new ShopSimulator(lifeSquare, usedSquare));
                    break;
                case ApplianceType.GasStation:
                    _gasStationProvider.Appliances.Add(new GasStationSimulator(lifeSquare, usedSquare));
                    break;
                case ApplianceType.Tanker:
                    _tankerProvider.Appliances.Add(new TankerSimulator(lifeSquare, usedSquare));
                    break;
                case ApplianceType.Bridge:
                    var bridge = new BridgeWay(lifeSquare.Surface.Type, usedSquare.Surface.Type, lifeSquare.LifeAppliance.Appliance.Side);
                    if(bridge.To == bridge.From)
                    {
                        switch (_rowSide)
                        {
                            case Side.Top:
                                if(bridge.Side == Side.Bottom)
                                    bridge.From = SurfaceType.Road;
                                break;
                            case Side.Right:
                                if (bridge.Side == Side.Left)
                                    bridge.From = SurfaceType.Road;
                                break;
                            case Side.Bottom:
                                if (bridge.Side == Side.Top)
                                    bridge.From = SurfaceType.Road;
                                break;
                            case Side.Left:
                                if (bridge.Side == Side.Right)
                                    bridge.From = SurfaceType.Road;
                                break;
                        }
                        
                    }
                    if (Bridges.ContainsKey(bridge)){
                        Bridges[bridge] = lifeSquare;
                    }
                    else
                    {
                        _bridgeIsCorrect = false;
                    }
                break;
            }
        }

        public bool IsCorrect(out string message)
        {
            var stringBuilder = new StringBuilder();
            bool topologyIsCorrect = true;

            foreach (var bridge in Bridges)
            {
                if (bridge.Value == null)
                {
                    stringBuilder.AppendLine($"Топология должна иметь один переезд на {bridge.Key.From} часте в сторону {bridge.Key.To}");
                    topologyIsCorrect = false;
                }
            }

            topologyIsCorrect = _gasStationProvider.IsCorrect(out var gasStatonErrorMessage) && topologyIsCorrect;
            topologyIsCorrect = _shopProvider.IsCorrect(out var shopErrorMessage) && topologyIsCorrect;
            topologyIsCorrect = _tankerProvider.IsCorrect(out var tankerErrorMessage) && topologyIsCorrect;
            topologyIsCorrect = _bridgeIsCorrect && topologyIsCorrect;

            if(gasStatonErrorMessage != string.Empty)
                stringBuilder.AppendLine(gasStatonErrorMessage);

            if (shopErrorMessage != string.Empty)
                stringBuilder.AppendLine(shopErrorMessage);

            if (tankerErrorMessage != string.Empty)
                stringBuilder.AppendLine(tankerErrorMessage);

            message = stringBuilder.ToString();
            return topologyIsCorrect;
        }
    }
}
