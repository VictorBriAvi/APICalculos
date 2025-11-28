using APICalculos.Application.DTOs;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Domain.Entities;
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

        public async Task<List<SaleDTO>> GetSalesByTodayAsync()
        {
            var sales = await _saleRepository.GetByTodayAsync();
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
 
            var client = await _unitOfWork.Clients.GetByIdAsync(saleCreationDTO.ClientId);
            if (client == null)
                throw new Exception($"El cliente con Id {saleCreationDTO.ClientId} no existe.");

            //var paymentType = await _unitOfWork.PaymentType.GetByIdAsync(saleCreationDTO.PaymentTypeId);
            //if (paymentType == null)
            //    throw new Exception($"El tipo de pago con Id {saleCreationDTO.PaymentTypeId} no existe.");

            var sale = _mapper.Map<Sale>(saleCreationDTO);
            sale.DateSale = DateTime.Now;

            await _unitOfWork.Sale.AddAsync(sale);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SaleDTO>(sale);
        }


        public async Task<SaleDTO> AddSaleWithDetailsAsync(SaleCreationDTO dto)
        {
            var sale = new Sale
            {
                ClientId = dto.ClientId,
                DateSale = DateTime.Now,
                SaleDetail = new List<SaleDetail>(),
                Payments = new List<SalePayment>()
            };

            foreach (var d in dto.SaleDetails)
            {
                sale.SaleDetail.Add(new SaleDetail
                {
                    ServiceTypeId = d.ServiceTypeId,
                    EmployeeId = d.EmployeeId,
                    UnitPrice = d.UnitPrice,
                    DiscountPercent = d.DiscountPercent,
                    AdditionalCharge = d.AdditionalCharge
                });
            }

            foreach (var p in dto.Payments)
            {
                sale.Payments.Add(new SalePayment
                {
                    PaymentTypeId = p.PaymentTypeId,
                    AmountPaid = p.AmountPaid,
                    PaymentDate = DateTime.Now
                });
            }

            sale.CalculateTotal();

            await _unitOfWork.Sale.AddAsync(sale);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SaleDTO>(sale);
        }

        public async Task UpdateSaleAsync(int id, SaleCreationDTO saleCreationDTO)
        {
            var saleDB = await _saleRepository.GetByIdAsync(id);
            if (saleDB == null)
                throw new KeyNotFoundException("Venta no encontrada");

            // 🔹 Actualizar cliente
            if (saleCreationDTO.ClientId > 0)
                saleDB.ClientId = saleCreationDTO.ClientId;

            // 🔹 Actualizar pagos
            if (saleCreationDTO.Payments != null && saleCreationDTO.Payments.Any())
            {
                // Limpia los pagos previos (para simplificar)
                saleDB.Payments.Clear();
                foreach (var paymentDTO in saleCreationDTO.Payments)
                {
                    saleDB.Payments.Add(new SalePayment
                    {
                        PaymentTypeId = paymentDTO.PaymentTypeId,
                        AmountPaid = paymentDTO.AmountPaid,
                        PaymentDate = DateTime.Now
                    });
                }
            }
            //else if (saleCreationDTO.PaymentTypeId > 0)
            //{
            //    // Compatibilidad con la versión anterior
            //    saleDB.Payments.Clear();
            //    saleDB.Payments.Add(new SalePayment
            //    {
            //        PaymentTypeId = saleCreationDTO.PaymentTypeId,
            //        AmountPaid = saleCreationDTO.TotalAmount,
            //        PaymentDate = DateTime.Now
            //    });
            //}

            // 🔹 Total
            if (saleCreationDTO.TotalAmount > 0)
                saleDB.TotalAmount = saleCreationDTO.TotalAmount;
            else
                saleDB.CalculateTotal();

            _saleRepository.Update(saleDB);
            await _unitOfWork.SaveChangesAsync();
        }



        public async Task DeleteSaleAsync(int id)
        {
            var saleDB = await _saleRepository.GetByIdAsync(id);
            if (saleDB == null)
                throw new KeyNotFoundException("Venta no encontrada");

            _saleRepository.Remove(saleDB);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
