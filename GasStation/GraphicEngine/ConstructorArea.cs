using GasStation.GraphicEngine.Common;
using GasStation.LifeEngine;
using System.Drawing;
using System.Windows.Forms;

namespace GasStation.GraphicEngine
{
    class ConstructorArea : Area<LifeSquare>
    {
        public ConstructorArea(Panel panel, Size squareSize) : base(panel, squareSize)
        {
            int id = 0;
            for (int i = 0; i < SquareWidthLength; i++)
            {
                for (int j = 0; j < SquareHeightLength; j++)
                {
                    var square = new LifeSquare(id, new Point(i * squareSize.Width, j * squareSize.Height), squareSize, new Surface(SurfaceType.Earth, Color.White));
                    AddSquare(id, square);
                    id++;
                }
            }
            var currnetSqure = GetSquare(22);
            currnetSqure.Surface = new Surface(SurfaceType.DeleteCar, Color.Green);
        }
    }
}
