﻿using GasStation.ConstructorEngine;
using System;

namespace GasStation.SimulatorEngine.ApplianceSimulators
{
    public class ShopSimulator : ApplianceSimulator
    {
        public ShopSimulator(LifeSquare applianceSquare, SimulatorSquare usedSquare) : base(applianceSquare, usedSquare)
        {
        }

        public override void UseSquare()
        {
            throw new NotImplementedException();
        }
    }
}
