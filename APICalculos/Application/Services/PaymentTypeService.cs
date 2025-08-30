﻿using APICalculos.Application.DTOs;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;

namespace APICalculos.Application.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public PaymentTypeService(IPaymentTypeRepository paymentTypeRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _paymentTypeRepository = paymentTypeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<PaymentTypeDTO>> GetAllPaymenteTypesAsync()
        {
            var paymenteType = await _paymentTypeRepository.GetAllAsync();
            return _mapper.Map<List<PaymentTypeDTO>>(paymenteType);
        }

        public async Task<PaymentTypeDTO> GetPaymentTypeForId(int id)
        {
            var paymentType = await _paymentTypeRepository.GetByIdAsync(id);
            if (paymentType == null)
            {
                return null;
            }
            return _mapper.Map<PaymentTypeDTO>(paymentType);
        }

        public async Task<PaymentTypeDTO> AddPaymenteTypeAsync(PaymentTypeCreationDTO paymentTypeCreationDTO)
        {
            if (string.IsNullOrWhiteSpace(paymentTypeCreationDTO.Name))
                throw new ArgumentException("El nombre no puede estar vacio");
            
            var existsName = await _paymentTypeRepository.ExistsByNameAsync(paymentTypeCreationDTO.Name);
            if (existsName)
            {
                throw new InvalidOperationException("El nombre del cliente ya existe");
            }

            var paymentType = _mapper.Map<PaymentType>(paymentTypeCreationDTO);

            await _unitOfWork.PaymentType.AddAsync(paymentType);

            await _unitOfWork.SaveChangesAsync();   
            
            return _mapper.Map<PaymentTypeDTO>(paymentType);
        }

        public async Task UpdatePaymentTypeAsync(int id, PaymentTypeCreationDTO paymentTypeCreationDTO)
        {
            var paymenteTypeDB = await _paymentTypeRepository.GetByIdAsync(id);

            if (paymenteTypeDB == null)
                throw new KeyNotFoundException("Tipo de pago no encontrado");

            if (!string.IsNullOrWhiteSpace(paymentTypeCreationDTO.Name))
                paymenteTypeDB.Name = paymentTypeCreationDTO.Name;

            _paymentTypeRepository.Update(paymenteTypeDB);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeletePaymentTypeAsync(int id)
        {
            var paymentTypeDB = await _paymentTypeRepository.GetByIdAsync(id);
            if (paymentTypeDB == null)
                throw new KeyNotFoundException("Tipo de pago no encontrado");

            _paymentTypeRepository.Remove(paymentTypeDB);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
