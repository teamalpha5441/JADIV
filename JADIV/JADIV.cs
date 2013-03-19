using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: System.Diagnostics.DebuggerVisualizer(
    typeof(TEAM_ALPHA.JADIV.JADIV),
    typeof(VisualizerObjectSource),
    Target = typeof(Bitmap),
    Description = "JADIV"
)]

namespace TEAM_ALPHA.JADIV
{
    public class JADIV : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            Image image = (Image)objectProvider.GetObject();

            Form form = new Form();
            form.Text = string.Format("JADIV - Image {0}x{1}", image.Width, image.Height);
            form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            form.ClientSize = CalculateSize(image.Size, new Size(300, 300));

            PictureBox pb = new PictureBox();
            pb.Image = image;
            pb.Parent = form;
            pb.Dock = DockStyle.Fill;
            pb.SizeMode = PictureBoxSizeMode.Zoom;

            windowService.ShowDialog(form);
        }

        private Size CalculateSize(Size Size, Size MaxSize)
        {
            float num = Math.Min((float)MaxSize.Width / (float)Size.Width, (float)MaxSize.Height / (float)Size.Height);
            return new Size(
                (int)(Size.Width * num),
                (int)(Size.Height * num)
            );
        }
    }
}