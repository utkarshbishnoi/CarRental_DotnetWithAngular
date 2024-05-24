using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface ICarOperations
    {
        Task<IEnumerable<CarEntity>> GetAllCars();
        Task<CarEntity> GetCarById(int id);

        Task<IEnumerable<CarEntity>> GetCarsByMaker(string maker);
       Task<CarEntity> AddCar(CarEntity entity);

        CarEntity UpdateCar(CarEntity entity);
        void RemoveCar(CarEntity entity);

        Task<RentalAgreementEntity> GetRentAgreementsById(int agreementId);
        Task<IEnumerable<RentalAgreementEntity>> GetAllAgreement();
        Task<IEnumerable<RentalAgreementEntity>> GetAgreementByUserId(int userId);

        Task<RentalAgreementEntity> CreateAgreement(RentalAgreementEntity agreement);
        RentalAgreementEntity UpdateAgreement(RentalAgreementEntity agreement);

        void DeleteAgreement(RentalAgreementEntity agreement);
        Task<int> SaveChanges();

        Task<User> GetUserById(int userId);

        Task<User> GetUserByEmail(string email);


    }
}