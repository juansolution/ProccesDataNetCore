using EjemploProcesadorDatos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EjemploNetCore
{
    class HostedService : IHostedService, IDisposable
    {
        private Timer Timer { get; set; }
        private static IConfiguration Configuration { get; set; }

        private static int ContTarea { get; set; }
        //private static Behavior BehaviorInstance = new Behavior();

        public HostedService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //System.Console.WriteLine(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

            Timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            System.Console.WriteLine("Servicio Detenido.");

            Timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            ContTarea = ContTarea + 1;
            Console.WriteLine("Tarea Iniciada: " + ContTarea.ToString());
            TareaEjecutandose(ContTarea);
        }

        private static void TareaEjecutandose(int tarea)
        {
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine("Procesando paso: " + i.ToString() + " de la tarea - " + tarea);

                using (var procesador = new Procesador(Configuration))
                {
                    procesador.AddDatos(i);
                }

                Thread.Sleep(2000);
            }

            Console.WriteLine("Tarea Finalizada: " + tarea.ToString());
        }
    }
}
