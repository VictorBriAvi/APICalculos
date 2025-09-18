using APICalculos.Application.DTOs;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Repositories;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;
using System.Globalization;

namespace APICalculos.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;   
            _unitOfWork = unitOfWork;
        }

        public async Task<List<EmployeeDTO>> GetAllEmployeesAsync()
        {
            var employes = await _employeeRepository.GetAllAsync();
            return _mapper.Map<List<EmployeeDTO>>(employes);
        }

        public async Task<EmployeeDTO> GetEmployeeForIdAsync(int id)
        {
            var employe = await _employeeRepository.GetByIdAsync(id);
            if (employe == null)
            {
                return null;
            }
            return _mapper.Map<EmployeeDTO>(employe);
        }

        public async Task<EmployeeDTO> AddEmployeeAsync(EmployeeCreationDTO employeeCreationDTO)
        {
            if (string.IsNullOrWhiteSpace(employeeCreationDTO.Name))
            {
                throw new ArgumentException("El nombre del colaborador no puede estar vacio");
            }

            var existsName = await _employeeRepository.ExistsByNameAsync(employeeCreationDTO.Name);
            if (existsName)
            {
                throw new InvalidOperationException("El nombre del cliente ya existe");
            }

            var employee = _mapper.Map<Employee>(employeeCreationDTO);
            
            employee.DateBirth = DateTime.ParseExact(employeeCreationDTO.ParseDateBirth,
    "dd/MM/yyyy HH:mm:ss",
    CultureInfo.InvariantCulture,
    DateTimeStyles.AssumeLocal | DateTimeStyles.AdjustToUniversal
);

            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<EmployeeDTO>(employee);
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeCreationDTO employeeCreationDTO)
        {
            var employeeDB = await _employeeRepository.GetByIdAsync(id);

            // Actualiza solo si vienen datos válidos
            if (!string.IsNullOrWhiteSpace(employeeCreationDTO.Name))
                employeeDB.Name = employeeCreationDTO.Name;

            if (!string.IsNullOrWhiteSpace(employeeCreationDTO.IdentityDocument))
                employeeDB.IdentityDocument = employeeCreationDTO.IdentityDocument;

            if (!string.IsNullOrWhiteSpace(employeeCreationDTO.ParseDateBirth))
                employeeDB.DateBirth = DateTime.Parse(employeeCreationDTO.ParseDateBirth);

            _employeeRepository.Update(employeeDB);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var clienteDB = await _employeeRepository.GetByIdAsync(id);
            if (clienteDB == null)
                throw new KeyNotFoundException("Cliente no encontrado");

            _employeeRepository.Remove(clienteDB);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
