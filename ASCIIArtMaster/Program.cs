using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ASCIIArtMaster {
    internal class Program {

        [STAThread]
        static void Main(string[] args) {

            var openFileDilaog = new OpenFileDialog {
                Filter = "Images | *.bmp; *.png; *.jpeg; *.JPEG; *.jpg"
            };

            while (true) {
                Console.ReadLine();

                if (openFileDilaog.ShowDialog() != DialogResult.OK)
                    continue;

                Console.Clear();

                var bitmap = new Bitmap(openFileDilaog.FileName);
                bitmap = Resize(bitmap);
                bitmap.ToGrayScale();

            }
        }

        private static Bitmap Resize(Bitmap bitmap) {
            var maxWidth = 350;
            var newHeight = bitmap.Height/1.5 * maxWidth/bitmap.Width;
            if (bitmap.Width > maxWidth || bitmap.Height > newHeight)
                bitmap = new Bitmap(bitmap, new Size(maxWidth, (int)newHeight));
            return bitmap;
        }
    }
}
