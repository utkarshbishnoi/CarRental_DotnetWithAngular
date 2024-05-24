using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;

namespace BLL.Facade
{
    public class FacadeOperations :  IFacadeOperations
    {
        private readonly DAL.Repository.ICarOperations _DAL;

        public FacadeOperations(DAL.Repository.ICarOperations dal)
        {
            _DAL = dal;
           
        }

        public async Task<IEnumerable<CarEntity>> GetAllCars()
        {
            IEnumerable<CarEntity> cars = await this._DAL.GetAllCars();
            return cars;
        }

        public async Task<CarEntity> GetCarById(int id)
        {

            CarEntity car = await this._DAL.GetCarById(id);
            return car;
        }

        public async Task<IEnumerable<CarEntity>> GetCarsByMaker(string category)
        {
            IEnumerable<CarEntity> cars = await this._DAL.GetCarsByMaker(category);
            return cars;
        }

        public async Task<CarEntity> AddCar(CarEntity car)
        {
            CarEntity newcar = await this._DAL.AddCar(car);
            await this._DAL.SaveChanges();
            return newcar;
        }

        public async Task<CarEntity> UpdateCar(CarEntity car)
        {
            CarEntity updatecar = this._DAL.UpdateCar(car);
            await this._DAL.SaveChanges();
            return updatecar;

        }

        public async Task<bool> RemoveCar(CarEntity car)
        {
            this._DAL.RemoveCar(car);
            await this._DAL.SaveChanges();
            return true;

        }

        public async Task<IEnumerable<RentalAgreementEntity>> GetAllAgreement()
        {
            IEnumerable<RentalAgreementEntity> agreement = await this._DAL.GetAllAgreement();
            return agreement;
        }

        public async Task<RentalAgreementEntity> GetAgreementById(int aggrementId)
        {
            RentalAgreementEntity agreement = await this._DAL.GetRentAgreementsById(aggrementId);
            return agreement;
        }
        public async Task<IEnumerable<RentalAgreementEntity>> GetAgreementByUserId(int userId)
        {
            IEnumerable<RentalAgreementEntity> agreement = await this._DAL.GetAgreementByUserId(userId);
            return agreement;
        }

        public async Task<RentalAgreementEntity> CreateAgreement(RentalAgreementEntity agreement)
        {
            RentalAgreementEntity newagreement = await this._DAL.CreateAgreement(agreement);
            await this._DAL.SaveChanges();
            return newagreement;
        }

        public async Task<RentalAgreementEntity> UpdateAgreement(RentalAgreementEntity agreement)
        {
            RentalAgreementEntity updatedagreement = this._DAL.UpdateAgreement(agreement);
            await this._DAL.SaveChanges();
            return updatedagreement;
        }

        public async Task<bool> DeleteAgreement(RentalAgreementEntity agreement)
        {
            this._DAL.DeleteAgreement(agreement);
            await this._DAL.SaveChanges();
            return true;
        }


        public async Task<User> GetUserById(int userId)
        {
            User user = await this._DAL.GetUserById(userId);
            return user;
        }


        public async Task<User> GetUserByEmail(string email)
        {
            User user = await this._DAL.GetUserByEmail(email);
            return user;
        }


    }
}



