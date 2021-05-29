using QRCoder;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

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
            bool makeRed = false;

            if (IsRed.IsChecked == null || IsRed.IsChecked == false)
            {
                makeRed = false;
            }
            else
            {
                makeRed = true;
            }

            //var itemData = new ItemData(Id.Text, Ip.Text);
            var itemData = new ItemData();
            var data = JsonConvert.SerializeObject(itemData, Formatting.Indented);
            using var bitmap = GenerateQr(data, makeRed);
            var bitmapImage = BmpImageFromBmp(bitmap);
            ShowImage(bitmapImage);
            ViewItemData.Content = data;
        }

        private void ShowImage(BitmapImage bitmapImage)
        {
            Qr.Source = bitmapImage;
        }

        private static Bitmap GenerateQr(string str, bool isRed)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData data = qrGenerator.CreateQrCode(str, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(data);

            //return qrCode.GetGraphic(20, Color.Red, Color.White, true);

            if (isRed)
            {
                System.Console.WriteLine("RED");
                return qrCode.GetGraphic(20, Color.Red, Color.White, true);
            }
            else
            {
                System.Console.WriteLine("BW");

                return qrCode.GetGraphic(20);
            }
        }

        private static BitmapImage BmpImageFromBmp(Bitmap bmp)
        {
            using var memory = new System.IO.MemoryStream();
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