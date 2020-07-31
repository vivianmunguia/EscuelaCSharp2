using System;
using System.Collections.Generic;
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

            //var obj = new ObjetoEscuelaBase();
            Printer.DrawLine(20);
            Printer.DrawLine(20);
            Printer.DrawLine(20);
            Printer.WriteTitle("Pruebas de Polimorfismo");
            var alumnoTest = new Alumno{Nombre = "Claire Underwood"};

            Printer.WriteTitle("Alumno");
            WriteLine($"Alumno: {alumnoTest.Nombre}");
            WriteLine($"Alumno: {alumnoTest.UniqueId}");
            WriteLine($"Alumno: {alumnoTest.GetType()}");

            ObjetoEscuelaBase ob = alumnoTest;
            Printer.WriteTitle("ObjetoEscuela");
            WriteLine($"Alumno: {ob.Nombre}");
            WriteLine($"Alumno: {ob.UniqueId}");
            WriteLine($"Alumno: {ob.GetType()}");

            var objDummy = new ObjetoEscuelaBase{Nombre = "Frank Underwood"};
            Printer.WriteTitle("ObjetoEscuelaBase");
            WriteLine($"Alumno: {objDummy.Nombre}");
            WriteLine($"Alumno: {objDummy.UniqueId}");
            WriteLine($"Alumno: {objDummy.GetType()}");

            var evaluación = new Evaluación(){Nombre="Evaluación de math", Nota=4.5f};
            Printer.WriteTitle("Evaluación");
            WriteLine($"Evaluación: {evaluación.Nombre}");
            WriteLine($"Evaluación: {evaluación.UniqueId}");
            WriteLine($"Evaluación: {evaluación.Nota}");
            WriteLine($"Evaluación: {evaluación.GetType()}");
        
            ob = evaluación;
            Printer.WriteTitle("ObjetoEscuela");
            WriteLine($"Alumno: {ob.Nombre}");
            WriteLine($"Alumno: {ob.UniqueId}");
            WriteLine($"Alumno: {ob.GetType()}");

            alumnoTest = (Alumno) (ObjetoEscuelaBase) evaluación;
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
