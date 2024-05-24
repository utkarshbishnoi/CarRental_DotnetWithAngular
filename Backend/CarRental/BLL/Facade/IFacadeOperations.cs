using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Facade
{
    public interface IFacadeOperations
    {
        Task<IEnumerable<CarEntity>> GetAllCars();

        Task<CarEntity> GetCarById(int id);
        Task<IEnumerable<CarEntity>> GetCarsByMaker(string category);
        Task<CarEntity> AddCar(CarEntity car);

        Task<CarEntity> UpdateCar(CarEntity car);
        Task<bool> RemoveCar(CarEntity car);
        Task<IEnumerable<RentalAgreementEntity>> GetAllAgreement();
        Task<RentalAgreementEntity> GetAgreementById(int aggrementId);
        Task<IEnumerable<RentalAgreementEntity>> GetAgreementByUserId(int userId);

        Task<RentalAgreementEntity> CreateAgreement(RentalAgreementEntity agreement);

        Task<RentalAgreementEntity> UpdateAgreement(RentalAgreementEntity agreement);
        Task<bool> DeleteAgreement(RentalAgreementEntity agreement);

        Task<User> GetUserById(int userId);
        Task<User> GetUserByEmail(string email);
    }
}