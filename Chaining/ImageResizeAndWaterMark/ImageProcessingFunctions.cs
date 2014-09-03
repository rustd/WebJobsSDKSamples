using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace ImageResizeAndWaterMark
{
    class ImageProcessingFunctions
    {
        public static void Resize(
        [BlobTrigger(@"images-input/{name}")] WebImage input,
        [Blob(@"images2-output/{name}")] out WebImage output)
        {
            var width = 80;
            var height = 80;
            output = input.Resize(width, height);
        }
        public static void WaterMark(
        [BlobTrigger(@"images2-output/{name}")] WebImage input,
        [Blob(@"images2-newoutput/{name}")] out WebImage output)
        {
            output = input.AddTextWatermark("WebJobs is now awesome!!!!", fontSize: 6);
        }
    }
    public class WebImageBinder : ICloudBlobStreamBinder<WebImage>
    {  
        public Task<WebImage> ReadFromStreamAsync(System.IO.Stream input, System.Threading.CancellationToken cancellationToken)
        {
            return Task.FromResult(new WebImage(input));
        }

        public async Task WriteToStreamAsync(WebImage result, System.IO.Stream output, System.Threading.CancellationToken cancellationToken)
        {
            var bytes = result.GetBytes();
            await output.WriteAsync(bytes, 0, bytes.Length,cancellationToken);
        }
    }
}
