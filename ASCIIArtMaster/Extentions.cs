using System.Drawing;

namespace ASCIIArtMaster {
    public static class Extentions {
        public static void ToGrayScale(this Bitmap bitmap) {
            /**
             * Convert bitmaps pixel colors to gray scale
             * Find the average of red, green & blue value and replace them with average value
             */
            for (int x = 0; x < bitmap.Width; x++) {
                for(int y = 0; y < bitmap.Height; y++) {
                    var pixel = bitmap.GetPixel(x, y);
                    int avg = (pixel.R+pixel.B + pixel.G)/3;
                    bitmap.SetPixel(x,y, Color.FromArgb(pixel.A, avg, avg, avg));
                }
            }
        }
    }
}
