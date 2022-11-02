using GasStation.GraphicEngine.Common;
using System;
using System.Windows.Forms;

namespace GasStation.LifeEngine
{
    public class AppliancePictureBox : Square
    {
        private LifeAppliance _appliance;
        public LifeAppliance Appliance
        {
            get
            {
                return _appliance;
            }

            set
            {
                _appliance = value;
                _pictureBox.Image = value.ViewComponent.Image;
                _pictureBox.BackColor = value.ViewComponent.Color;
            }
        }
        public event EventHandler<EventArgs> EndDragDrop;
        public event EventHandler<DragAndDropData<LifeAppliance>> StartDrop;

        public AppliancePictureBox(EditorProvider editorProvider, Appliance appliance, PictureBox pictureBox) : base(pictureBox)
        {
            Appliance = editorProvider.Appliance[appliance];
            _pictureBox.Image = Appliance.ViewComponent.Image;
            _pictureBox.BackColor = Appliance.ViewComponent.Color;

            Control.AllowDrop = true;
            Control.MouseUp += (sender, e) =>
            {
                if(e.Button == MouseButtons.Right)
                {
                    switch (Appliance.Appliance.Side)
                    {
                        case Side.Top:
                            Appliance = editorProvider.Appliance[new Appliance(Appliance.Appliance.Type, Side.Right)];
                            break;
                        case Side.Right:
                            Appliance = editorProvider.Appliance[new Appliance(Appliance.Appliance.Type, Side.Bottom)];
                            break;
                        case Side.Bottom:
                            Appliance = editorProvider.Appliance[new Appliance(Appliance.Appliance.Type, Side.Left)];
                            break;
                        case Side.Left:
                            Appliance = editorProvider.Appliance[new Appliance(Appliance.Appliance.Type, Side.Top)];
                            break;
                        default:
                            break;
                    }
                }    
            };

            Control.MouseDown += (sender, e) =>
            {
                if(e.Button == MouseButtons.Left)
                {
                    Control.DoDragDrop(new DragAndDropData<LifeAppliance>(Appliance, null), DragDropEffects.Move);
                }              
            };




            Control.QueryContinueDrag += Control_QueryContinueDrag;


        }



        private void Control_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            if(e.Action == DragAction.Continue)
            {
                StartDrop.Invoke(sender, new DragAndDropData<LifeAppliance>(Appliance, null));
            }

            if(e.Action == DragAction.Drop)
            {
                EndDragDrop.Invoke(sender, e);
            }
        }
    }
}
