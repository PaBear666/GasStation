using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.DB.Controller
{
    public class TopologyController
    {

        public static string createTopology(string name, string constraction)
        {

            DataBaseContext context = new DataBaseContext();
            try
            {
                Topology topology  = new Topology();
                topology.Name = name;
                topology.Construction = constraction;

                context.Topologies.Add(topology);
                context.SaveChanges();
                return null;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string EditTopologyConstruction(Topology oldeTopology, string constuction)
        {
            try
            {
                DataBaseContext context = new DataBaseContext();
                var topology = context.Topologies.Where(x => x.ID == oldeTopology.ID).FirstOrDefault();
                if (topology != null)
                {
                    topology.Construction = constuction;
                    context.SaveChanges();
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static string EditTopologyName(Topology oldeTopology, string name)
        {
            try
            {
                DataBaseContext context = new DataBaseContext();
                var topology = context.Topologies.Where(x => x.ID == oldeTopology.ID).FirstOrDefault();
                if (topology != null)
                {
                    topology.Name = name;
                    context.SaveChanges();
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static string Remove(Topology rtopology)
        {
            try
            {
                DataBaseContext context = new DataBaseContext();
                var topology = context.Topologies.Where(x => x.ID == rtopology.ID).FirstOrDefault();
                if (topology != null)
                {
                    context.Topologies.Remove(topology);
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
