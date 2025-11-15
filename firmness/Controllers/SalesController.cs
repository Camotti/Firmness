using Microsoft.AspNetCore.Mvc;
using firmness.Application.Interfaces;
using firmness.Web.ViewModels;


namespace firmness.Web.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISalesService _salesService;
        private readonly IProductService _productService;
        private readonly IClientService _clientService;
        private readonly IEmployeeService _employeeService;

        public SalesController(
            ISalesService salesService,
            IProductService productService,
            IClientService clientService,
            IEmployeeService employeeService)
        {
            _salesService = salesService;
            _productService = productService;
            _clientService = clientService;
            _employeeService = employeeService;
        }
    }
}