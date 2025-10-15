using APICalculos.Application.DTOs;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Repositories;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;
using System.Globalization;

namespace APICalculos.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SaleService(ISaleRepository saleRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SaleDTO>> GetAllSaleAsync()
        {
            var sales = await _saleRepository.GetAllAsync();
            return _mapper.Map<List<SaleDTO>>(sales);
        }

        public async Task<SaleDTO> GetSaleForId(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
                return null;
            return _mapper.Map<SaleDTO>(sale);
        }

        public async Task<SaleDTO> AddSaleAsync(SaleCreationDTO saleCreationDTO)
        {
            var sale = _mapper.Map<Sale>(saleCreationDTO);
            sale.DateSale = DateTime.Now;
            await _unitOfWork.Sale.AddAsync(sale);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SaleDTO>(sale);
        }

        public async Task<SaleDTO> AddSaleWithDetailsAsync(SaleCreationDTO saleCreationDTO)
        {
            // Mapear la venta
            var sale = _mapper.Map<Sale>(saleCreationDTO);
            sale.DateSale = DateTime.Now;
            sale.CalculateTotal();

            // Los detalles se agregan automáticamente por la relación
            await _unitOfWork.Sale.AddAsync(sale);

            // Guardar todo junto
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SaleDTO>(sale);
        }


        public async Task UpdateSaleAsync(int id, SaleCreationDTO saleCreationDTO)
        {
            var saleDB = await _saleRepository.GetByIdAsync(id);
            if(saleDB == null)
                throw new KeyNotFoundException("Venta no encontrada");

            if (saleCreationDTO.ClientId > 0)
                saleDB.ClientId = saleCreationDTO.ClientId;

            if (saleCreationDTO.PaymentTypeId > 0)
                saleDB.PaymentTypeId = saleCreationDTO.PaymentTypeId;

            if (saleCreationDTO.TotalAmount != 0)
            {
                saleDB.TotalAmount = saleCreationDTO.TotalAmount;
            }

            _saleRepository.Update(saleDB);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteSaleAsync(int Id)
        {
            var saleDB = await _saleRepository.GetByIdAsync(Id);

            if (saleDB == null)
            {
                throw new KeyNotFoundException("Venta no encontrada");
            }

            _saleRepository.Remove(saleDB);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
