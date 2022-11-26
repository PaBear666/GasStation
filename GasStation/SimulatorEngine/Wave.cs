using GasStation.ConstructorEngine;
using GasStation.GraphicEngine.Common;
using GasStation.SimulatorEngine.Cars;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GasStation.SimulatorEngine
{
    public class Wave
    {
        private int[] _sideSquareIndexs = new int[] { 1, 3, 5, 7 };
        private SimulatorSquare[] _simulatorSquares;
        private int _height;
        private int _width;

        public Wave(SimulatorSquare[] simulatorSquares, int height, int width)
        {
            _simulatorSquares = simulatorSquares;
            _height = height;
            _width = width;
        }

        public bool TryGetSide(int from, int to, out Side side)
        {
            bool needBridge = false;
            if (_simulatorSquares[from].Surface.Type != _simulatorSquares[to].Surface.Type)
            {
                needBridge = true;
            
            }

            side = Side.Left;
            return true;
        }

        private bool TryGetWay(SurfaceType[] surfaceTypes, int from, int to, bool carIsGo, out WaveSqaure[] resultSquares)
        {
            var waveSquares = FillByAvailable(surfaceTypes, carIsGo);
            waveSquares[from].MainSquare = true;
            Stack<WaveSqaure> squaresForCheckedArround = new Stack<WaveSqaure>();
            squaresForCheckedArround.Push(waveSquares[from]);
            
            bool isFoundingAppliance = false;

            while(squaresForCheckedArround.Count > 0)
            {
                var currentSquare = squaresForCheckedArround.Pop();
                var arroundSquares = SquareHelper.GetArroundSquares(_simulatorSquares, currentSquare.SimulatorSquare, _height, _width);

                foreach (var sideIndex in _sideSquareIndexs)
                {
                    var sideSquare = arroundSquares[sideIndex];
                    if (sideSquare.Surface.Type == _simulatorSquares[from].Surface.Type
                        && waveSquares[sideSquare.Id].Weigth == 0
                        && !waveSquares[sideSquare.Id].MainSquare)
                    {
                        waveSquares[sideSquare.Id] = new WaveSqaure(sideSquare, currentSquare.Weigth + 1);
                    }

                    if(sideSquare.Id == to)
                    {
                        isFoundingAppliance = true;
                    }
                }
            }
            resultSquares = waveSquares;
            return isFoundingAppliance;
        }

        public WaveSqaure[] FillByAvailable(SurfaceType[] surfaceTypes, bool carIsGo)
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
