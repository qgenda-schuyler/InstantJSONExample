using InstantJSONExample.Helper;
using PSPDFKit;
using PSPDFKit.Providers;

namespace InstantJSONExample
{
    public class InstantJson
    {
        private static Mutex psPdfKitImportDocumentAndSaveMutexLock = new Mutex();

        public void RunInstantJsonConcurrently()
        {
            Thread[] threads = new Thread[300];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(this.ApplyInstantJson);
            }

            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }

        private void ApplyInstantJson()
        {
            var docStream = File.Open(DocumentHelper.GetAssetPath("default.pdf"), FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            var document = new Document(new StreamDataProvider(docStream));

            psPdfKitImportDocumentAndSaveMutexLock.WaitOne();
            try
            {
                Console.WriteLine("Obtained lock and started writing JSON");
                var jsonStream = File.Open(DocumentHelper.GetAssetPath("instant.json"), FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                document.ImportDocumentJson(new StreamDataProvider(jsonStream));

                Random rnd = new Random();
                var tempPath = Path.Combine(Path.GetTempPath(), "default" + rnd.Next(10000000).ToString() + ".pdf");
                using (var tempStream = File.Create(tempPath))
                {
                    document.SaveAs(new StreamDataProvider(tempStream), new DocumentSaveOptions());
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                Console.WriteLine("Releasing lock");
                psPdfKitImportDocumentAndSaveMutexLock.ReleaseMutex();
            }
        }
    }
}