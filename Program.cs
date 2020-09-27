using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {   //El evento se va a ejecutar siempre que termine la aplicación, no importa cómo termine
            AppDomain.CurrentDomain.ProcessExit += AccionDelEvento; //AccionDelEvento es un apuntador a una función
            AppDomain.CurrentDomain.ProcessExit += (o, s) => Printer.Beep(2000,1000,1);
            AppDomain.CurrentDomain.ProcessExit -= AccionDelEvento; 

            var engine = new EscuelaEngine();
            engine.Inicializar();

            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");

            var reporteador = new Reporteador(engine.GetDiccionarioObjetos());    
            var evalList = reporteador.GetListaEvaluaciones();
            var listaAsg = reporteador.GetListaAsignaturas();
            var listaEvalXAsig = reporteador.GetDicEvaluaXAsig();
        }

        private static void AccionDelEvento(object sender, EventArgs e)
        {
            Printer.WriteTitle("SALIENDO");
            Printer.Beep(3000, 1000, 3);
            Printer.WriteTitle("SALIÓ");
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
