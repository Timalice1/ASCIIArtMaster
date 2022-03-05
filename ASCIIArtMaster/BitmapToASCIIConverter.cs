using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIArtMaster {
    class BitmapToASCIIConverter {

        private readonly char[] _asciiTable = {' ', '.', ',', ':', '+', '*', '?', '%', '$', '#', '@'}; //Normal color
        //private readonly char[] _asciiTable = { '@', '#', '$', '%', '?', '*', '+', ':', ',', '.', ' ' }; //Inverted color (negative img)

        private readonly Bitmap _bitmap;

        public BitmapToASCIIConverter(Bitmap bitmap) {
            _bitmap = bitmap;
        }

        public char[,] Convert() {
            char[,] result = new char[_bitmap.Height,_bitmap.Width];

            //Iterating bitmap line by line
            for(int y = 0; y < _bitmap.Height; y++) {
                
                //Iterating pixels in current line
                for(int x = 0; x < _bitmap.Width; x++) {
                    int mapIndex = (int)Map(_bitmap.GetPixel(x,y).R, 0, 255, 0, _asciiTable.Length-1);//Get required char from _ascciTable
                    result[y,x] = _asciiTable[mapIndex];
                }
            }

            return result;
        }

        //Convert pixel brightness value (0-255) to _asciiTable char (0-9)
        private float Map(float valueToMap, float start1, float stop1, float start2, float stop2) {
            return ((valueToMap - start1) / (stop1 - start1)) * (stop2 - start2) + start2; 
        }
    }
}
