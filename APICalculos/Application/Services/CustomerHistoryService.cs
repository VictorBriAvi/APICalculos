using APICalculos.Application.DTOs;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;

namespace APICalculos.Application.Services
{
    public class CustomerHistoryService : ICustomerHistoryService
    {
        private readonly ICustomerHistoryRepository _customerHistoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerHistoryService(ICustomerHistoryRepository customerHistoryRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _customerHistoryRepository = customerHistoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<List<CustomerHistoryDTO>> GetAllCustomerHistoriesAsync()
        {
            var customerHistory = await _customerHistoryRepository.GetAllAsync();
            return _mapper.Map<List<CustomerHistoryDTO>>(customerHistory);
        }
        
        public async Task<CustomerHistoryDTO> GetCustomerHistoryForId(int id)
        {
            var customerHistory = await _customerHistoryRepository.GetByIdAsync(id);
            if (customerHistory == null)
            {
                return null;
            }

            return _mapper.Map<CustomerHistoryDTO>(customerHistory);
        }

        public async Task<CustomerHistoryDTO> AddCustomerHistoryAsync(CustomerHistoryCreationDTO customerHistoryCreationDTO)
        {
            if (string.IsNullOrWhiteSpace(customerHistoryCreationDTO.Title))
            {
                throw new ArgumentException("El titulo del registro historial ya existe ");
            }
            
            var existsTitle = await _customerHistoryRepository.ExistsByTitleAsync(customerHistoryCreationDTO.Title);
            if (existsTitle)
            {
                throw new InvalidOperationException("El titulo de la historia ya existe");
            }

            var customerHistory = _mapper.Map<CustomerHistory>(customerHistoryCreationDTO);
            customerHistory.DateHistory = DateTime.UtcNow;

            await _unitOfWork.CustomerHistory.AddAsync(customerHistory);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CustomerHistoryDTO>(customerHistory);
        }

        public async Task UpdateCustomerHistoryAsync(int id, CustomerHistoryUpdateDTO dto)
        {
            var customerHistoryDB = await _customerHistoryRepository.GetByIdAsync(id);

            if (customerHistoryDB == null)
            {
                throw new KeyNotFoundException("Historial Cliente no encontrado");
            }

            // Solo actualizo lo que sí se puede modificar
            if (!string.IsNullOrWhiteSpace(dto.Title))
            {
                customerHistoryDB.Title = dto.Title;
            }

            if (!string.IsNullOrWhiteSpace(dto.Description))
            {
                customerHistoryDB.Description = dto.Description;
            }

            if (dto.ClientId != 0)
            {
                // Desasociamos la navegación para que EF tome solo la FK
                customerHistoryDB.Client = null;
                customerHistoryDB.ClientId= dto.ClientId;
            }

            // ⚠️ Importante: NO tocamos customerHistoryDB.DateHistory
            // Ese campo representa la fecha de creación y nunca debe cambiar.

            _customerHistoryRepository.Update(customerHistoryDB);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task DeleteCustomerHistoriesAsync(int id)
        {
            var customerHistoriesDB = await _customerHistoryRepository.GetByIdAsync(id);

            if (customerHistoriesDB == null)
            {
                throw new KeyNotFoundException("Historial cliente no encontrado");
            }

            _customerHistoryRepository.Remove(customerHistoriesDB);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
