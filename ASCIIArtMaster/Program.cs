using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ASCIIArtMaster {
    internal class Program {

        [STAThread]
        static void Main(string[] args) {

            //Open fileDialog & filter files by img extentions 
            var openFileDilaog = new OpenFileDialog {
                Filter = "Images | *.bmp; *.png; *.jpeg; *.JPEG; *.jpg"
            };


            while (true) {
                Console.ReadLine();

                if (openFileDilaog.ShowDialog() != DialogResult.OK)
                    continue;

                //Clear console from previous img
                Console.Clear();

                //Create new bitmap from opened file
                var bitmap = new Bitmap(openFileDilaog.FileName);

                //Resize and conver to gray scale
                bitmap = Resize(bitmap);
                bitmap.ToGrayScale();

            }
        }

        //Set for opened image console size
        private static Bitmap Resize(Bitmap bitmap) {
            var maxWidth = 350;
            var newHeight = bitmap.Height/1.5 * maxWidth/bitmap.Width;
            if (bitmap.Width > maxWidth || bitmap.Height > newHeight)
                //Resize bitmap
                bitmap = new Bitmap(bitmap, new Size(maxWidth, (int)newHeight));
            return bitmap;
        }
    }
}
