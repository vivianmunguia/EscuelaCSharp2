using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;

/**
@
*/

namespace CoreEscuela.App
{   //el modificador sealed indica que se pueden crear instancias de la clase pero no heredar de ella
    public sealed class EscuelaEngine
    {
        public Escuela Escuela { get; set; }

        public EscuelaEngine()
        {

        }

        public void Inicializar()
        {
            Escuela = new Escuela("Platzi Academy", 2012,
            TiposEscuela.Primaria,
            ciudad: "Bogotá", pais: "Colombia");
            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();

        }

        public void ImprimirDiccionario(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dic, bool imprEval = false) {
            foreach (var objdic in dic) {
                Printer.WriteTitle(objdic.Key.ToString());

                foreach (var val in objdic.Value) {
                    switch (objdic.Key){
                        case LlaveDiccionario.Evaluación:
                            if (imprEval)
                                Console.WriteLine(val);
                            break;
                        case LlaveDiccionario.Escuela:
                            Console.WriteLine("Escuela: " + val);
                            break;
                        case LlaveDiccionario.Alumno:
                            Console.WriteLine("Alumno: " + val.Nombre);
                            break;
                        case LlaveDiccionario.Curso:
                            var curtmp = val as Curso; //La variable es igualada al valor que se le pasó visto como un Curso
                            if(curtmp != null) //Si es diferente de null significa que es un curso
                            {
                                int count = curtmp.Alumnos.Count;
                                Console.WriteLine("Curso: " + val.Nombre + " Cantidad alumnos: " + count);
                            }
                            break;
                        default:
                            Console.WriteLine(val);
                            break;
                    }
                }
            }
        }

