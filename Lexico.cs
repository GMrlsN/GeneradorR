//Gabriel Morales Nuñez
using System;
using System.IO;

namespace GeneradorR
{
    public class Lexico : Token, IDisposable
    {
        protected StreamReader archivo;
        protected StreamWriter log;
        protected StreamWriter lenguaje;
        protected StreamWriter programa;
        const int F = -1;
        const int E = -2;
        long contador = 0;
        protected int linea;
        int[,] TRAND = new int[,]
        {
            {0,1,5,3,4,5},
            {F,F,2,F,F,F},
            {F,F,F,F,F,F},
            {F,F,F,3,F,F},
            {F,F,F,F,F,F},
            {F,F,F,F,F,F}
        };
        private bool disposedValue;

        protected void setLinea(int lin){
            linea = lin;
        }
        protected int getLinea(){
            return linea;
        }
        public Lexico()
        {
            linea = 1;
            string path;
            bool windows = false;
            if(windows)
            {
                path = "C:\\Users\\gabri\\OneDrive\\Documents\\ITQ\\Materias\\Lenguajes y Automatas II\\Semantica\\prueba.cpp";
                ////asm = new StreamWriter("C:\\Users\\gabri\\OneDrive\\Documents\\ITQ\\Materias\\Lenguajes y Automatas II\\Semantica\\prueba.//asm");
                log = new StreamWriter("C:\\Users\\gabri\\OneDrive\\Documents\\ITQ\\Materias\\Lenguajes y Automatas II\\Semantica\\prueba.Log");
                lenguaje = new StreamWriter("C:\\Users\\gabri\\OneDrive\\Documents\\ITQ\\Materias\\Lenguajes y Automatas II\\Semantica\\Generador\\c2.Log");
            }
            else
            {
                path = "/workspace/GeneradorR/prueba.cpp";
                log = new StreamWriter("/workspace/GeneradorR/prueba.Log");
                ////asm = new StreamWriter("/workspace/Semantica/prueba.//asm");
                lenguaje = new StreamWriter("/workspace/GeneradorR/Generico/c2.gra");

            }
            //string path = "C:\\Users\\gabri\\OneDrive\\Documents\\ITQ\\Materias\\Lenguajes y Automatas II\\Semantica\\prueba.cpp";
            bool existencia = File.Exists(path);
            //log = new StreamWriter("C:\\Users\\gabri\\OneDrive\\Documents\\ITQ\\Materias\\Lenguajes y Automatas II\\Semantica\\prueba.Log");
            log.AutoFlush = true;
            ////asm = new StreamWriter("C:\\Users\\gabri\\OneDrive\\Documents\\ITQ\\Materias\\Lenguajes y Automatas II\\Semantica\\prueba.//asm");
            lenguaje.AutoFlush = true;
            //log.WriteLine("Primer constructor");
            log.WriteLine("Archivo: prueba.cpp");
            log.WriteLine(DateTime.Now);

            ////asm.WriteLine(";Archivo: prueba.//asm");
            ////asm.WriteLine(";fecha: " + DateTime.Now);
            //Investigar como checar si un archivo existe o no existe 
            if (existencia == true)
            {
                archivo = new StreamReader(path);
            }
            else
            {
                throw new Error("Error: El archivo prueba no existe", log);
            }
        }
        public Lexico(string nombre)
        {
            linea = 1;
            string pathLog = Path.ChangeExtension(nombre, ".log");
            log = new StreamWriter(pathLog); 
            log.AutoFlush = true;
            string pathLenguaje = Path.ChangeExtension(nombre, ".gra");
            lenguaje = new StreamWriter(pathLenguaje);
            lenguaje.AutoFlush = true;
            //log.WriteLine("Segundo constructor");
            log.WriteLine("Archivo: "+nombre);
            log.WriteLine("fecha: "+ DateTime.Now);

            ////asm.WriteLine(";Archivo: "+ nombre);
            ////asm.WriteLine(";feccha: " + DateTime.Now);
            if (File.Exists(nombre))
            {
                archivo = new StreamReader(nombre);
            }
            else
            {
                throw new Error("Error: El archivo " +Path.GetFileName(nombre)+ " no existe ", log);
            }
        }
        public void cerrar()
        {
            archivo.Close();
            log.Close();
            ////asm.Close();
        }       

        private void clasifica(int estado)
        {
            switch(estado)
            {
                case 1:
                    setClasificacion(Tipos.ST);
                    break;
                case 2:
                    setClasificacion(Tipos.Produce);
                    break;
                case 3:
                    setClasificacion(Tipos.SNT);
                    break;
                case 4:
                    setClasificacion(Tipos.FinProduccion);
                    break;
                case 5:
                    setClasificacion(Tipos.ST);
                    break;

            } 
        }
        private int columna(char c)
        {
            if(c == 10)
            {
                return 4;
            }
            else if(char.IsWhiteSpace(c))
            {
                return 0;
            }
            else if(c == '-')
            {
                return 1;
            }
            else if(c == '>')
            {
                return 2;
            }
            else if(char.IsLetter(c))
            {
                return 3;
            }
            return 5;
        }
        //WS,EF,EL,L, D, .,	E, +, -, =,	:, ;, &, |,	!, >, <, *,	%, /, ", ?,La, ', #
        public void NextToken() 
        {
            string buffer = "";           
            char c;      
            int estado = 0;
            while(estado >= 0)
            {
                c = (char)archivo.Peek(); //Funcion de transicion
                estado = TRAND[estado,columna(c)];
                clasifica(estado);
                if (estado >= 0)
                {
                    archivo.Read();
                    contador++;
                    if(c == '\n')
                    {
                        linea++;
                    }
                    if (estado >0)
                    {
                        buffer += c;
                    }
                    else
                    {
                        buffer = "";
                    }
                }
            }
            setContenido(buffer); 
            switch(buffer)
            {
            }
            if(estado == E)
            {
                //Requerimiento 9 agregar el numero de linea en el error
                if (getContenido() [0] == '"')
                {
                    throw new Error("Error lexico: No se cerro la cadena con \" en linea: "+linea, log);
                }
                else if (getContenido() [0] == '\'')
                {
                    throw new Error("Error lexico: No se cerro el caracter con ' en linea: "+linea, log);
                }
                //else if (getClasificacion() == Tipos.Numero)
                //{
                //    throw new Error("Error lexico: Se espera un digito en linea: "+linea, log);
                //}
                else
                {
                    throw new Error("Error lexico: No definido en linea: "+linea, log);
                }
            }
            else if (!FinArchivo())
            {
                log.WriteLine(getContenido() + " | " + getClasificacion());
            }
        }
        public long getContador(){
            return contador;
        }
        public void setContador(long cont){
            //archivo.DiscardBufferedData();
            contador = cont;
        }
        public bool FinArchivo()
        {
            return archivo.EndOfStream;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Console.WriteLine("Dispose");
            cerrar();
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}