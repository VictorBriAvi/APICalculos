using APICalculos.Application.DTOs;
using APICalculos.Application.DTOs.Client;
using APICalculos.Application.DTOs.Employee;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Repositories;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace APICalculos.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<EmployeeDTO>> GetAllEmployeesAsync(int storeId, string? search)
        {
            var employees =
                await _employeeRepository.GetAllAsync(storeId, search);

            return _mapper.Map<List<EmployeeDTO>>(employees);
        }

        public async Task<EmployeeDTO?> GetEmployeeForIdAsync(int id, int storeId)
        {
            var employee =
                await _employeeRepository.GetByIdAsync(id, storeId);

            if (employee == null)
                return null;

            return _mapper.Map<EmployeeDTO>(employee);
        }

        public async Task<EmployeeDTO> AddEmployeeAsync(int storeId, EmployeeCreationDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("El nombre no puede estar vacío");

            var existsName =
                await _employeeRepository.ExistsByNameAsync(dto.Name, storeId);

            if (existsName)
                throw new InvalidOperationException("El colaborador ya existe en esta tienda");

            var employee = _mapper.Map<Employee>(dto);

            employee.StoreId = storeId;

            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<EmployeeDTO>(employee);
        }

        public async Task UpdateEmployeeAsync(int id, int storeId, EmployeeCreationDTO dto)
        {
            var employeeDB =
                await _employeeRepository.GetByIdAsync(id, storeId);

            if (employeeDB == null)
                throw new KeyNotFoundException("Colaborador no encontrado");

            if (!string.IsNullOrWhiteSpace(dto.Name))
                employeeDB.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.IdentityDocument))
                employeeDB.IdentityDocument = dto.IdentityDocument;

            if (dto.PaymentPercentage > 0)
                employeeDB.PaymentPercentage = dto.PaymentPercentage;

            if (dto.DateBirth != default)
                employeeDB.DateBirth = dto.DateBirth;

            _employeeRepository.Update(employeeDB);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id, int storeId)
        {
            var employeeDB =
                await _employeeRepository.GetByIdAsync(id, storeId);

            if (employeeDB == null)
                throw new KeyNotFoundException("Colaborador no encontrado");

            try
            {
                _employeeRepository.Remove(employeeDB);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                throw new InvalidOperationException(
                    "No se puede eliminar porque está asociado a una venta.");
            }
        }

        public async Task<List<EmployeeSearchDTO>> SearchEmployeeAsync(int storeId, string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<EmployeeSearchDTO>();

            var employees =
                await _employeeRepository.SearchAsync(storeId, query.Trim(), 15);

            return _mapper.Map<List<EmployeeSearchDTO>>(employees);
        }
    }

}
