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
        private readonly IEmailService _emailService;

        public SalesService(ISalesRepository salesRepo, IMapper mapper, IEmailService emailService)
        {
            _salesRepo = salesRepo;
            _mapper = mapper;
            _emailService = _emailService;
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

        public async Task<bool> SendReceiptAsync(SendReceiptDto dto)
        {
            var sale = await _salesRepo.GetByIdAsync(dto.SaleId);
            if (sale == null)
                return false;
            
            // construct receipt content 
            string body = $@"
                <h2>Purchase Receipt</h2>
                <p>Sale ID: {sale.SaleId}</p>
                <p>Client ID: {sale.ClientId}</p>
                <p>Employee ID: {sale.EmployeeId}</p>
                <h3>Products:</h3>
                ";

            foreach (var detail in sale.SaleDetails)
            {
                body += $"<p>{detail.ProductId} * {detail.Quantity} - {detail.UnitPrice}</p>";
            }

            await _emailService.SendEmailAsync(dto.Email, "your Purchase Receipt", body);
            return true;
        }
    }
    
}