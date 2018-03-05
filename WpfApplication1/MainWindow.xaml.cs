using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TwainDotNet;
using WiaDotNet;
using Xceed.Wpf.Toolkit;

namespace WpfApplication1
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        NotifyIcon ni;

        public MainWindow()
        {
            InitializeComponent();

            ni = new NotifyIcon();
            ni.Icon = new Icon(@"C:\_Projects\WPF\WpfApplication1\WpfApplication1\Main.ico");
            ni.Visible = true;
            ni.DoubleClick += ShowWindow;
        }


        private void ShowWindow(object sender, EventArgs args)
        {
            this.Show();
            this.WindowState = System.Windows.WindowState.Normal;
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

        private void scan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WiaManager mg = new WiaManager();

                //WIA.ImageFile img = mg.AcquireScan(mg.Devices[0], 
                var device = mg.Devices[0];
                mg.AcquireScan(device,
                    DocumentSources.SingleSided,
                    ScanTypes.Color,
                    ScanQualityTypes.None,
                    200);
                string filepatch = "";
                //img.SaveFile(filepatch);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        Twain _twain;
        private void scan_twain_Click(object sender, RoutedEventArgs e)
        {
            reportViewer.Reset();

            // Tarihi yaz
            txtTarih.Text = DateTime.Now.ToShortDateString();

            _twain = new Twain(new TwainDotNet.Wpf.WpfWindowMessageHook(this));
            _twain.TransferImage += _twain_TransferImage;
            _twain.ScanningComplete += _twain_ScanningComplete;

            var ss = new ScanSettings();
            ss.UseDuplex = true;
            //ss.ShowTwainUI = true;

            ss.Rotation = new RotationSettings()
            {
                AutomaticRotate = true
            };
            ss.Resolution = new ResolutionSettings()
            {
                ColourSetting = ColourSetting.Colour,
                Dpi = 72
            };

            _twain.StartScanning(ss);
        }
        private Bitmap MergedBitmaps(Bitmap bmp1, Bitmap bmp2)
        {
            Bitmap result = new Bitmap(bmp1.Width * 2, bmp1.Height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp1, 0, 0);
                //var destPoints = new System.Drawing.Point[];
                g.DrawImage(bmp2, bmp1.Width, 0);
            }
            return result;
        }
        private void _twain_ScanningComplete(object sender, ScanningCompleteEventArgs e)
        {
            // Tamamlandı
            //lst.Add(MergedBitmaps(lst[0], lst[1]));
            //var source = BitmapToImageSource(lst.Last());
            image1.Source = BitmapToImageSource(MainWindow.MergeTwoImagesHorizontal(lst[0], lst[1]));
            //image1.Source = source;
            //image1.Stretch = Stretch.Fill;
            //image1.Width = source.PixelWidth;
            //image1.Height = source.PixelHeight;
        }

        public static Bitmap MergeTwoImagesVertical(System.Drawing.Image firstImage, System.Drawing.Image secondImage)
        {
            if (firstImage == null)
            {
                throw new ArgumentNullException("firstImage");
            }

            if (secondImage == null)
            {
                throw new ArgumentNullException("secondImage");
            }

            int outputImageWidth = firstImage.Width > secondImage.Width ? firstImage.Width : secondImage.Width;

            int outputImageHeight = firstImage.Height + secondImage.Height + 1;

            Bitmap outputImage = new Bitmap(outputImageWidth, outputImageHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(outputImage))
            {
                graphics.DrawImage(firstImage, new System.Drawing.Rectangle(new System.Drawing.Point(), firstImage.Size),
                    new System.Drawing.Rectangle(new System.Drawing.Point(), firstImage.Size), GraphicsUnit.Pixel);
                graphics.DrawImage(secondImage, new System.Drawing.Rectangle(new System.Drawing.Point(0, firstImage.Height + 1), secondImage.Size),
                    new System.Drawing.Rectangle(new System.Drawing.Point(), secondImage.Size), GraphicsUnit.Pixel);
            }

            return outputImage;
        }


        public static Bitmap MergeTwoImagesHorizontal(System.Drawing.Image firstImage, System.Drawing.Image secondImage)
        {
            if (firstImage == null)
            {
                throw new ArgumentNullException("firstImage");
            }

            if (secondImage == null)
            {
                throw new ArgumentNullException("secondImage");
            }

            int outputImageWidth = firstImage.Width + secondImage.Width + 1;

            int outputImageHeight = firstImage.Height;

            Bitmap outputImage = new Bitmap(outputImageWidth, outputImageHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(outputImage))
            {
                g.DrawImage(firstImage,
                    new System.Drawing.Rectangle(new System.Drawing.Point(), firstImage.Size),
                    new System.Drawing.Rectangle(new System.Drawing.Point(), firstImage.Size),
                    GraphicsUnit.Pixel);
                g.DrawImage(secondImage,
                    new System.Drawing.Rectangle(new System.Drawing.Point(firstImage.Width + 1, 0), secondImage.Size),
                    new System.Drawing.Rectangle(new System.Drawing.Point(), secondImage.Size),
                    GraphicsUnit.Pixel);


                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.TranslateTransform(outputImage.Width / 2, outputImage.Height / 2);
                g.RotateTransform(30);
                String damga = "BAŞKA BİR AMAÇLA KULLANILAMAZ";
                Font font = new Font("Tahoma", 40);
                SizeF textSize = g.MeasureString(damga, font);
                g.DrawString(damga, font, System.Drawing.Brushes.Red, -(textSize.Width / 2), -(textSize.Height / 2));

                g.Flush();
            }

            return outputImage;
        }

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
        List<Bitmap> lst = new List<Bitmap>();
        int i = 1;
        private void _twain_TransferImage(object sender, TransferImageEventArgs e)
        {
            if (e.Image != null)
            {
                lst.Add(e.Image);
            }

            //if (e.Image != null)
            //{
            //    if (i == 1)
            //    {
            //        image1.Source = BitmapToImageSource(e.Image);
            //        i++;
            //    }
            //    else
            //    {
            //        image2.Source = BitmapToImageSource(e.Image);
            //        i = 1;
            //    }
            //}
        }

        private FlowDocument CreateFlowDocument()
        {
            // Create a FlowDocument
            FlowDocument doc = new FlowDocument();

            // Create a Section
            Section sec = new Section();

            // Create first Paragraph
            Paragraph p1 = new Paragraph();

            // Create and add a new Bold, Italic and Underline
            Bold bld = new Bold();
            bld.Inlines.Add(new Run("First Paragraph"));

            Italic italicBld = new Italic();
            italicBld.Inlines.Add(bld);

            Underline underlineItalicBld = new Underline();
            underlineItalicBld.Inlines.Add(italicBld);

            // Add Bold, Italic, Underline to Paragraph
            p1.Inlines.Add(underlineItalicBld);

            // Add Paragraph to Section
            sec.Blocks.Add(p1);

            // Add Section to FlowDocument
            doc.Blocks.Add(sec);

            return doc;
        }
        private void btnYazdir_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
            //if (printDialog.ShowDialog() == true)
            //{
            //    DrawingVisual dv = new DrawingVisual();

            //    var dc = dv.RenderOpen();

            //    var rect = new Rect(new System.Windows.Point(20, 20), new System.Windows.Size(350, 240));
            //    dc.DrawRoundedRectangle(System.Windows.Media.Brushes.Yellow,
            //        new System.Windows.Media.Pen(System.Windows.Media.Brushes.Purple, 2),
            //        rect, 20, 20);

            //    dc.DrawImage(image1.Source, new Rect(0, 0, image1.Width, image1.Height));
            //    dc.Close();

            //    printDialog.PrintVisual(dv, "Print");
            //}

            var musteri = new Musteri
            {
                Adres = tbAdres.Text,
                FisBelgeNo = txtFisNo.Text,
                KayitNo = txKayitNo.Text,
                Telefon = txTelefon.Text,
                //Image1 = image1.Source
            };
            ReportDataSource rds = new ReportDataSource("Dataset1",new List<Musteri> { musteri });
            reportViewer.LocalReport.DataSources.Add(rds);
            reportViewer.LocalReport.ReportEmbeddedResource = "WpfApplication1.Report2.rdlc";
            reportViewer.RefreshReport();

        }

        public void print_flowDocument()
        {
            // Create a PrintDialog
            var printDlg = new System.Windows.Controls.PrintDialog();

            // Create a FlowDocument dynamically.
            FlowDocument doc = CreateFlowDocument();
            doc.Name = "FlowDoc";

            // Create IDocumentPaginatorSource from FlowDocument
            IDocumentPaginatorSource idpSource = doc;

            // Call PrintDocument method to send document to printer
            //printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");
            printDlg.PrintVisual(this, "Hello WPF Printing.");
        }
    }
}
