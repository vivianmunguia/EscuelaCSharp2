using System;
using System.Linq; //Es muy útil al trabajar con colecciones ya que ofrece muchas opciones
using System.Collections.Generic;
using CoreEscuela.Entidades;

namespace CoreEscuela.App
{
    public class Reporteador
    {
        Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;
        public Reporteador(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dicObsEsc)
        {
            if (dicObsEsc == null)
                throw new ArgumentNullException(nameof(dicObsEsc));
            _diccionario = dicObsEsc;
        }
        public IEnumerable<Evaluación> GetListaEvaluaciones()
        {   
            if (_diccionario.TryGetValue(LlaveDiccionario.Evaluación,
                                        out IEnumerable<ObjetoEscuelaBase> lista))
            {
                return lista.Cast<Evaluación>();
            }
            {
                return new List<Evaluación>();
                //Escribir en el log de auditoría
            }
        }

        public IEnumerable<string> GetListaAsignaturas()
        {
            return GetListaAsignaturas(out var dummy);
        }

        public IEnumerable<string> GetListaAsignaturas(out IEnumerable<Evaluación> listaEvaluaciones)
        {
            listaEvaluaciones = GetListaEvaluaciones();
            //Devuelve las asignaturas de las evaluaciones, si hay asignaturas iguales sólo devuelve una
            return (from Evaluación ev in listaEvaluaciones
                   select ev.Asignatura.Nombre).Distinct();; //Se encarga de devolver sólo un dato cuando se repite muchas veces
        }

        public Dictionary<string, IEnumerable<Evaluación>> GetDicEvaluaXAsig()
        {
            var dictaRta = new Dictionary<string, IEnumerable<Evaluación>>();
            
            var listaAsig = GetListaAsignaturas(out var listaEval);

            foreach (var asig in listaAsig) //Se recorren las asignaturas obtenidas
            {   //Se seleccionan las evaluaciones que coincidan con la asignatura actual
                var evalAsig = from eval in listaEval
                               where eval.Asignatura.Nombre == asig
                               select eval; 
                dictaRta.Add(asig, evalAsig);
            }
            return dictaRta;
        }

        public  Dictionary<string, IEnumerable<object>> GetPromeAlumnPorAsignatura()
        {
            var rta = new Dictionary<string, IEnumerable<object>>();
            var dicEvalXAsig = GetDicEvaluaXAsig();

            foreach (var asigConEval in dicEvalXAsig)
            {
                var dumy = from eval in asigConEval.Value
                            group eval by eval.Alumno.UniqueId
                            into grupoEvalsAlumno
                            select new
                            { 
                                AlumnoId = grupoEvalsAlumno.Key,
                                Promedio = grupoEvalsAlumno.Average(evaluacion => evaluacion.Nota)
                            };
            }

            return rta;
        }

    }
}