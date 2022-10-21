using GasStation.GraphicEngine.Common;
using System;
using System.Windows.Forms;

namespace GasStation.LifeEngine
{
    public class AppliancePictureBox : Square
    {
        public Appliance Appliance { get; }
        public event EventHandler<EventArgs> EndDragDrop;
        public event EventHandler<DragAndDropData<Appliance>> StartDrop;
        public AppliancePictureBox(Appliance appliance, PictureBox pictureBox) : base(pictureBox)
        {
            Appliance = appliance;
            _pictureBox.Image = appliance.ViewComponent.Image;
            _pictureBox.BackColor = appliance.ViewComponent.Color;

            Control.AllowDrop = true;
            Control.MouseDown += (object sender, MouseEventArgs e) =>
            {
                Control.DoDragDrop(new DragAndDropData<Appliance>(Appliance, null), DragDropEffects.Move);
            };

            Control.DragDrop += (object sender, DragEventArgs e) =>
            {
                
            };

            Control.QueryContinueDrag += Control_QueryContinueDrag;
        }

        private void Control_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            if(e.Action == DragAction.Continue)
            {
                StartDrop.Invoke(sender, new DragAndDropData<Appliance>(Appliance, null));
            }

            if(e.Action == DragAction.Drop)
            {
                EndDragDrop.Invoke(sender, e);
            }
        }
    }
}
