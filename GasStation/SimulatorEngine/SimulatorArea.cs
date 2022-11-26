using GasStation.ConstructorEngine;
using GasStation.GraphicEngine.Common;
using GasStation.SimulatorEngine.ApplianceProviders;
using GasStation.SimulatorEngine.Cars;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GasStation.SimulatorEngine
{
    public class SimulatorArea : Area<SimulatorSquare>
    {
        readonly ApplianceManager _applianceManager;
        readonly EditorProvider _editorProvider;
        CarProvider _carProvider;
        readonly Task _simulation;
        Form _form;

        public int Acceleration { get; set; }
        public bool IsCorrect { get; }
        public string ErrorMessage { get; }


        public SimulatorArea(Panel panel, TopologyTransfer topology, EditorProvider editorProvider, Form form) : base(panel, topology.WidthLength, topology.HeightLength)
        {
            _editorProvider = editorProvider;
            _applianceManager = new ApplianceManager(topology.RowSide);
            _simulation = new Task(Examine);
            Acceleration = 1;
            _form = form;

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

                if (square.LifeAppliance != null)
                {
                    currentSquare.LifeAppliance = _editorProvider.Appliance[square.LifeAppliance.Appliance];
                    _applianceManager.AddAppliance(currentSquare,
                        SquareHelper.GetArroundSquare(Squares,
                        currentSquare,
                        topology.HeightLength,
                        topology.WidthLength,
                        currentSquare.LifeAppliance.Appliance.Side));
                }
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
                    spawnSquare = GetSquare(Heightength * WidthLength - 1);
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
            _carProvider = new CarProvider(spawnSquare, disspawnSquare, Squares, WidthLength, Heightength, _form);
        }

        public void Run()
        {
            _simulation.Start();
        }

        public void Stop()
        {
            if (IsCorrect)
            {
                _simulation.Wait();
            }
        }

        private void Examine()
        {
            try
            {
                while (true)
                {
                    _carProvider.SimulateCar();
                    var random = new Random();
                    if (random.NextDouble() > 0.5)
                        _carProvider.SpawnCar(CarType.CommonCar);
                    Thread.Sleep(1000 / Acceleration);
                }
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine(e);
            }       
        }
    }
}
