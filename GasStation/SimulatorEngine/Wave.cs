using GasStation.ConstructorEngine;
using GasStation.GraphicEngine.Common;
using GasStation.SimulatorEngine.ApplianceProviders;
using GasStation.SimulatorEngine.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;

namespace GasStation.SimulatorEngine
{
    public class Wave
    {
        private int[] _sideSquareIndexs = new int[] { 1, 3, 5, 7 };
        private SimulatorSquare[] _simulatorSquares;
        private int _height;
        private int _width;
        private IDictionary<BridgeWay, SimulatorSquare> _bridges;

        public Wave(SimulatorSquare[] simulatorSquares, IDictionary<BridgeWay, SimulatorSquare> Bridges ,int height, int width)
        {
            _simulatorSquares = simulatorSquares;
            _height = height;
            _width = width;
            _bridges = Bridges;
        }

        public bool TryGetSide(SurfaceType availableSurface, int from, int to, out Side? side)
        {
            int successWay;
            WaveSqaure[] waveSqaures;
            if (_simulatorSquares[from].Surface.Type != _simulatorSquares[to].Surface.Type)
            {
                var bridge = _bridges[new BridgeWay(_simulatorSquares[from].Surface.Type, _simulatorSquares[to].Surface.Type)];
                var availableMap = GetAvialableMap(new SurfaceType[] { _simulatorSquares[from].Surface.Type }, true);
                successWay = TryGetWay(availableMap, from, bridge.Id, true, out waveSqaures, bridge.Id);
                to = bridge.Id;

            }
            else
            {
                var availableMap = GetAvialableMap(new SurfaceType[] { _simulatorSquares[from].Surface.Type, availableSurface }, true);
                successWay = TryGetWay(availableMap, from, to, true, out waveSqaures);

            }
         
            if(successWay != -1)
            {
                return GetSide(waveSqaures, from, to, false, out side);
            }
            else
            {
                side = null;
                return false;
            }
        }

        private bool GetSide(WaveSqaure[] currentWave, int from, int to, bool carIsGo,out Side? side)
        {
            if(from == to)
            {
                side = null;
                return false;
            }

            var currentSquare = currentWave[to];
            var fromSquares = SquareHelper.GetArroundSquares(_simulatorSquares, currentWave[from].SimulatorSquare, _height, _width);

            while (true)
            {
                var arroundSquares = SquareHelper.GetArroundSquares(_simulatorSquares, currentSquare.SimulatorSquare, _height, _width);
                foreach (var sideIndex in _sideSquareIndexs)
                {
                    if(arroundSquares[sideIndex] != null)
                    {
                        var arroundSquare = currentWave[arroundSquares[sideIndex].Id];

                        if (arroundSquare.MainSquare)
                        {
                            int i = 0;
                            while(fromSquares.Length > i)
                            {
                                if(fromSquares[i] != null && fromSquares[i].Id == currentSquare.SimulatorSquare.Id)
                                {
                                    switch (i)
                                    {
                                        case 1:
                                            side = Side.Left;
                                            break;
                                        case 3:
                                            side = Side.Top;
                                            break;
                                        case 5:
                                            side = Side.Bottom;
                                            break;
                                        case 7:
                                            side = Side.Right;
                                            break;
                                        default:
                                            side = null;
                                            break;
                                    }

                                    return side != null;
                                }
                                i++;
                            }
                        }
                        
                        if (arroundSquare.Weigth > 0 && currentSquare.Weigth > arroundSquare.Weigth)
                        {
                            currentSquare = arroundSquare;
                            break;
                        }
                    }                    
                }

            }


        }

        private int TryGetWay(WaveSqaure[] currentWave, int from, int to, bool carIsGo, out WaveSqaure[] resultSquares, int bridgeId = -1)
        {
            var waveSquares = currentWave;
            waveSquares[from].MainSquare = true;
            Queue<WaveSqaure> squaresForCheckedArround = new Queue<WaveSqaure>();
            squaresForCheckedArround.Enqueue(waveSquares[from]);

            if (waveSquares[from].SimulatorSquare.LifeAppliance != null 
                && waveSquares[from].SimulatorSquare.LifeAppliance.Appliance.Type == ApplianceType.Bridge)
            {
                waveSquares[from].Weigth = 0;
            }

            int isFoundingAppliance = -1;

            while(squaresForCheckedArround.Count > 0)
            {
                var currentSquare = squaresForCheckedArround.Dequeue();
                var arroundSquares = SquareHelper.GetArroundSquares(_simulatorSquares, currentSquare.SimulatorSquare, _height, _width);

                foreach (var sideIndex in _sideSquareIndexs)
                {
                    var sideSquare = arroundSquares[sideIndex];
                    if(sideSquare != null)
                    {
                        if (
                            sideSquare.Id == bridgeId                          
                            || (sideSquare.Surface.Type == _simulatorSquares[from].Surface.Type
                            && waveSquares[sideSquare.Id].Weigth == 0               
                            && !waveSquares[sideSquare.Id].MainSquare))
                        {
                            waveSquares[sideSquare.Id] = new WaveSqaure(sideSquare, currentSquare.Weigth + 1);
                            squaresForCheckedArround.Enqueue(waveSquares[sideSquare.Id]);
                        }

                        if (sideSquare.Id == to)
                        {
                            resultSquares = waveSquares;
                            return sideSquare.Id;
                        }
                    }
                }
            }
            resultSquares = waveSquares;
            return isFoundingAppliance;
        }

        public WaveSqaure[] GetAvialableMap(SurfaceType[] surfaceTypes, bool carIsGo)
        {
            WaveSqaure[] waveSqaures = new WaveSqaure[_simulatorSquares.Length];

            foreach (var square in _simulatorSquares)
            {
                if ((carIsGo || square.Car == null)
                    && surfaceTypes.Any(s => s == square.Surface.Type)
                    && (square.LifeAppliance == null
                       || (square.LifeAppliance != null 
                       && square.LifeAppliance.Appliance.Type == ApplianceType.Bridge)))
                {
                    waveSqaures[square.Id] = new WaveSqaure(square, 0);
                }
                else
                {
                    waveSqaures[square.Id] = new WaveSqaure(square, -1);
                }
            }

            return waveSqaures;
        }
    }



    public class WaveSqaure
    {
        public WaveSqaure(SimulatorSquare simulatorSquare, int weigth)
        {
            SimulatorSquare = simulatorSquare;
            Weigth = weigth;
        }

        public SimulatorSquare SimulatorSquare { get; set; }
        public int Weigth { get; set; }

        public bool MainSquare { get; set; }
    }
}
