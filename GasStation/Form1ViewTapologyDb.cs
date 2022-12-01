using GasStation.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GasStation
{
    public class Form1ViewTapologyDb
    {

        public static void SaveTopology(string Name, string constraction)
        {



        }

        public static void ViewTopologys(ListBox list1)
        {
            DataBaseContext context = new DataBaseContext();
            List<Topology> topologyList = context.Topologies.ToList();
            for(int i = 0; i < topologyList.Count; i++)
            {
                list1.Items.Add(topologyList[i].Name);
            }    
        }
        public static string LoadTopology(int i)
        {
            DataBaseContext context = new DataBaseContext();
            List<Topology> topologyList = context.Topologies.ToList();
            return topologyList[i].Construction;
        }
        

    }
}
