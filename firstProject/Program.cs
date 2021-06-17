using System;
namespace firstProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            Syst system = new Syst();
            string username = system.readMessage();
            if (string.IsNullOrEmpty(username))
            {
                throw new NullReferenceException("Error");
            }

            Console.WriteLine($"The username is {username}");
        }

    }
}
