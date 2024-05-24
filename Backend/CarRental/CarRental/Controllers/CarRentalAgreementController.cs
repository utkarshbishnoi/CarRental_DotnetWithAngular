using BLL.Facade;
using CarRental.DTO;
using DAL.Entities;
using DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CarRentalAgreementController : ControllerBase
    {
        private readonly IFacadeOperations _facade;
       
        public CarRentalAgreementController(IFacadeOperations facade,ICarOperations carOperations)
        {

            _facade = facade;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("agreements")]
        public async Task<IActionResult> GetAllAgreement()
        {
            IEnumerable<RentalAgreementEntity> agreements = await _facade.GetAllAgreement();
            foreach (var item in agreements)
            {
                User user = await _facade.GetUserById(item.UserId);
                CarEntity car = await _facade.GetCarById(item.CarId);
                item.Car = car;
                item.User = user;

            }
            return Ok(agreements);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAgreementByUserId(int userId)
        {
            IEnumerable<RentalAgreementEntity> agreementCarts = await _facade.GetAgreementByUserId(userId);
            foreach (var item in agreementCarts)
            {
                User user = await _facade.GetUserById(item.UserId);
                CarEntity car = await _facade.GetCarById(item.CarId);
                item.Car = car;
                item.User = user;

            }
            return Ok(agreementCarts);
        }
        [Authorize(Roles = "User")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateAgreement([FromBody] AgreementDTO agreementDTO)
        {
            try
            {

                CarEntity car = await _facade.GetCarById(agreementDTO.CarId);
                User user = await _facade.GetUserById(agreementDTO.UserId);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (car != null && car.AvailableQuantity > 0)
                {
                    // Map carDto to a Car entity
                    var agreement = new RentalAgreementEntity
                    {

                        CarId = agreementDTO.CarId,
                        StartDate = agreementDTO.StartDate,
                        EndDate = agreementDTO.EndDate,
                        UserId = agreementDTO.UserId,


                    };

                    TimeSpan rentalPeriod = agreementDTO.EndDate - agreementDTO.StartDate;
                    agreement.TotalCost = car.RentalPrice * rentalPeriod.Days;
                    agreement.User = user;
                    agreement.Car = car;
                    var createdagreement = await _facade.CreateAgreement(agreement);

                    car.AvailableQuantity -= 1;
                    car.TotalRented += 1;
                    if (agreement == null)
                    {
                        return StatusCode(500, "Failed to create car");
                    }
                    CarEntity updateCar = await _facade.UpdateCar(car);
                    // Return the created car
                    return Ok(agreement);
                }

                else
                {
                    return StatusCode(404, "Car not available");
                }
            }
            catch (Exception)
            {
                // Handle the exception and return an appropriate error response
                return StatusCode(500, "An error occurred while creating the agreement");
            }
        }

        [Authorize(Roles = "User")]
        [HttpPut("update/{agreementId}")]
        public async Task<IActionResult> UpdateagreementByUser(int agreementId, [FromBody] UpdateAgreementDTO updateagreementDTO)
        {
            try
            {
                RentalAgreementEntity agreement = await _facade.GetAgreementById(agreementId);


                if (agreement == null)
                {
                    return NotFound("agreement not found");
                }
                User user = await _facade.GetUserById(agreement.UserId);
                CarEntity car = await _facade.GetCarById(agreement.CarId);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!string.IsNullOrEmpty(updateagreementDTO.CarId.ToString()))
                {
                    agreement.CarId = updateagreementDTO.CarId;
                    if (agreement.CarId != updateagreementDTO.CarId)
                    {
                        car.AvailableQuantity -= 1;
                        car.TotalRented += 1;
                        await _facade.UpdateCar(car);
                    }
                }

                if (!string.IsNullOrEmpty(updateagreementDTO.StartDate.ToString()))
                {
                    agreement.StartDate = updateagreementDTO.StartDate;
                }

                if (!string.IsNullOrEmpty(updateagreementDTO.EndDate.ToString()))
                {
                    agreement.EndDate = updateagreementDTO.EndDate;
                }

                if (!string.IsNullOrEmpty(updateagreementDTO.ReqForReturn.ToString()))
                {
                    agreement.ReqForReturn = updateagreementDTO.ReqForReturn;
                    if (updateagreementDTO.ReqForReturn == true)
                    {
                        agreement.RentalStatus = "wait for incpection";
                    }
                }
                if (!string.IsNullOrEmpty(updateagreementDTO.RentalAcceptance.ToString()))
                {
                    agreement.RentalAcceptance = updateagreementDTO.RentalAcceptance;
                    if (updateagreementDTO.RentalAcceptance == true)
                    {
                        agreement.RentalStatus = "wait for approval";
                    }
                }

                TimeSpan rentalPeriod = updateagreementDTO.EndDate - updateagreementDTO.StartDate;
                agreement.TotalCost = car.RentalPrice * rentalPeriod.Days;
                agreement.User = user;
                agreement.Car = car;

                // Update the car using the facade
                var updatedagreement = await _facade.UpdateAgreement(agreement);

                // Return the updated car
                return Ok(updatedagreement);
            }
            catch (Exception)
            {
                // Handle the exception and return an appropriate error response
                return StatusCode(500, "An error occurred while update the agreement");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("admin/update/{agreementId}")]
        public async Task<IActionResult> UpdateagreementByAdmin(int agreementId, [FromBody] UpdateAgreementDTO updateagreementDTO)
        {
            try
            {
                RentalAgreementEntity agreement = await _facade.GetAgreementById(agreementId);


                if (agreement == null)
                {
                    return NotFound("agreement not found");
                }
                User user = await _facade.GetUserById(agreement.UserId);
                CarEntity car = await _facade.GetCarById(agreement.CarId);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!string.IsNullOrEmpty(updateagreementDTO.CarId.ToString()))
                {
                    agreement.CarId = updateagreementDTO.CarId;
                    if (agreement.CarId != updateagreementDTO.CarId)
                    {
                        car.AvailableQuantity -= 1;
                        car.TotalRented += 1;
                        await _facade.UpdateCar(car);
                    }
                }
                if (!string.IsNullOrEmpty(updateagreementDTO.StartDate.ToString()))
                {
                    agreement.StartDate = updateagreementDTO.StartDate;
                }
                if (!string.IsNullOrEmpty(updateagreementDTO.EndDate.ToString()))
                {
                    agreement.EndDate = updateagreementDTO.EndDate;
                }
                if (!string.IsNullOrEmpty(updateagreementDTO.RentalStatus.ToString()))
                {
                    agreement.RentalStatus = updateagreementDTO.RentalStatus;
                }
                if (!string.IsNullOrEmpty(updateagreementDTO.RentalAcceptance.ToString()))
                {
                    agreement.RentalAcceptance = updateagreementDTO.RentalAcceptance;
                }

                TimeSpan rentalPeriod = updateagreementDTO.EndDate - updateagreementDTO.StartDate;
                agreement.TotalCost = car.RentalPrice * rentalPeriod.Days;
                agreement.User = user;
                agreement.Car = car;

                // Update the car using the facade
                var updatedagreement = await _facade.UpdateAgreement(agreement);

                // Return the updated car
                return Ok(updatedagreement);
            }
            catch (Exception)
            {
                // Handle the exception and return an appropriate error response
                return StatusCode(500, "An error occurred while update the agreement");
            }
        }

        [Authorize(Roles = "User")]
        [HttpDelete("delete/{agreementId}")]
        public async Task<IActionResult> DeleteagreementByUser(int agreementId)
        {
            try
            {
                RentalAgreementEntity deleteagreement = await _facade.GetAgreementById(agreementId);
                if (deleteagreement != null && !deleteagreement.RentalAcceptance)
                {
                    await _facade.DeleteAgreement(deleteagreement);
                    return StatusCode(200, "agreement Deleted Successfully");
                }
                else
                {
                    return BadRequest("Not allowed or agreement not found");
                }

            }
            catch (Exception)
            {
                // Handle the exception and return an appropriate error response
                return StatusCode(500, "An error occurred while deleting the agreement");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("admin/delete/{agreementId}")]
        public async Task<IActionResult> DeleteagreementByAdmin(int agreementId)
        {
            try
            {
                RentalAgreementEntity deleteagreement = await _facade.GetAgreementById(agreementId);
                if (deleteagreement == null)
                {
                    return BadRequest("agreement not found");
                }
                else
                {
                    await _facade.DeleteAgreement(deleteagreement);
                    return StatusCode(200, "agreement Deleted Successfully");
                }
            }
            catch (Exception)
            {
                // Handle the exception and return an appropriate error response
                return StatusCode(500, "An error occurred while deleting the agreement");
            }
        }
    }
}
