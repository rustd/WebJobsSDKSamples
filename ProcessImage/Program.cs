using nQuant;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessImage
{
    // To learn more about Microsoft Azure WebJobs, please see http://go.microsoft.com/fwlink/?LinkID=401557
    class Program
    {
        static void Main()
        {
        }
        public static void SquishNewlyUploadedPNGs(
            Stream input, 
            Stream output)
        {
            var quantizer = new WuQuantizer();
            using (var bitmap = new Bitmap(input))
            {
                using (var quantized = quantizer.QuantizeImage(bitmap))
                {
                    quantized.Save(output, ImageFormat.Png);
                }
            }
        }
    }
}
