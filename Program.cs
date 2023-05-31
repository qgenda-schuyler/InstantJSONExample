using System.Diagnostics;
using PSPDFKit;
using PSPDFKit.Providers;
using System.Drawing.Imaging;
using InstantJSONExample.Helper;

namespace InstantJSONExample
{
    class Test
    {
        static void Main(string[] args)
        {
            PSPDFKit.Sdk.InitializeTrial();

            for (var i = 0; i < 100; i++)
            {
                var docStream = File.Open(DocumentHelper.GetAssetPath("default.pdf"), FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                var document = new Document(new StreamDataProvider(docStream));

                var jsonStream = File.Open(DocumentHelper.GetAssetPath("instant.json"), FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                document.ImportDocumentJson(new StreamDataProvider(jsonStream));

                var tempPath = Path.Combine(Path.GetTempPath(), "default.pdf");
                var tempStream = File.Open(tempPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                document.SaveAs(new StreamDataProvider(tempStream), new DocumentSaveOptions());
            }
            Console.WriteLine("Done!");
        }
    }
}