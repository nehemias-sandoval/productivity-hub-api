using productivity_hub_api.DTOs.Mail;
using productivity_hub_api.Models;
using productivity_hub_api.Repository;
using productivity_hub_api.Repository.NotificacionRepository;
using productivity_hub_api.Service.MailService;

namespace productivity_hub_api.Service.ReminderService
{
    public class ReminderService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer? _timer;

        public ReminderService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            ScheduleNextRun();
            return Task.CompletedTask;
        }

        private void ScheduleNextRun()
        {
            var now = DateTime.Now;
            var nextExecutionTime = new DateTime(now.Year, now.Month, now.Day, 14, 50, 0);
            if (now > nextExecutionTime)
            {
                nextExecutionTime = nextExecutionTime.AddDays(1);
            }

            var delay = nextExecutionTime - now;
            _timer = new Timer(DoWork, null, delay, Timeout.InfiniteTimeSpan);
        }

        private async void DoWork(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var notificacionRepository = scope.ServiceProvider.GetRequiredService<NotificacionRepository>();
                var tareaRepository = scope.ServiceProvider.GetRequiredKeyedService<IRepository<Tarea>>("tareaRepository");
                var mailService = scope.ServiceProvider.GetRequiredService<IMailReminderService>();

                var tareas = await tareaRepository.GetAllAsync();
                var personas = tareas.Select(t => t.Persona).Distinct();
                var notificaciones = new List<Notificacion>();

                if (personas.Any())
                {
                    personas.ToList().ForEach(persona =>
                    {
                        var tareasPorVencer = tareas
                            .Where(t => t.IdPersona == persona.Id && t.IdEtiqueta != 3 && t.FechaLimite.Date == DateTime.Today);

                        if (tareasPorVencer.Count() > 0)
                        {
                            var mailData = new HTMLReminderMailDataDto()
                            {
                                EmailDestinatario = persona.Usuario.Email,
                                NombreDestinatario = persona.Nombre + " " + persona.Apellido,
                                TareasPendientes = tareasPorVencer.Count()
                            };

                            string strTareaPendiente = mailData.TareasPendientes > 1 ? "tareas pendientes" : "tarea pendiente";
                            string notificacion = $"Tiene {mailData.TareasPendientes} {strTareaPendiente} para ahora";
                            notificaciones.Add(new Notificacion
                            {
                                Mensaje = notificacion,
                                Fecha = DateTime.Today,
                                Leida = false,
                                IdTipoNotificacion = 1, // Recordatorio
                                IdPersona = persona.Id,
                            });

                            mailService.SendHTMLMail(mailData);
                        }
                    });
                }

                if (notificaciones.Count() > 0)
                {
                    foreach (var notificacion in notificaciones)
                    {
                        await notificacionRepository.AddAsync(notificacion);
                    }

                    await notificacionRepository.SaveAsync();
                }
                

                // Reprogramar el temporizador para el próximo día a las 6 AM
                ScheduleNextRun();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
