

using GasStation.ConstructorEngine;
using GasStation.GraphicEngine.Common;
using System.Drawing;
using System.Windows.Forms;

namespace GasStation.SimulatorEngine
{
    public class SimulatorArea : Area<SimulatorSquare>
    {
        readonly EditorProvider _editorProvider;
        public SimulatorArea(Panel panel, TopologyTransfer topology, EditorProvider editorProvider) : base(panel, topology.WidthLength, topology.HeightLength)
        {
            _editorProvider = editorProvider;
            InitArea(SquareSize, topology);
        }

        private void InitArea(Size size, TopologyTransfer topology)
        {
            int id = 0;
            for (int i = 0; i < topology.WidthLength; i++)
            {
                for (int j = 0; j < topology.HeightLength; j++)
                {
                    var square = new SimulatorSquare(id, new Point(i * size.Width, j * size.Height), size, _editorProvider.Surfaces[SurfaceType.GasStation]);
                    AddSquare(id, square);
                    id++;
                }
            }


            foreach (var square in topology.Squares)
            {
                var currentSquare = GetSquare(square.Id);
                currentSquare.Surface = _editorProvider.Surfaces[square.Surface.Type];

                if (square.LifeAppliance != null)
                {
                    currentSquare.LifeAppliance = _editorProvider.Appliance[square.LifeAppliance.Appliance];
                }
            }
        }


    }
}
