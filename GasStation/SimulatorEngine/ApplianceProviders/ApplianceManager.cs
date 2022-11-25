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
        readonly ICollection<LifeSquare> _bridgeCollection;

        readonly IDictionary<Appliance, int> _bridgeCheckerService;
        readonly IDictionary<Appliance, int> _bridgeCheckerGasStation;

        public ApplianceManager(Side rowSide)
        {
            _gasStationProvider = new GasStationProvider();
            _shopProvider = new ShopProvider();
            _tankerProvider = new TankerProvider();
            _bridgeIsCorrect = true;

            _bridgeCheckerService = new Dictionary<Appliance, int>();
            _bridgeCheckerGasStation = new Dictionary<Appliance, int>();
            _bridgeCollection = new List<LifeSquare>();

            if (rowSide == Side.Top || rowSide == Side.Bottom)
            {
                _bridgeCheckerGasStation.Add(new Appliance(ApplianceType.Bridge, Side.Top), 0);
                _bridgeCheckerGasStation.Add(new Appliance(ApplianceType.Bridge, Side.Bottom), 0);

                _bridgeCheckerService.Add(new Appliance(ApplianceType.Bridge, Side.Top), 0);
                _bridgeCheckerService.Add(new Appliance(ApplianceType.Bridge, Side.Bottom), 0);
            }
            else
            {
                _bridgeCheckerGasStation.Add(new Appliance(ApplianceType.Bridge, Side.Left), 0);
                _bridgeCheckerGasStation.Add(new Appliance(ApplianceType.Bridge, Side.Right), 0);

                _bridgeCheckerService.Add(new Appliance(ApplianceType.Bridge, Side.Left), 0);
                _bridgeCheckerService.Add(new Appliance(ApplianceType.Bridge, Side.Right), 0);
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
                    switch (lifeSquare.Surface.Type)
                    {
                        case SurfaceType.Service:
                            if (_bridgeCheckerService.TryGetValue(lifeSquare.LifeAppliance.Appliance, out int value))
                            {
                                _bridgeCheckerService[lifeSquare.LifeAppliance.Appliance] = value + 1;
                                _bridgeCollection.Add(lifeSquare);
                            }
                            else
                            {
                                _bridgeIsCorrect = false;
                            }
                            break;

                        case SurfaceType.GasStation:
                            if (_bridgeCheckerGasStation.TryGetValue(lifeSquare.LifeAppliance.Appliance, out int value1))
                            {
                                _bridgeCheckerGasStation[lifeSquare.LifeAppliance.Appliance] = value1 + 1;
                                _bridgeCollection.Add(lifeSquare);
                            }
                            else
                            {
                                _bridgeIsCorrect = false;
                            }
                            break;
                    }
                    break;
            }
        }

        public bool IsCorrect(out string message)
        {
            var stringBuilder = new StringBuilder();
            bool topologyIsCorrect = true;

            foreach (var serviceBridge in _bridgeCheckerService)
            {
                if(serviceBridge.Value != 1)
                {
                    stringBuilder.AppendLine($"Топология должна иметь один переезд на сервисной часте в сторону {serviceBridge.Key.Side}");
                    topologyIsCorrect = false;
                }
            }

            foreach (var bridge in _bridgeCheckerGasStation)
            {
                if (bridge.Value != 1)
                {
                    stringBuilder.AppendLine($"Топология должна иметь один переезд на заправочной часте в сторону {bridge.Key.Side}");
                    topologyIsCorrect = false;
                }
            }

            topologyIsCorrect = _gasStationProvider.IsCorrect(out var gasStatonErrorMessage) && topologyIsCorrect;
            topologyIsCorrect = _shopProvider.IsCorrect(out var shopErrorMessage) && topologyIsCorrect;
            topologyIsCorrect = _tankerProvider.IsCorrect(out var tankerErrorMessage) && topologyIsCorrect;
            topologyIsCorrect = _bridgeIsCorrect && topologyIsCorrect;

            stringBuilder.AppendLine(gasStatonErrorMessage);
            stringBuilder.AppendLine(shopErrorMessage);
            stringBuilder.AppendLine(tankerErrorMessage);

            message = stringBuilder.ToString();
            return topologyIsCorrect;
        }
    }
}
