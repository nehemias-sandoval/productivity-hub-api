namespace productivity_hub_api.Service.NotificacionService
{
    public interface INotificacionService<NotificacionDto>
    {
        Task<IEnumerable<NotificacionDto>> GetAllNotificacionesAsync();
    }
}
