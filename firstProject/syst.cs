using System;

using System.IO;
public class Syst
{
    public string readMessage()
    {
        Console.Write("Write your name: ");
        string mess = Console.ReadLine();
        if (!string.IsNullOrEmpty(mess))
        {
            Console.WriteLine($"Hello {mess}!");
        }
        else
        {
            Console.WriteLine("Hello stranger");
        }
        return mess;

    }
}

