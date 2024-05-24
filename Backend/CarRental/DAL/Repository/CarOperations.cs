using DAL.Data;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class CarOperations : ICarOperations
    {
        private readonly DataContext _context;

        public CarOperations(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CarEntity>> GetAllCars()
        {
            return _context.Cars.ToList();
        }


        public async Task<CarEntity> GetCarById(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task<IEnumerable<CarEntity>> GetCarsByMaker(string maker)
        {
            return await _context.Cars
                .Where(p => p.Maker == maker)
                .ToListAsync();
        }

        public Task<CarEntity> AddCar(CarEntity entity)
        {
            _context.Cars.Add(entity);
            return Task.FromResult(entity);
        }

        public CarEntity UpdateCar(CarEntity entity)
        {

            _context.Cars.Update(entity);
            return entity;
        }

        public void RemoveCar(CarEntity entity)
        {
            _context.Cars.Remove(entity);
            
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
        //Car Agreement operations
        public async Task<RentalAgreementEntity> GetRentAgreementsById(int agreementId)
        {
            return await _context.RentalAgreements.FindAsync(agreementId);
        }
        public async Task<IEnumerable<RentalAgreementEntity>> GetAllAgreement()
        {
            return await _context.RentalAgreements.ToListAsync();
        }
        public async Task<IEnumerable<RentalAgreementEntity>> GetAgreementByUserId(int userId)
        {
            return await _context.RentalAgreements.Where(p => p.UserId == userId).ToListAsync();
        }
        public Task<RentalAgreementEntity> CreateAgreement(RentalAgreementEntity agreement)
        {
            _context.RentalAgreements.Add(agreement);

            return Task.FromResult(agreement);
        }
        public RentalAgreementEntity UpdateAgreement(RentalAgreementEntity agreement)
        {
            _context.RentalAgreements.Update(agreement);
            return agreement;
        }
        public void DeleteAgreement(RentalAgreementEntity agreement)
        {
            _context.RentalAgreements.Remove(agreement);
        }

        //User operations
        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }



    }

}





