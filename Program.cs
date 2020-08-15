using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new EscuelaEngine();
            engine.Inicializar();

            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            //Printer.Beep(10000, cantidad:10);
            
            ImprimirCursosEscuela(engine.Escuela);
            Dictionary<int, string> diccionario = new Dictionary<int, string>();

            diccionario.Add(10, "JuanK");
            diccionario.Add(23, "Lorem Ipsum");

            foreach (var keyValPair in diccionario)
            {
                WriteLine($"Key: {keyValPair.Key} Valor: {keyValPair.Value}");
            }

            Printer.WriteTitle("Acceso a Diccionario");
            diccionario[0] = "Pakerman";
            WriteLine(diccionario[0]);
            Printer.WriteTitle("Otro diccionario");
            var dic = new Dictionary<string, string>();
            dic["Luna"] = "Cuerpo celeste que gira alrededor de la tierra";
            WriteLine(dic["Luna"]);
            dic.Add("Luna", "Protagonista de Soy Luna");
            WriteLine(dic["Luna"]);

        }

        private static void ImprimirCursosEscuela(Escuela escuela)
        {
            Printer.WriteTitle("Cursos de la escuela");

            //if (escuela != null && escuela.Cursos != null) 
            //Primero se verifica la primera expresión, si es falsa ya no se evalúa la segunda
            //El operador ? significa que no se van a verificar los cursos si la escuela no es diferente de null
            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre }, Id {curso.UniqueId}");
                }
            }

        }

    }
}
