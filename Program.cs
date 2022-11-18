using System;

namespace GeneradorR
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (Lenguaje a = new Lenguaje("c2.gram"))
                {
                    a.gramatica();
                }
            }
            catch{
                //Console.WriteLine(e.Message);
            }
        }
    }
}
