namespace InstantJSONExample
{
    class Test
    {
        static void Main(string[] args)
        {
            PSPDFKit.Sdk.InitializeTrial();

            var test = new InstantJson();
            test.RunInstantJsonConcurrently();
            Console.WriteLine("Done!");
        }
    }
}