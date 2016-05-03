using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Odonit.DAL.Models;

namespace Odonit.Business
{
    public interface IServiceFactory
    {
        IService<Person> CreatePersonService();
        IService<MedicalHistory> CreateMedicalHistoryService();
        IService<ToothService> CreateToothServiceService();
        IService<Treatment> CreateTreatmentService();
        IService<Service> CreateServicesService();
        IService<MedicalSecurity> CreateMedicalSecurityService();
        IService<Address> CreateAddressService();
        IService<Contact> CreateContactService();
        IService<Exam> CreateExamService();
    }
}
