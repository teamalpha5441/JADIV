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
            form.Text = string.Format("JADIV - {0}x{1} {2} PaletteEntries: {3}", image.Width, image.Height, image.PixelFormat.ToString(), image.Palette.Entries.Length);
            form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            form.ClientSize = CalculateSize(image.Size, new Size(300, 300));
            form.BackgroundImage = CreateChessPattern(new Size(18, 18), Color.White, Color.LightGray);
            PictureBox pb = new PictureBox();
            pb.Image = (Image)image.Clone();
            pb.Parent = form;
            pb.Dock = DockStyle.Fill;
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.BackColor = Color.Transparent;
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

        private Bitmap CreateChessPattern(Size Size, Color Color1, Color Color2)
        {
            if (Size.Width % 2 > 0 || Size.Height % 2 > 0)
                throw new ArgumentException("Only even size possible");
            Bitmap chess = new Bitmap(Size.Width, Size.Height);
            Brush Brush1 = new SolidBrush(Color1);
            Brush Brush2 = new SolidBrush(Color2);
            Size = new Size(Size.Width / 2, Size.Height / 2);
            using (Graphics g = Graphics.FromImage(chess))
            {
                g.FillRectangle(Brush1, 0, 0, Size.Width, Size.Height);
                g.FillRectangle(Brush1, Size.Width, Size.Height, Size.Width, Size.Height);
                g.FillRectangle(Brush2, Size.Width, 0, Size.Width, Size.Height);
                g.FillRectangle(Brush2, 0, Size.Height, Size.Width, Size.Height);
            }
            return chess;
        }
    }
}