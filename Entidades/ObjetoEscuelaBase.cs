using System;

namespace CoreEscuela.Entidades
{   //el modificador abstract indica que no se pueden crear objetos de la clase, pero sí heredar de ella
    public abstract class ObjetoEscuelaBase
    {
        public string UniqueId { get; private set; }
        public string Nombre { get; set; }

        public ObjetoEscuelaBase()
        {
            UniqueId = Guid.NewGuid().ToString();
        }

        public override string ToString() 
        {
            return $"{Nombre}, {UniqueId}";
        }
    }
}