//Gabriel Morales Nuñez
using System;
using System.IO;

namespace  GeneradorR
{
public class Lenguaje : Sintaxis
    {
        public Lenguaje( string nombre) : base(nombre)
        {

        }
        public Lenguaje()
        {

        }
        public void gramatica()
        {
            cabecera();
            listaProducciones();
        }
        private void cabecera()
        {
            match("Gramatica");
            match(":");
            match(Tipos.SNT);
            match(Tipos.FinProduccion);
        }
        private void listaProducciones()
        {
            match(Tipos.SNT);
            match(Tipos.Produce);
            match(Tipos.FinProduccion);
            if(!FinArchivo())
            {
                listaProducciones();
            }
        }
    }
}