//Gabriel Morales Nuñez
using System;
using System.IO;

namespace GeneradorR
{
    public class Error : Exception
    {
        public Error(string mensaje, StreamWriter log) : base(mensaje)
        {
            //Console.WriteLine(mensaje);
            log.WriteLine(mensaje);
        }
    }
}