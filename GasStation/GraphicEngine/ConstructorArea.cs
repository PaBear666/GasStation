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
            var currnetSqure = GetSquare(0);
            currnetSqure.Surface = new Surface(SurfaceType.DeleteCar, Color.Green);
            var a = new Bitmap("C:\\Users\\NIKITA\\OneDrive\\Рабочий стол\\00_B9AkuG0.jpg.740x555_q85_box-314,0,1918,1200_crop_detail_upscale.jpg");
           
            currnetSqure.OverEntity = new Simulation(currnetSqure.Surface, SimulatorType.TankCar,Color.Red, image: a);
        }
    }
}
