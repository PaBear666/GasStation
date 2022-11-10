using GasStation.GraphicEngine.Common;
using System;
using System.Windows.Forms;

namespace GasStation.LifeEngine
{
    public class AppliancePictureBox : Square, IDisposable
    {
        private LifeAppliance _appliance;
        private EditorProvider _editorProvider;
        public Func<ApplianceType, bool> CanDoDrag { get; set; }
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
        public event EventHandler<ApplianceType> EndDragDropApplianceType;
        public event EventHandler<EventArgs> EndDragDrop;
        public event EventHandler<DragAndDropData<LifeAppliance>> StartDrop;

        public AppliancePictureBox(
            EditorProvider editorProvider,
            Func<ApplianceType, bool> canDoDrag,
            Appliance appliance,
            PictureBox pictureBox) : base(pictureBox)
        {
            _editorProvider = editorProvider;
            Appliance = editorProvider.Appliance[appliance];
            _pictureBox.Image = Appliance.ViewComponent.Image;
            _pictureBox.BackColor = Appliance.ViewComponent.Color;

            CanDoDrag = canDoDrag;
            Control.AllowDrop = true;
            Control.MouseUp += MouseUp;
            Control.MouseDown += MouseDown;
            Control.QueryContinueDrag += Control_QueryContinueDrag;
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && CanDoDrag(Appliance.Appliance.Type))
            {
                Control.DoDragDrop(new DragAndDropData<LifeAppliance>(Appliance, null), DragDropEffects.Move);
            }
        }

        private void MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                switch (Appliance.Appliance.Side)
                {
                    case Side.Top:
                        Appliance = _editorProvider.Appliance[new Appliance(Appliance.Appliance.Type, Side.Right)];
                        break;
                    case Side.Right:
                        Appliance = _editorProvider.Appliance[new Appliance(Appliance.Appliance.Type, Side.Bottom)];
                        break;
                    case Side.Bottom:
                        Appliance = _editorProvider.Appliance[new Appliance(Appliance.Appliance.Type, Side.Left)];
                        break;
                    case Side.Left:
                        Appliance = _editorProvider.Appliance[new Appliance(Appliance.Appliance.Type, Side.Top)];
                        break;
                    default:
                        break;
                }
            }
        }

        public override void Dispose()
        {
            Control.MouseUp -= MouseUp;
            Control.MouseDown -= MouseDown;
            Control.QueryContinueDrag -= Control_QueryContinueDrag;
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
