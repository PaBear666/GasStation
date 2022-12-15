using GasStation.ConstructorEngine;
using GasStation.DB;
using GasStation.GraphicEngine.Common;
using GasStation.MathLogic;
using GasStation.SimulatorEngine.ApplianceProviders;
using GasStation.SimulatorEngine.Cars;
using MathNet.Numerics.Random;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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
        readonly CarViewProvider _carViewProvider;
        readonly CancellationToken _cancellation;
        bool _wasStarted;
        public bool IsStop { get; set; }

        public int Acceleration { get; set; }
        public bool IsCorrect { get; }
        public string ErrorMessage { get; }


        public SimulatorArea(Panel panel, TopologyTransfer topology, EditorProvider editorProvider, CancellationToken cancellation) : base(panel, topology.WidthLength, topology.HeightLength)
        {
            _editorProvider = editorProvider;
            _applianceManager = new ApplianceManager(topology.RowSide);
            _simulation = new Task(Examine, cancellation);
            _carViewProvider = new CarViewProvider();
            Acceleration = 5;
            _cancellation = cancellation;

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

            foreach(var square in topology.Squares)
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
            _carProvider = new CarProvider(spawnSquare,
                disspawnSquare,
                Squares,
                WidthLength,
                Heightength,
                new Wave(Squares,
                    _applianceManager.Bridges,
                    Heightength,
                    WidthLength));
        }

        public void Run()
        {
            if (!_wasStarted)
            {
                _simulation.Start();
                _wasStarted = true;
            }
            
            IsStop = false;
        }

        public void Stop()
        {
            IsStop = true;
        }

        private void Examine()
        {
            try
            {
                var random = new Random();
                int counter = -1000;
                DescTopologyClass topologyClass = new DescTopologyClass();
                topologyClass = DescTopologyClass.GetDesc("descriptor");
                Transport[] transports = topologyClass.Transports;
                double carTimer = RandomDistribution.GetTimeValue( topologyClass, random)*1000;
                while (!_cancellation.IsCancellationRequested)
                {
                    while (IsStop && !_cancellation.IsCancellationRequested) 
                    { 
                        Thread.Sleep(1000); 
                    }
                    _carProvider.SimulateCar();
                    
                    if (counter > carTimer)
                    {
                        var availableAppliance = _applianceManager.GasStationProvider.Appliances.FirstOrDefault(a => a.IsFree);
                        var car = new CommonCar(_carViewProvider.GetView(CarType.CommonCar), null, _carProvider.SpawnSquare, _carViewProvider.CarVType);
                        int rd = random.Next(0, transports.Length);
                        car.FuelV = transports[rd].Fuel;
                        car.MaxFuel = transports[rd].FuelVolume;
                        if (_carProvider.SpawnCar(car) && availableAppliance != null && availableAppliance.IsFree)
                        {
                            car.ToSquare = availableAppliance.UsedSquare;
                            availableAppliance.Cars.Enqueue(car);
                        }
                        carTimer = RandomDistribution.GetTimeValue(topologyClass, random) * 1000;
                        counter = -1000;
                    }
               
                    _applianceManager.Simulate();
                    //_applianceManager.TankerProvider.Appliances()
                    counter+=1000;
                    Thread.Sleep(1000 / Acceleration);
                }
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine(e.ToString());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
    }
}
