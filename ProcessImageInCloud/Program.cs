using ImageProcessor;
using Microsoft.Azure.Jobs;
using nQuant;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessImageInCloud
{
    // To learn more about Microsoft Azure WebJobs, please see http://go.microsoft.com/fwlink/?LinkID=401557
    class Program
    {
        static void Main()
        {
            JobHost host = new JobHost();
            host.RunAndBlock();
        }
        public static void SquishNewlyUploadedPNGs(
            [BlobTrigger("processimageincloud/{name}")] Stream input,
            [Blob("processimageincloudoutput/{name}", FileAccess.Write)] Stream output)
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
