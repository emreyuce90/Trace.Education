using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trace.Education.Order.API.Feature.Order;

namespace Trace.Education.Order.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ORdersController(IOrderService orderService) : ControllerBase {

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateDto dto) {
            await orderService.CreateAsync(dto);
            return Ok(new {OrderId=55});
        }
            
    }
}
