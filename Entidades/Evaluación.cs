using System;

namespace CoreEscuela.Entidades
{
    public class Evaluación    {
        public Alumno Alumno { get; set; }
        public Asignatura Asignatura { get; set; }
        public String Nombre { get; set; }
        public float Nota { get; set; }
    }
}