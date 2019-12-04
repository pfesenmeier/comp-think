using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramRun
{

public class Programmer 
{
    public List<string> Languages { get; set; } = new List<string>();
    public void AddLanguage(string language)
    {
        Languages.Add(language);
    } 
}

public class ProgrammerTeacher: Programmer
{
    public bool Teach(Programmer programmer, string language)
    {
        if (Languages.Contains(language))
        {
            programmer.AddLanguage(language);
            return true;
        }
        else
        {
            return false;
        }
    }
}

    public class Program
    {
        public static void Main(string[] args)
        {
                  ProgrammerTeacher teacher = new ProgrammerTeacher();
                  teacher.AddLanguage("C#");

                 Programmer programmer = new Programmer();
                 teacher.Teach(programmer, "C#");

                  foreach (var language in programmer.Languages)
                      Console.WriteLine(language);
        }
    }
}
