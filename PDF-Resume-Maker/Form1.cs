using SkiaSharp;
using System.Text.Json;
using System.Diagnostics;

namespace PDF_Resume_Maker
{
    public partial class ResumeMakerForm : Form
    {
        public ResumeMakerForm()
        {
            InitializeComponent();
        }

        private string filepath = "";
        private Resume_Data resumedata;
        private List<SKPicture>? pages;
        private ResumeRenderer renderer = new();

        private void ChooseFilebutton_Click(object sender, EventArgs e)
        {
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "C:\\";
                    openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {

                        var fileStream = openFileDialog.OpenFile();
                        try
                        {
                            resumedata = JsonSerializer.Deserialize<Resume_Data>(fileStream);
                            pages = renderer.RenderResume(resumedata);
                            skControl1.Invalidate();
                        }
                        catch (JsonException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                }


            }
        }

        private void SaveFilebutton_Click(object sender, EventArgs e)
        {
            if (pages is not null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF documents (*.pdf)|*.pdf|All files (*.*)|*.*";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var filename = saveFileDialog.FileName;
                        using (var pdfDocument = SKDocument.CreatePdf(filename))
                        {
                            var pageCanvas = pdfDocument.BeginPage(8.5F * 72, 11F * 72);
                            var page = pages.First();
                            pageCanvas.DrawPicture(page);
                            pdfDocument.EndPage();
                            pdfDocument.Close();
                        }
                    }
                }
            }
        }

        private void skControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            if (pages is not null)
            {
                var firstpage = pages.First();
                var canvasSize = skControl1.CanvasSize;
                var scaleMatrix = SKMatrix.CreateScale(canvasSize.Width / (8.5F * 72), canvasSize.Height / (11.5F * 72));
                e.Surface.Canvas.DrawRect(0, 0, canvasSize.Width, canvasSize.Height, new SKPaint { Color = new(255, 255, 255) });
                e.Surface.Canvas.DrawPicture(firstpage, ref scaleMatrix, new SKPaint { IsAntialias = true });
            }
        }
    }
}