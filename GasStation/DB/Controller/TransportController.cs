using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.DB.Controller
{
    public class TransportController
    {

        public static string createTransport(string Name, int FuelVolume,Fuel fuel)
        {

            DataBaseContext context = new DataBaseContext();
            try
            {
                Transport transport = new Transport();
                transport.Name = Name;
                transport.FuelVolume = FuelVolume; 
                transport.FuelId = fuel.ID;
                context.Transports.Add(transport);
                context.SaveChanges();
                return null;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string EditTransport(Transport oldTransport, Transport newTransport)
        {
            try
            {
                DataBaseContext context = new DataBaseContext();
                var transport = context.Transports.Where(x => x.ID == oldTransport.ID).FirstOrDefault();
                if (transport != null)
                {

                    if (newTransport.Name != null)
                        transport.Name = newTransport.Name;
                    if (newTransport.FuelVolume != 0)
                        transport.FuelVolume = newTransport.FuelVolume;
                    if (newTransport.Fuel != null)
                        transport.FuelId = newTransport.Fuel.ID;
                    context.SaveChanges();
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static string Remove(Transport ttransport)
        {
            try
            {
                DataBaseContext context = new DataBaseContext();
                var transport = context.Transports.Where(x => x.ID == ttransport.ID).FirstOrDefault();
                if (transport != null)
                {
                    context.Transports.Remove(transport);
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
