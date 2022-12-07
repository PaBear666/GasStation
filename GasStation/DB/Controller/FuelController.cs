using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.DB.Controller
{
    public class FuelController
    {
        public static string createFuel(string fuelType, double cost)
        {

            DataBaseContext context = new DataBaseContext();
            try
            {
                Fuel fuel = new Fuel();
                fuel.Type = fuelType;
                fuel.Cost = cost;
                
                context.Fuels.Add(fuel);
                context.SaveChanges();
                return null;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
     
        public static string EditFuel(Fuel oldeFuel, Fuel newFuel)
        {
            try
            {
                DataBaseContext context = new DataBaseContext();
                var fuel = context.Fuels.Where(x => x.ID == oldeFuel.ID).FirstOrDefault();
                if (fuel != null)
                {
                    
                    if (newFuel.Type != null)
                        fuel.Type = newFuel.Type;
                    if (newFuel.Cost != 0)
                        fuel.Cost = newFuel.Cost;
                        context.SaveChanges();
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static string Remove(Fuel ffuel)
        {
            try
            {
                DataBaseContext context = new DataBaseContext();
                var fuel = context.Fuels.Where(x => x.ID == ffuel.ID).FirstOrDefault();
                if (fuel != null)
                {
                    context.Fuels.Remove(fuel);
                    context.SaveChanges();
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
