﻿using GasStation.ConstructorEngine;
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

        public IDictionary<BridgeWay, SimulatorSquare> Bridges { get; private set; }
        private IDictionary<(BridgeWay, Side), LifeSquare> _bridges;

        public ApplianceManager(Side rowSide)
        {
            _gasStationProvider = new GasStationProvider();
            _shopProvider = new ShopProvider();
            _tankerProvider = new TankerProvider();
            _bridgeIsCorrect = true;
            _rowSide = rowSide;

            Bridges = new Dictionary<BridgeWay, SimulatorSquare>();
            _bridges = new Dictionary<(BridgeWay,Side), LifeSquare>();

            switch (rowSide)
            {
                case Side.Top:
                    _bridges.Add((new BridgeWay(SurfaceType.Road, SurfaceType.Service), Side.Bottom), null);
                    _bridges.Add((new BridgeWay(SurfaceType.Service, SurfaceType.Road), Side.Top), null);
                    _bridges.Add((new BridgeWay(SurfaceType.Road, SurfaceType.GasStation), Side.Bottom), null);
                    _bridges.Add((new BridgeWay(SurfaceType.GasStation, SurfaceType.Road), Side.Top), null);
                    break;       
                case Side.Right: 
                    _bridges.Add((new BridgeWay(SurfaceType.Road, SurfaceType.Service), Side.Left), null);
                    _bridges.Add((new BridgeWay(SurfaceType.Service, SurfaceType.Road), Side.Right), null);
                    _bridges.Add((new BridgeWay(SurfaceType.Road, SurfaceType.GasStation), Side.Left), null);
                    _bridges.Add((new BridgeWay(SurfaceType.GasStation, SurfaceType.Road), Side.Right), null);
                    break;       
                case Side.Bottom:
                    _bridges.Add((new BridgeWay(SurfaceType.Road, SurfaceType.Service), Side.Top), null);
                    _bridges.Add((new BridgeWay(SurfaceType.Service, SurfaceType.Road), Side.Bottom), null);
                    _bridges.Add((new BridgeWay(SurfaceType.Road, SurfaceType.GasStation), Side.Top), null);
                    _bridges.Add((new BridgeWay(SurfaceType.GasStation, SurfaceType.Road), Side.Bottom), null);
                    break;       
                case Side.Left:  
                    _bridges.Add((new BridgeWay(SurfaceType.Road, SurfaceType.Service), Side.Right), null);
                    _bridges.Add((new BridgeWay(SurfaceType.Service, SurfaceType.Road), Side.Left), null);
                    _bridges.Add((new BridgeWay(SurfaceType.Road, SurfaceType.GasStation), Side.Right), null);
                    _bridges.Add((new BridgeWay(SurfaceType.GasStation, SurfaceType.Road), Side.Left), null);
                    break;

            }
        }

        public void AddAppliance(SimulatorSquare current, SimulatorSquare usedSquare)
        {
            switch (current.LifeAppliance.Appliance.Type)
            {
                case ApplianceType.Shop:
                    _shopProvider.Appliances.Add(new ShopSimulator(current, usedSquare));
                    break;
                case ApplianceType.GasStation:
                    _gasStationProvider.Appliances.Add(new GasStationSimulator(current, usedSquare));
                    break;
                case ApplianceType.Tanker:
                    _tankerProvider.Appliances.Add(new TankerSimulator(current, usedSquare));
                    break;
                case ApplianceType.Bridge:
                    if(usedSquare == null)
                    {
                        _bridgeIsCorrect = false;
                        return;
                    }

                    var bridge = new BridgeWay(current.Surface.Type, usedSquare.Surface.Type);
                    if(bridge.To == bridge.From)
                    {
                        switch (_rowSide)
                        {
                            case Side.Top:
                                if(current.LifeAppliance.Appliance.Side == Side.Bottom)
                                    bridge.From = SurfaceType.Road;
                                break;
                            case Side.Right:
                                if (current.LifeAppliance.Appliance.Side == Side.Left)
                                    bridge.From = SurfaceType.Road;
                                break;
                            case Side.Bottom:
                                if (current.LifeAppliance.Appliance.Side == Side.Top)
                                    bridge.From = SurfaceType.Road;
                                break;
                            case Side.Left:
                                if (current.LifeAppliance.Appliance.Side == Side.Right)
                                    bridge.From = SurfaceType.Road;
                                break;
                        }
                        
                    }
                    if (_bridges.ContainsKey((bridge, current.LifeAppliance.Appliance.Side))){
                        _bridges[(bridge, current.LifeAppliance.Appliance.Side)] = current;
                        Bridges.Add(bridge, current);
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

            foreach (var bridge in _bridges)
            {
                if (bridge.Value == null)
                {
                    stringBuilder.AppendLine($"Топология должна иметь один переезд на {bridge.Key.Item1.From} часте в сторону {bridge.Key.Item1.To}");
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
