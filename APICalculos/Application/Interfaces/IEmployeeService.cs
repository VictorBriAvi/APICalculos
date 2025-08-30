﻿using APICalculos.Application.DTOs;

namespace APICalculos.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetAllEmployeesAsync();
        Task<EmployeeDTO> GetEmployeeForIdAsync(int id);
        Task<EmployeeDTO> AddEmployeeAsync(EmployeeCreationDTO clienteCreacionDTO);
        Task UpdateEmployeeAsync(int id, EmployeeCreationDTO clienteCreacionDTO);
        Task DeleteEmployeeAsync(int id);
    }
}
