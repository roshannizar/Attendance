using Attendance.Api.DbContexts;
using Attendance.Api.Enums;
using Attendance.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Attendance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly AttendanceDBContext context;

        public DeliveryController(AttendanceDBContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public ActionResult CreateDelivery(Delivery delivery)
        {
            delivery.Id = Guid.NewGuid().ToString();
            delivery.Active = Enums.RecordState.Active;

            var response = context.Employees.FirstOrDefault(x => x.Id == delivery.EmployeeId);

            if(response == null)
            {
                return new JsonResult(new { message = "Employee not found" }) { StatusCode = StatusCodes.Status404NotFound };
            } else
            {
                var deliveryResponse = context.Delivery.FirstOrDefault(x => x.EmployeeId == delivery.EmployeeId && (delivery.Status == DeliveryStatus.InQueue || delivery.Status == DeliveryStatus.OutForDelivery));

                if(deliveryResponse != null)
                {
                    return new JsonResult(new { message = "Employee not found" }) { StatusCode = StatusCodes.Status404NotFound };
                }

                if(deliveryResponse.Status == DeliveryStatus.OutForDelivery)
                {
                    return Ok("Out for delivery, complete and come back");
                }

                if (deliveryResponse.Status == DeliveryStatus.InQueue)
                {
                    return Ok("In queue, complete and come back");
                }
                context.Delivery.Add(delivery);
                context.SaveChanges();
            }

            return Created("User queued", delivery);
        }

        [HttpPut]
        public ActionResult UpdateDeliveryStatus(Delivery delivery)
        {
            var response = context.Employees.FirstOrDefault(x => x.Id == delivery.EmployeeId);

            if (response == null)
            {
                return new JsonResult(new { message = "Employee not found" }) { StatusCode = StatusCodes.Status404NotFound };
            }
            else
            {
                var deliveryResponse = context.Delivery.FirstOrDefault(x => x.EmployeeId == delivery.EmployeeId && (delivery.Status == DeliveryStatus.InQueue || delivery.Status == DeliveryStatus.OutForDelivery));

                if(deliveryResponse != null)
                {
                    deliveryResponse.Status = DeliveryStatus.Completed;
                    context.Delivery.Update(deliveryResponse);
                    context.SaveChanges();
                } else
                {
                    return new JsonResult(new { message = "Delivery not found" }) { StatusCode = StatusCodes.Status404NotFound };
                }
            }

            return Created("User queued", delivery);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Delivery>> GetDeliveries()
        {
            var response = context.Delivery.ToList();

            return Ok(response);
        }

        [HttpGet("{Id}")]
        public ActionResult<Delivery> GetDelivery(string Id)
        {
            var response = context.Delivery.FirstOrDefault(e => e.Id == Id);

            return Ok(response);
        }

    }
}
