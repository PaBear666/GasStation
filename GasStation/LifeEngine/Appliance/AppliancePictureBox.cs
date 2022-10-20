using GasStation.GraphicEngine.Common;
using System.Windows.Forms;

namespace GasStation.LifeEngine
{
    public class AppliancePictureBox : Square
    {
        Appliance Appliance { get; }
        public AppliancePictureBox(Appliance appliance, PictureBox pictureBox) : base(pictureBox)
        {
            Appliance = appliance;
            _pictureBox.Image = appliance.ViewComponent.Image;
            _pictureBox.BackColor = appliance.ViewComponent.Color;

            Control.AllowDrop = true;
            Control.MouseDown += (object sender, MouseEventArgs e) =>
            {
                Control.DoDragDrop(new DragAndDropData<Appliance>(Appliance, () => { }), DragDropEffects.Move);
            };
        }
    }
}
