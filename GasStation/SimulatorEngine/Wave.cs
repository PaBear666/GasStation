using GasStation.ConstructorEngine;
using GasStation.GraphicEngine.Common;
using System.Collections.Generic;
using System.Linq;

namespace GasStation.SimulatorEngine
{
    public class Wave
    {
        public SurfaceType AvailableSurface { get; set; }
        public int BridgeId { get; set; }

        //public bool TryGetWay(SimulatorSquare[] simulatorSquares, int from, int to, out Side side)
        //{
        //    ICollection<SimulatorSquare> bridgeAfterSurface = new List<SimulatorSquare>();
        //    WaveSqaure[] waveSqaures = new WaveSqaure[simulatorSquares.Length];

        //    foreach (var item in waveSqaures)
        //    {
        //        if (item.SimulatorSquare.Car != null
        //            || (item.SimulatorSquare.LifeAppliance != null 
        //                && item.SimulatorSquare.Id != BridgeId)
        //            || (item.SimulatorSquare.Surface.Type == AvailableSurface
        //                && item.SimulatorSquare.Surface.Type != SurfaceType.Road))
        //        {
        //            item.Weigth = -1;
        //        }
        //        else
        //        {
        //            item.Weigth = 0;
        //        }
        //    }

        //    for (int i = 0; i < waveSqaures.Length; i++)
        //    {

        //    }
        //}

        private bool TryGetWayByBridge(SimulatorSquare[] simulatorSquares, int from, int height, int width, out WaveSqaure[] resultSquares)
        {
            var waveSquares = FillByAvailable(simulatorSquares, new SurfaceType[] { simulatorSquares[from].Surface.Type });
            waveSquares[from].MainSquare = true;
            Stack<WaveSqaure> squaresForCheckedArround = new Stack<WaveSqaure>();
            squaresForCheckedArround.Push(waveSquares[from]);
            var sideSquareIndexs = new int[] { 1, 3, 5, 7 };
            bool isBridgeFounded = false;

            while(squaresForCheckedArround.Count > 0)
            {
                var currentSquare = squaresForCheckedArround.Pop();
                var arroundSquares = SquareHelper.GetArroundSquares(simulatorSquares, currentSquare.SimulatorSquare, height, width);

                foreach (var sideIndex in sideSquareIndexs)
                {
                    var sideSquare = arroundSquares[sideIndex];
                    if (sideSquare.Surface.Type == simulatorSquares[from].Surface.Type
                        && waveSquares[sideSquare.Id].Weigth == 0
                        && !waveSquares[sideSquare.Id].MainSquare)
                    {
                        waveSquares[sideSquare.Id] = new WaveSqaure(sideSquare, currentSquare.Weigth + 1);
                    }

                    if(sideSquare.Id == BridgeId)
                    {
                        isBridgeFounded = true;
                    }
                }
            }
            resultSquares = waveSquares;
            return isBridgeFounded;
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
