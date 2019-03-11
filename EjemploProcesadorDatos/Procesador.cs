using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace EjemploProcesadorDatos
{
    public class Procesador : IDisposable
    {
        public List<int> ListaDatos { get; set; }
        public IConfiguration Configuration { get; set; }

        public Procesador(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
            ListaDatos = new List<int>();
        }

        public void AddDatos(int datos)
        {
            ListaDatos.Add(datos);
        }

        public void Dispose()
        {
            ListaDatos.Clear();
            ListaDatos = null;
        }
    }
}
