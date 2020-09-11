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
            var listaEvaluaciones = GetListaEvaluaciones();
            //Devuelve las asignaturas de las evaluaciones, si hay asignaturas iguales sólo devuelve una
            return (from Evaluación ev in listaEvaluaciones
                   select ev.Asignatura.Nombre).Distinct();; //se encarga de devolver sólo un dato cuando se repite muchas veces
        }

        public Dictionary<string, IEnumerable<Evaluación>> GetDicEvaluaXAsig()
        {
            var dictaRta = new Dictionary<string, IEnumerable<Evaluación>>();
            return dictaRta;
        }

    }
}