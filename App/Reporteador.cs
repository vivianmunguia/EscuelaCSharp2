using System;
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
            _diccionario[LlaveDiccionario.Evaluación];
        }
    }
}