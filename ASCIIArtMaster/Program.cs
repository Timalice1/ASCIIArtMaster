using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ASCIIArtMaster {
    internal class Program {
        private const double SCALE = 1.5;
        private const int MAX_WIDTH = 474; //Max 474, console font size = 8pt

        [STAThread]
        static void Main(string[] args) {


            //Open fileDialog & filter files by img extentions 
            var openFileDilaog = new OpenFileDialog {
                Filter = "Images | *.bmp; *.png; *.jpeg; *.JPEG; *.jpg; *.gif"
            };


            while (true) {
                Console.WriteLine("Press enter to start...");
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

                //Convert img to ascii
                var converter = new BitmapToASCIIConverter(bitmap);
                var rows = converter.Convert();

                //Show converted img in console
                for(int y = 0; y < rows.GetLength(0); y++) {
                    for (int x = 0; x < rows.GetLength(1); x++) {
                        Console.Write(rows[y,x]);
                    }
                    Console.WriteLine();
                }

                Console.SetCursorPosition(0, 0);

            }
        }

        //Set for opened image console size
        private static Bitmap Resize(Bitmap bitmap) {
            var newHeight = bitmap.Height/SCALE * MAX_WIDTH/bitmap.Width;
            if (bitmap.Width > MAX_WIDTH || bitmap.Height > newHeight)
                //Resize bitmap
                bitmap = new Bitmap(bitmap, new Size(MAX_WIDTH, (int)newHeight));
            return bitmap;
        }
    }
}
