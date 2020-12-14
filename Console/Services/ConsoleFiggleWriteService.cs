namespace Console.Services
{
    public class FiggleWriteService : IFiggleWriteService
    {
        private IWriteService service;

        public FiggleWriteService(IWriteService service)
        {
            this.service = service;
        }

        public void WriteLine(string @string)
        {
            service.WriteLine(
                Figgle.FiggleFonts.Standard.Render(@string)
                );
        }
    }
}