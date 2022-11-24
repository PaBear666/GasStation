using GasStation.ConstructorEngine;
using GasStation.GraphicEngine.Common;
using System.Collections.Generic;
using System.Linq;

namespace GasStation.SimulatorEngine
{
    public class Wave
    {
        private int[] _sideSquareIndexs = new int[] { 1, 3, 5, 7 };
        public bool TryGetSide(SimulatorSquare[] simulatorSquares, int from, int to, int bridgeId,int height, int width, out Side side)
        {
            if(TryGetWay(simulatorSquares, from, bridgeId, height, width, out var result))
            {

            }

            side = Side.Left;
            return false;
        }

        private bool TryGetWay(SimulatorSquare[] simulatorSquares, int from, int to, int height, int width, out WaveSqaure[] resultSquares)
        {
            var waveSquares = FillByAvailable(simulatorSquares, new SurfaceType[] { simulatorSquares[from].Surface.Type });
            waveSquares[from].MainSquare = true;
            Stack<WaveSqaure> squaresForCheckedArround = new Stack<WaveSqaure>();
            squaresForCheckedArround.Push(waveSquares[from]);
            
            bool isFoundingAppliance = false;

            while(squaresForCheckedArround.Count > 0)
            {
                var currentSquare = squaresForCheckedArround.Pop();
                var arroundSquares = SquareHelper.GetArroundSquares(simulatorSquares, currentSquare.SimulatorSquare, height, width);

                foreach (var sideIndex in _sideSquareIndexs)
                {
                    var sideSquare = arroundSquares[sideIndex];
                    if (sideSquare.Surface.Type == simulatorSquares[from].Surface.Type
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

        private WaveSqaure[] FillByAvailable(SimulatorSquare[] simulatorSquares, SurfaceType[] surfaceTypes)
        {
            WaveSqaure[] waveSqaures = new WaveSqaure[simulatorSquares.Length];
            foreach (var square in simulatorSquares)
            {
                if(square.Car == null 
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
