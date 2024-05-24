using BLL;
using DAL.Repository;
using DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Facade;
using Microsoft.AspNetCore.Authorization;
using DAL.Entities;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CarController : ControllerBase
    {

        private readonly IFacadeOperations _facadeOperations;
        public CarController(IFacadeOperations carService, ICarOperations carOperations)
        {
            _facadeOperations = carService;
        }

        [HttpGet("cars")]
        public async Task<IActionResult> GetAllCars()
        {
            try
            {
                IEnumerable<CarEntity> cars = await _facadeOperations.GetAllCars();
                return Ok(cars);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while fetching the car");
            }

        }
        [HttpGet("cars/maker/{maker}")]
        public async Task<IActionResult> GetCarsByMaker(string maker)
        {
            IEnumerable<CarEntity> cars = await _facadeOperations.GetCarsByMaker(maker);
            return Ok(cars);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateCar([FromBody] CarDTO CarDTO)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var car = new CarEntity
                {

                    AvailableQuantity = CarDTO.AvailableQuantity,
                    Image = CarDTO.Image,
                    RentalPrice = CarDTO.RentalPrice,
                    Maker = CarDTO.Maker,
                    Model = CarDTO.Model,


                };
                var createdCar = await _facadeOperations.AddCar(car);

                if (createdCar == null)
                {
                    return StatusCode(500, "Failed to create car");
                }

                return Ok(createdCar);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the car");
            }
        }
       // [Authorize(Roles = "Admin")]
        [HttpPut("update/{carId}")]
        public async Task<IActionResult> UpdateCarAsync(int carId, [FromBody] UpdateCarDTO updateCarDTO)
        {
            try
            {
                CarEntity car = await _facadeOperations.GetCarById(carId);

                if (car == null)
                {
                    return NotFound("Car not found");
                }

                if (!string.IsNullOrEmpty(updateCarDTO.Image))
                {
                    car.Image = updateCarDTO.Image;
                }

                if (!string.IsNullOrEmpty(updateCarDTO.Maker))
                {
                    car.Maker = updateCarDTO.Maker;
                }

                if (!string.IsNullOrEmpty(updateCarDTO.Model))
                {
                    car.Model = updateCarDTO.Model;
                }

                if (updateCarDTO.AvailableQuantity > 0)
                {
                    car.AvailableQuantity = updateCarDTO.AvailableQuantity;
                }

                if (updateCarDTO.RentalPrice > 0)
                {
                    car.RentalPrice = updateCarDTO.RentalPrice;
                }

                var updatedCar = await _facadeOperations.UpdateCar(car);

                return Ok(updatedCar);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the car");
            }
        }
        [HttpGet("{carId}")]
        public async Task<IActionResult> GetCarByIdAsync(int carId)
        {
            try
            {
                CarEntity car = await _facadeOperations.GetCarById(carId);

                if (car == null)
                {
                    return NotFound("Car not found");
                }

                return Ok(car);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the car");
            }
        }
       // [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{carId}")]
        public async Task<IActionResult> DeleteCar(int carId)
        {
            try
            {
                CarEntity car = await _facadeOperations.GetCarById(carId);

                if (car == null)
                {
                    return NotFound("Car not found");
                }

                await _facadeOperations.RemoveCar(car);

                return Ok("Car deleted successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the car");
            }
        }
    }



}






