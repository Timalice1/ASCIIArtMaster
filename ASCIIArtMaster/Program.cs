using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace ASCIIArtMaster {
    internal class Program {
        private const double SCALE = 2;
        private const int MAX_WIDTH = 350; //Max 474, console font size = 8pt
        private static string filePath = null;

        [STAThread]
        static void Main(string[] args) {


            //Open fileDialog & filter files by img extentions 
            var openFileDilaog = new OpenFileDialog {
                Filter = "Images | *.bmp; *.png; *.jpeg; *.JPEG; *.jpg; *.gif"
            };


            while (true) {
                //Clear console from previous img
                Console.Clear();

                Console.WriteLine("Press enter to start...");
                Console.ReadLine();

                if (openFileDilaog.ShowDialog() != DialogResult.OK)
                    continue;
                
                //Create new bitmap from opened file
                filePath = openFileDilaog.FileName;
                var bitmap = new Bitmap(filePath);

                //Resize and conver to gray scale
                bitmap = Resize(bitmap);
                bitmap.ToGrayScale();

                //Convert img to ascii
                var converter = new BitmapToASCIIConverter(bitmap);
                var img = converter.Convert();

                //Show converted img in console
                for(int y = 0; y < img.GetLength(0); y++) {
                    for (int x = 0; x < img.GetLength(1); x++) {
                        Console.Write(img[y,x]);
                    }
                    Console.WriteLine();
                }


                Console.SetCursorPosition(0, 0);

                Console.ReadLine();

                var res = MessageBox.Show("Save this art to txt file?", "Save", MessageBoxButtons.OKCancel);
                if (res == DialogResult.OK)
                    //Save negative img to file
                    SaveArt(converter.ConvertNegative());
                else continue;
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
    
        //Save art to txt file
        private static void SaveArt(char[,] art) {
            FolderBrowserDialog selectFolder = new FolderBrowserDialog();

            string path = null;
            if(selectFolder.ShowDialog() == DialogResult.OK) {
                path = selectFolder.SelectedPath;
            }

            string tmp = filePath.Remove(0, filePath.LastIndexOf("\\") + 1);
            string fileName = tmp.Remove(tmp.LastIndexOf("."));
            string destination = path + "\\" + fileName + ".txt";

            using(StreamWriter sw = new StreamWriter(destination)) {
               for(int y = 0; y < art.GetLength(0); y++) {
                    for (int x = 0; x < art.GetLength(1); x++) {
                        sw.Write(art[y,x]);
                    }
                    sw.WriteLine();
                }
            }

            MessageBox.Show($"Sucessfully saved to\n{destination}");
        }
    
    }
}
