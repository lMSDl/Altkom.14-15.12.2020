namespace Console.Services
{
    public class ConsoleWriteService : IWriteService
    {
        public virtual void WriteLine(string @string)
        {
            System.Console.WriteLine(@string);
        }
    }
}