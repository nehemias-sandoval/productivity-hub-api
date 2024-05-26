using AutoMapper;
using productivity_hub_api.DTOs.Catalogo;
using productivity_hub_api.Repository.CatalogoRepository;

namespace productivity_hub_api.Service.CatalogoService
{
    public class CatalogoService : ICatalogoService<EtiquetaDto, FrecuenciaDto, PrioridadDto, TipoEventoDto, TipoNotificacionDto>
    {
        private CatalogoRepository _catalogoRepository;
        IMapper _mapper;

        public CatalogoService(CatalogoRepository catalogoRepository, IMapper mapper)
        {
            _catalogoRepository = catalogoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EtiquetaDto>> GetAllEtiquetasAsync()
        {
            var etiqueas = await _catalogoRepository.GetAllTipoEtiquetasAsync();
            var etiquetasDto = _mapper.Map<IEnumerable<EtiquetaDto>>(etiqueas);

            return etiquetasDto;
        }

        public async Task<IEnumerable<FrecuenciaDto>> GetAllFrecuenciasAsync()
        {
           var frecuencias = await _catalogoRepository.GetAllFrecuenciasAsync();
           var frecuenciasDto = _mapper.Map<IEnumerable<FrecuenciaDto>>(frecuencias);
           
           return frecuenciasDto;
        }

        public async Task<IEnumerable<PrioridadDto>> GetAllPrioridadesAsync()
        {
            var prioridades = await _catalogoRepository.GetAllPrioridadesAsync();
            var prioridadesDto = _mapper.Map<IEnumerable<PrioridadDto>>(prioridades);

            return prioridadesDto;
        }

        public async Task<IEnumerable<TipoEventoDto>> GetAllTipoEventosAsync()
        {
            var tpEventos = await _catalogoRepository.GetAllTipoEventosAsync();
            var tpEventosDto = _mapper.Map<IEnumerable<TipoEventoDto>>(tpEventos);

            return tpEventosDto;
        }

        public async Task<IEnumerable<TipoNotificacionDto>> GetAllTipoNotificacionAsync()
        {
            var tpNotificaciones = await _catalogoRepository.GetAllTipoNotificacionesAsync();
            var tpNotificacionesDto = _mapper.Map<IEnumerable<TipoNotificacionDto>>(tpNotificaciones);

            return tpNotificacionesDto;
        }
    }
}
