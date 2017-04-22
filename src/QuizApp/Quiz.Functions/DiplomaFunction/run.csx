#r "System.Drawing"
#r "Microsoft.WindowsAzure.Storage" 
#load "background.csx"

using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.WindowsAzure.Storage.Blob; 


public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log, CloudBlockBlob outputBlob)
{
    log.Info("C# HTTP trigger function processing a request.");
    // parse query parameter
    string text = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "text", true) == 0).Value;
    string name = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0).Value;
    string score = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "score", true) == 0).Value;

    // Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();

    // Set name to query string or body data
    text = text ?? data?.text ?? "Missing text";
    name = name ?? data?.name ?? "Anon";
    score = score ?? data?.score ?? "0 points";
  
    if(name == null) {
        return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body");
    }
        
           //The followig does not work as working directory != this folder!
            //const string imagePath = @"2017-logo-800x540-pokal.png";
            //Image image = Image.FromFile(imagePath);

            Image image = BackgroundImage();
            var brush = Brushes.OrangeRed;
       using (Graphics graphics = Graphics.FromImage(image))
            {
                using (Font arialFont = new Font("Arial", 36))
                {
                    graphics.DrawString(text, arialFont, brush, new PointF(10f, 50f));
                    graphics.DrawString(name, arialFont, brush, new PointF(50f, 110f));
                    graphics.DrawString(score, arialFont, brush, new PointF(50f, 180f));
                }
            }
         log.Info("Image generated");
           
            var tempPath = Path.GetTempPath();
            var outputFileName = "temp.png"; 
            var filePath = tempPath + outputFileName;
            image.Save(filePath, ImageFormat.Png);
            using (var fileStream = System.IO.File.OpenRead(filePath))
        {
            log.Info("Upload starting");
            outputBlob.UploadFromStream(fileStream);
            log.Info("Uploaded");
        }
            
            File.Delete(filePath);
            log.Info("Deleted");

    //Set the expiry time and permissions for the blob.
    //In this case the start time is specified as a few minutes in the past, to mitigate clock skew.
    //The shared access signature will be valid immediately.
    SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
    sasConstraints.SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5);
    sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24);
    sasConstraints.Permissions = SharedAccessBlobPermissions.Read;

    //Generate the shared access signature on the blob, setting the constraints directly on the signature.
    string sasBlobToken = outputBlob.GetSharedAccessSignature(sasConstraints);

    //Return the URI string for the container, including the SAS token.
    //return outputBlob.Uri + sasBlobToken;

    return req.CreateResponse(HttpStatusCode.OK, "url: " + outputBlob.Uri + sasBlobToken);
}

