using GasStation.DB;
using GasStation.DB.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GasStation
{
    public class ViewTapologyDb
    {

     

        public static void ViewTopologys(ListBox list1)
        {
            
            list1.Items.Clear();
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
        public static void SaveTopology(ListBox listBox,string newConstrution)
        {
            int i = listBox.SelectedIndex;
            DataBaseContext context = new DataBaseContext();
            List<Topology> topologyList = context.Topologies.ToList();
            if(topologyList[i].Construction!=newConstrution)
                TopologyController.EditTopologyConstruction(topologyList[i],newConstrution);
        }
        public static void AddTopologytoNew(ListBox listBox, string newConstrution)
        {
            int i = listBox.SelectedIndex;
            DataBaseContext context = new DataBaseContext();
            List<Topology> topologyList = context.Topologies.ToList();
            TopologyController.EditTopologyConstruction(topologyList[topologyList.Count-1], newConstrution);
        }
        public static void RemoveTopology(ListBox listBox)
        {
            int i = listBox.SelectedIndex;
            if (listBox.SelectedIndex != -1)
            {
                listBox.Items.RemoveAt(i);
                DataBaseContext context = new DataBaseContext();
                List<Topology> topologyList = context.Topologies.ToList();
                TopologyController.Remove(topologyList[i]);
            }
        }

    }
}
