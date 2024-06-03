using AutoMapper;
using productivity_hub_api.DTOs.Notificacion;
using productivity_hub_api.Repository.NotificacionRepository;

namespace productivity_hub_api.Service.NotificacionService
{
    public class NotificacionService : INotificacionService<NotificacionDto>
    {
        NotificacionRepository _notificacionRepository;
        IMapper _mapper;

        public NotificacionService(NotificacionRepository notificacionRepository, IMapper mapper)
        {
            _notificacionRepository = notificacionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotificacionDto>> GetAllNotificacionesAsync()
        {
            var notificaciones = await _notificacionRepository.GetAllNotificacionesAsync();
            var notificacionesDto = _mapper.Map<IEnumerable<NotificacionDto>>(notificaciones);

            return notificacionesDto;
        }
    }
}
