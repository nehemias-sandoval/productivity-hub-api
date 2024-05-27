namespace productivity_hub_api.Service.CatalogoService
{
    public interface ICatalogoService<EtiquetaDto, PrioridadDto, TipoEventoDto, TipoNotificacionDto>
    {
        public Task<IEnumerable<EtiquetaDto>> GetAllEtiquetasAsync();

        public Task<IEnumerable<PrioridadDto>> GetAllPrioridadesAsync();

        public Task<IEnumerable<TipoEventoDto>> GetAllTipoEventosAsync();

        public Task<IEnumerable<TipoNotificacionDto>> GetAllTipoNotificacionAsync();
    }
}
