namespace API.Custom_Logging
{
    public class CustomLogger : ICustomLogger
    {
        public void Log(string message, string type)
        {
            if(type == "error")
            {
                Console.WriteLine("Error - " + message);
                Console.BackgroundColor = ConsoleColor.Red;

            }
            else
            {
                Console.WriteLine(message);
            }
        }
    }
}
