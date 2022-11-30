﻿using GasStation.ConstructorEngine;
using GasStation.GraphicEngine.Common;
using System.Drawing;

namespace GasStation.SimulatorEngine.Cars
{
    public abstract class SimulatorCar : ViewComponent
    {
        public SurfaceType AvailableSurfaceType { get; }
        public ApplianceType Appliance { get; }
        public CarType Type { get; }
        public CarState State { get; set; }
        public SimulatorSquare ToSquare { get; set; }

        public SimulatorSquare CurrentSquare { get; set; }

        public SimulatorCar(
            SimulatorSquare current,
            SimulatorSquare to,
            SurfaceType surfaceType,
            ApplianceType appliance,
            CarType carType,
            ViewComponent viewComponent) 
            : base(viewComponent.Color,
                  viewComponent.Image)
        {
            AvailableSurfaceType = surfaceType;
            Appliance = appliance;
            Type = carType;
            State = CarState.ToAppliance;
            ToSquare = to;
            CurrentSquare = current;
        }
    }
}
