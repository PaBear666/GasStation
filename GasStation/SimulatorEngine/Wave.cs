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

        public bool TryGetSide(int from, int to, bool carIsGo, out Side side)
        {
            bool needBridge = false;
            WaveSqaure[] waveSqaures = null;
            var successWay = -1;
            if (_simulatorSquares[from].Surface.Type != _simulatorSquares[to].Surface.Type)
            {
                needBridge = true;
                var bridge =_bridges[new BridgeWay(_simulatorSquares[from].Surface.Type, _simulatorSquares[to].Surface.Type)];
                var availableMap = GetAvialableMap(new SurfaceType[] { _simulatorSquares[from].Surface.Type }, carIsGo);
                successWay = TryGetWay(availableMap, from, bridge.Id, carIsGo, out waveSqaures, bridge.Id);
            }
            else
            {
                var availableMap = GetAvialableMap(new SurfaceType[] { _simulatorSquares[from].Surface.Type }, carIsGo);
                successWay = TryGetWay(availableMap, from, to, carIsGo, out waveSqaures);

            }

            if(successWay != -1)
            {
                side = GetSide(waveSqaures, from, to, false);
            }
            else
            {
                side = Side.Top;
            }
         
            return successWay != -1;
        }

        private Side GetSide(WaveSqaure[] currentWave, int from, int to, bool carIsGo)
        {
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


                        for (int i = 0; i < fromSquares.Length; i++)
                        {
                            if (fromSquares[i] == null)
                            {
                                continue;
                            }

                            if(fromSquares[i].Id == arroundSquare.SimulatorSquare.Id)
                            {
                                switch (i)
                                {
                                    case 1:
                                        return Side.Left;
                                    case 3:
                                        return Side.Top;
                                    case 5:
                                        return Side.Bottom;
                                    case 7:
                                        return Side.Right;
                                }
                            }

                        }


                        if (arroundSquare.Weigth != -1 && currentSquare.Weigth > arroundSquare.Weigth)
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
            Stack<WaveSqaure> squaresForCheckedArround = new Stack<WaveSqaure>();
            squaresForCheckedArround.Push(waveSquares[from]);
            
            int isFoundingAppliance = -1;

            while(squaresForCheckedArround.Count > 0)
            {
                var currentSquare = squaresForCheckedArround.Pop();
                var arroundSquares = SquareHelper.GetArroundSquares(_simulatorSquares, currentSquare.SimulatorSquare, _height, _width);

                foreach (var sideIndex in _sideSquareIndexs)
                {
                    var sideSquare = arroundSquares[sideIndex];
                    if(sideSquare != null)
                    {
                        if (
                            (!carIsGo || sideSquare.Car == null) 
                            && sideSquare.Id == bridgeId
                            || (sideSquare.Surface.Type == _simulatorSquares[from].Surface.Type
                            && waveSquares[sideSquare.Id].Weigth == 0               
                            && !waveSquares[sideSquare.Id].MainSquare))
                        {
                            waveSquares[sideSquare.Id] = new WaveSqaure(sideSquare, currentSquare.Weigth + 1);
                            squaresForCheckedArround.Push(waveSquares[sideSquare.Id]);
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
                    && square.LifeAppliance == null)
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
