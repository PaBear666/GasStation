using GasStation.ConstructorEngine;
using GasStation.GraphicEngine.Common;
using GasStation.SimulatorEngine.ApplianceProviders;
using GasStation.SimulatorEngine.Cars;
using System.Drawing;
using System.Windows.Forms;

namespace GasStation.SimulatorEngine
{
    public class SimulatorArea : Area<SimulatorSquare>
    {
        public bool IsCorrect { get; }
        public string ErrorMessage { get; }
        readonly ApplianceManager _applianceManager;
        readonly EditorProvider _editorProvider;
        CarProvider _carProvider;

        public SimulatorArea(Panel panel, TopologyTransfer topology, EditorProvider editorProvider) : base(panel, topology.WidthLength, topology.HeightLength)
        {
            _editorProvider = editorProvider;
            _applianceManager = new ApplianceManager(topology.RowSide);
            InitArea(SquareSize, topology);

            IsCorrect = _applianceManager.IsCorrect(out var errorMessage);
            ErrorMessage = errorMessage;
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
            }

            SimulatorSquare spawnSquare = null;
            SimulatorSquare disspawnSquare = null;

            switch (topology.RowSide)
            {
                case Side.Top:
                    spawnSquare = GetSquare(Heightength * WidthLength - Heightength);
                    disspawnSquare = GetSquare(0);
                    break;
                case Side.Bottom:
                    spawnSquare = GetSquare(Heightength * WidthLength);
                    disspawnSquare = GetSquare(Heightength - 1);
                    break;
                case Side.Right:
                    spawnSquare = GetSquare(Heightength * WidthLength);
                    disspawnSquare = GetSquare(Heightength * WidthLength - Heightength);
                    break;
                case Side.Left:
                    spawnSquare = GetSquare(Heightength - 1);
                    disspawnSquare = GetSquare(0);
                    break;
            }
            _carProvider = new CarProvider(spawnSquare, disspawnSquare);

            foreach (var square in topology.Squares)
            {
                if (square.LifeAppliance != null)
                {
                    var currentSquare = GetSquare(square.Id);
                    currentSquare.LifeAppliance = _editorProvider.Appliance[square.LifeAppliance.Appliance];
                    _applianceManager.AddAppliance(currentSquare,
                        SquareHelper.GetArroundSquare(Squares,
                        currentSquare,
                        topology.HeightLength,
                        topology.WidthLength,
                        currentSquare.LifeAppliance.Appliance.Side));
                }
            }
        }
    }
}