        public Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> GetDiccionarioObjetos()
        {
            var diccionario = new Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>>();

            //diccionario.Add(LlaveDiccionario.Escuela, new[] { Escuela }); //Para introducir la escuela al diccionario se creó una colección de un sólo elemento
            diccionario.Add(LlaveDiccionario.Curso, Escuela.Cursos.Cast<ObjetoEscuelaBase>()); //Se añade el curso al diccionario
            
            var listatmp = new List<Evaluación>();
            var listatmpas = new List<Asignatura>();
            var listatmpal = new List<Alumno>();

            foreach (var cur in Escuela.Cursos)
            {   
                listatmpas.AddRange(cur.Asignaturas);
                listatmpal.AddRange(cur.Alumnos);

                foreach (var alum in cur.Alumnos)
                {
                    listatmp.AddRange(alum.Evaluaciones);
                }
                
            }
                diccionario.Add(LlaveDiccionario.Evaluación, listatmp.Cast<ObjetoEscuelaBase>());
                diccionario.Add(LlaveDiccionario.Asignatura, listatmpas.Cast<ObjetoEscuelaBase>()); //Se añaden las asignaturas al diccionario
                diccionario.Add(LlaveDiccionario.Alumno, listatmpal.Cast<ObjetoEscuelaBase>()); //Se añaden los alumnos al diccionario
            return diccionario;
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            bool traeEvaluaciones = true, //parámetro opcional, debe ir al final
            bool traeAlumnos = true, //parámetro opcional, debe ir al final
            bool traeAsignaturas = true, //parámetro opcional, debe ir al final
            bool traeCursos = true //parámetro opcional, debe ir al final
            )
        {
            return GetObjetosEscuela(out int dummy, out dummy, out dummy, out dummy);
        }
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            bool traeEvaluaciones = true, //parámetro opcional, debe ir al final
            bool traeAlumnos = true, //parámetro opcional, debe ir al final
            bool traeAsignaturas = true, //parámetro opcional, debe ir al final
            bool traeCursos = true //parámetro opcional, debe ir al final
            )
        {
            return GetObjetosEscuela(out conteoEvaluaciones, out int dummy, out dummy, out dummy);
        }
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            out int conteoCursos,
            bool traeEvaluaciones = true, //parámetro opcional, debe ir al final
            bool traeAlumnos = true, //parámetro opcional, debe ir al final
            bool traeAsignaturas = true, //parámetro opcional, debe ir al final
            bool traeCursos = true //parámetro opcional, debe ir al final
            )
        {
            return GetObjetosEscuela(out conteoEvaluaciones, out conteoCursos, out int dummy, out dummy);
        }
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            out int conteoCursos,
            out int conteoAsignaturas,
            bool traeEvaluaciones = true, //parámetro opcional, debe ir al final
            bool traeAlumnos = true, //parámetro opcional, debe ir al final
            bool traeAsignaturas = true, //parámetro opcional, debe ir al final
            bool traeCursos = true //parámetro opcional, debe ir al final
            )
        {
            return GetObjetosEscuela(out conteoEvaluaciones, out conteoCursos, out conteoAsignaturas, out int dummy);
        }
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            out int conteoCursos,
            out int conteoAsignaturas,
            out int conteoAlumnos,
            bool traeEvaluaciones = true, //parámetro opcional, debe ir al final
            bool traeAlumnos = true, //parámetro opcional, debe ir al final
            bool traeAsignaturas = true, //parámetro opcional, debe ir al final
            bool traeCursos = true //parámetro opcional, debe ir al final
            )
        {
            conteoEvaluaciones = conteoAsignaturas = conteoAlumnos = 0; //asignaciones múltiples
            var listaObj = new List<ObjetoEscuelaBase>();
            listaObj.Add(Escuela);

            if (traeCursos)
                listaObj.AddRange(Escuela.Cursos);

            conteoCursos = Escuela.Cursos.Count;
            foreach (var curso in Escuela.Cursos)
            {
                conteoAsignaturas += curso.Asignaturas.Count;
                conteoAlumnos += curso.Alumnos.Count;
                if (traeAsignaturas)
                    listaObj.AddRange(curso.Asignaturas);

                if (traeAlumnos)
                    listaObj.AddRange(curso.Alumnos);

                if (traeEvaluaciones)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        listaObj.AddRange(alumno.Evaluaciones);
                        conteoEvaluaciones += alumno.Evaluaciones.Count;
                    }
                }
            }

            return listaObj.AsReadOnly();
        }

        #region Métodos de carga
        private void CargarEvaluaciones()
        {
            var rnd = new Random();
            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluación
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i + 1}",
                                Nota = MathF.Round (
                                        5 * (float) rnd.NextDouble()
                                        , 2), //Redondea el resultado a dos cifras
                                Alumno = alumno
                            };
                            alumno.Evaluaciones.Add(ev);
                        }
                    }
                }
            }
        }

        private void CargarAsignaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                List<Asignatura> listaAsignaturas = new List<Asignatura>(){
                    new Asignatura { Nombre = "Matemáticas" },
                    new Asignatura { Nombre = "Educación Física" },
                    new Asignatura { Nombre = "Castellano" },
                    new Asignatura { Nombre = "Ciencias Naturales" }
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }

        private List<Alumno> GenerarAlumnosAlAzar(int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Álvaro", "Nicolás" };
            string[] apellido1 = { "Ruíz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };
            //linQ
            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno { Nombre = $"{n1} {n2} {a1}" };
            return listaAlumnos.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();
        }

        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>(){
                new Curso(){ Nombre = "101", Jornada = TiposJornada.Mañana },
                new Curso(){ Nombre = "201", Jornada = TiposJornada.Mañana },
                new Curso(){ Nombre = "301", Jornada = TiposJornada.Mañana },
                new Curso(){ Nombre = "401", Jornada = TiposJornada.Tarde},
                new Curso(){ Nombre = "501", Jornada = TiposJornada.Tarde}
            };

            Random rnd = new Random();
            foreach (var c in Escuela.Cursos)
            {
                int cantRandom = rnd.Next(5, 20); //Genera un número random entre 5 y 20
                c.Alumnos = GenerarAlumnosAlAzar(cantRandom);
            }
        }
    }
    #endregion
}