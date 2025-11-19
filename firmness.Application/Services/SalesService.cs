using firmness.Domain.Entities;
using firmness.Application.Interfaces;
using firmness.Application.DTOs;
using firmness.Application.Interfaces.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace firmness.Application.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepo;
        private readonly IMapper _mapper;

        public SalesService(ISalesRepository salesRepo, IMapper mapper)
        {
            _salesRepo = salesRepo;
            _mapper = mapper;
        }

        public async Task<List<SaleDto>> GetAllSalesAsync()
        {
            var sales = await _salesRepo.GetAllAsync();
            return _mapper.Map<List<SaleDto>>(sales);
        }

        public async Task<SaleDto?> GetSaleByIdAsync(int id)
        {
            var sale = await _salesRepo.GetByIdAsync(id);
            return _mapper.Map<SaleDto?>(sale);
        }

        public async Task<bool> CreateSaleAsync(CreateSaleDto saleDto)
        {
            try
            {
                if (saleDto.ClientId <= 0 || saleDto.EmployeeId <= 0 || saleDto.Details.Count == 0)
                    return false;

                var sale = new Sale
                {
                    ClientId = saleDto.ClientId,
                    EmployeeId = saleDto.EmployeeId,
                    Date = saleDto.SaleDate,
                    SaleDetails = _mapper.Map<List<SaleDetail>>(saleDto.Details)
                };

                await _salesRepo.AddAsync(sale);
                await _salesRepo.SaveAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateSaleAsync(UpdateSaleDto saleDto)
        {
            try
            {
                var existing = await _salesRepo.GetByIdAsync(saleDto.Id);
                if (existing == null) return false;

                existing.ClientId = saleDto.ClientId;
                existing.EmployeeId = saleDto.EmployeeId;
                existing.Date = saleDto.SaleDate;

                await _salesRepo.UpdateAsync(existing);
                await _salesRepo.SaveAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            try
            {
                var existing = await _salesRepo.GetByIdAsync(id);
                if (existing == null) return false;

                await _salesRepo.DeleteAsync(id);
                await _salesRepo.SaveAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ClientDto>> GetClientsAsync()
        {
            var clients = await _salesRepo.GetClientsAsync();
            return _mapper.Map<List<ClientDto>>(clients);
        }

        public async Task<List<EmployeeDto>> GetEmployeesAsync()
        {
            var employees = await _salesRepo.GetEmployeesAsync();
            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var products = await _salesRepo.GetProductsAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}