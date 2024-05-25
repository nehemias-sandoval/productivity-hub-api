namespace productivity_hub_api.Service.CatalogoService
{
    public interface ICatalogoService<EtiquetaDto, FrecuenciaDto, PrioridadDto, TipoEventoDto, TipoNotificacionDto>
    {
        public Task<IEnumerable<EtiquetaDto>> GetAllEtiquetasAsync();

        public Task<IEnumerable<FrecuenciaDto>> GetAllFrecuenciasAsync();

        public Task<IEnumerable<PrioridadDto>> GetAllPrioridadesAsync();

        public Task<IEnumerable<TipoEventoDto>> GetAllTipoEventosAsync();

        public Task<IEnumerable<TipoNotificacionDto>> GetAllTipoNotificacionAsync();
    }
}
