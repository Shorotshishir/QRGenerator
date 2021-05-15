using QRCoder;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;

namespace QRGenerator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateQrButton_Click(object sender, RoutedEventArgs e)
        {
            var str = TextInput.Text;
            var bitmap = GenerateQr(str);
            var bitmapImage = BmpImageFromBmp(bitmap);
            ShowImage(bitmapImage);
        }

        private void ShowImage(BitmapImage bitmapImage)
        {
            Qr.Source = bitmapImage;
        }

        private static Bitmap GenerateQr(string str)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData data = qrGenerator.CreateQrCode(str, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(data);
            Bitmap qrBitmap = qrCode.GetGraphic(20);
            return qrBitmap;
        }

        private static BitmapImage BmpImageFromBmp(Bitmap bmp)
        {
            using (var memory = new System.IO.MemoryStream())
            {
                bmp.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}