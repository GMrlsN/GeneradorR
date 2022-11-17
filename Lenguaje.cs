//Gabriel Morales Nuñez
//Requerimiento 1.- Construir un metodo para escribir en el archivo lenguaje.cs identando el codigo
//                  { incrementa un tabulador, } decrementa un tabulador
//Requerimiento 2.- Declarar un atributo "primeraProduccion" de tipo string y actualizarlo con la primmera produccion de la gramatica
//Requerimiento 3.- La primera produccion es publica y el resto privadas
using System;
using System.IO;

namespace  GeneradorR
{
public class Lenguaje : Sintaxis
    {
        string nombreProyecto;
        public Lenguaje( string nombre) : base(nombre)
        {
            nombreProyecto = "";
        }
        public Lenguaje()
        {
            nombreProyecto = "";
        }
        private void Programa(string espacioProyecto, string produccionPrincipal)
        {   
            programa.WriteLine("using System;");
            programa.WriteLine("");
            programa.WriteLine("namespace " + espacioProyecto);
            programa.WriteLine("\t{");
            programa.WriteLine("class Program");
            programa.WriteLine("\t\t{");
            programa.WriteLine("static void Main(string[] args)");
            programa.WriteLine("class Program");
            programa.WriteLine("\t\t\t{");
            programa.WriteLine("static void Main(string[] args)");
            programa.WriteLine("\t\t\t\t{");
            programa.WriteLine("try");
            programa.WriteLine("\t\t\t\t\t{");
            programa.WriteLine("using (Lenguaje a = new Lenguaje()");
            programa.WriteLine("\t\t\t\t\t\t{");
            programa.WriteLine("a." + produccionPrincipal + "();");
            programa.WriteLine("\t\t\t\t\t}");
            programa.WriteLine("\t\t\t\t}");
            programa.WriteLine("catch");
            programa.WriteLine("\t\t\t\t\t{");
            programa.WriteLine("Console.WriteLine(\"error\");");
            programa.WriteLine("\t\t\t\t}");
            programa.WriteLine("\t\t\t}");
            programa.WriteLine("\t\t}");
            programa.WriteLine("\t}");
            programa.WriteLine("}");

        }
        public void gramatica()
        {
            cabecera();
            Programa(nombreProyecto, "Programa");
            cabeceraLenguaje(nombreProyecto);
            listaProducciones();
        }
        private void cabeceraLenguaje(string espacioProyecto)
        {
            programa.WriteLine("using System;");
            programa.WriteLine("using System.IO;");
            programa.WriteLine("");
            programa.WriteLine("namespace " + espacioProyecto);
            programa.WriteLine("{");
            programa.WriteLine("public class Lenguaje : Sintaxis");
            programa.WriteLine("{");
            programa.WriteLine("string nombreProyecto;");
            programa.WriteLine("public Lenguaje( string nombre) : base(nombre)");
            programa.WriteLine("{");
            programa.WriteLine("nombreProyecto = \"\";");
            programa.WriteLine("}");
            programa.WriteLine("public Lenguaje()");
            programa.WriteLine("{");
            programa.WriteLine("nombreProyecto = \"\";");
            programa.WriteLine("}");
        }
        private void cabecera()
        {
            match("Gramatica");
            match(":");
            nombreProyecto = getContenido();
            match(Tipos.SNT);
            match(Tipos.FinProduccion);
        }
        private void listaProducciones()
        {
            lenguaje.WriteLine("private void " + getContenido() +"()");
            lenguaje.WriteLine("{");
            match(Tipos.SNT);
            match(Tipos.Produce);
            match(Tipos.FinProduccion);
            lenguaje.WriteLine("}");
            lenguaje.WriteLine();
            if(!FinArchivo())
            {
                listaProducciones();
            }
        }
    }
}