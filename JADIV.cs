using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(TA.JADIV.JADIV),
    typeof(VisualizerObjectSource),
    Target = typeof(Bitmap),
    Description = "JADIV"
)]

namespace TA.JADIV
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
            form.BackgroundImage = CreateChessPattern(9, Color.White, Color.LightGray);
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

        private Bitmap CreateChessPattern(byte Size, Color Color1, Color Color2)
        {
            Bitmap chess = new Bitmap(Size, Size);
            Brush Brush1 = new SolidBrush(Color1);
            Brush Brush2 = new SolidBrush(Color2);
            using (Graphics g = Graphics.FromImage(chess))
            {
                g.FillRectangle(Brush1, 0, 0, Size, Size);
                g.FillRectangle(Brush1, Size, Size, Size, Size);
                g.FillRectangle(Brush2, Size, 0, Size, Size);
                g.FillRectangle(Brush2, 0, Size, Size, Size);
            }
            return chess;
        }
    }
}