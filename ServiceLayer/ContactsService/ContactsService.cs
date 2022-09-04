using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer;
using RepositoryLayer.DapperRepository;
using RepositoryLayer.Repository;
using RepositoryLayer.ViewModel;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ContactService
{
    public class ContactsService : IContactsService
    {
        private IRepository<Contacts> _contactRepository;
        private IApplicationReadDbConnection _applicationReadDbConnection;
        public ContactsService(IRepository<Contacts> repository, IApplicationReadDbConnection applicationReadDbConnection)
        {
            _contactRepository = repository;
            _applicationReadDbConnection = applicationReadDbConnection;
        }

       

        public async Task<string> InsertContactAsync(ContactsViewModel contactmodel)
        {
            string result;
            var contact = new Contacts();

            var isContactNumberExist = (await _contactRepository.FindByConditionAsync(x => x.ContactNumber == contactmodel.ContactNumber)).Any();
            if (isContactNumberExist)
            {
                result = "Contact Already Exist";
            }
            else
            {
                contact.FirstName = contactmodel.FirstName;
                contact.MiddleName =(string)contactmodel.MiddleName;
                contact.LastName = contactmodel.LastName;
                contact.ContactNumber = contactmodel.ContactNumber;
                contact.DateOfBirth = contactmodel.DateOfBirth;
                contact.Address = (string)contactmodel.Address;
                contact.Email = contactmodel.Email;
                contact.Gender = contactmodel.Gender;
                
                contact.CreatedTS = DateTime.Now;
                await _contactRepository.InsertAsync(contact);
                await _contactRepository.SaveChangesAsync();
                result = "Added Successfully!";
            }

            return result;

        }


        public async Task<IEnumerable<ContactsViewModel>> GellAllContactsAsync()
        {
            var query = $"SELECT * FROM Contacts";
            var contacts = await _applicationReadDbConnection.QueryAsync<ContactsViewModel>(query);
            return contacts;
            // return await _contactRepository.FindAllAsync();
        }


        public async Task<ContactsViewModel> GetContactAsync(int contactID)
        {
            var result = await _contactRepository.FindByConditionAsync(x => x.ContactID == contactID);
            var contact = result.Select(x => new ContactsViewModel
            {
                ContactID = x.ContactID,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                ContactNumber = x.ContactNumber,
                DateOfBirth = x.DateOfBirth,
                Address = (string)x.Address,
                Email = x.Email,
                Gender=x.Gender,
                ProfileImageUrl = x.ProfileImageUrl,
                ImageName = x.ImageName,

            }).FirstOrDefault();

            //null data
            //null case handle
            return contact;
            
        }




        public async Task<ContactsViewModel> UpdateContactAsync(ContactsViewModel contactmodel)
        {

            var doesContactExist = (await _contactRepository.FindByConditionAsync(x => x.ContactID == contactmodel.ContactID)).FirstOrDefault();
            if (doesContactExist != null)
            {
                 
                doesContactExist.FirstName = contactmodel.FirstName;
                doesContactExist.MiddleName = contactmodel.MiddleName;
                doesContactExist.LastName = contactmodel.LastName;
                doesContactExist.ContactNumber =contactmodel.ContactNumber;
                doesContactExist.DateOfBirth = contactmodel.DateOfBirth;
                doesContactExist.Address = contactmodel.Address;
                doesContactExist.Email = contactmodel.Email;
                doesContactExist.Gender = contactmodel.Gender;
                doesContactExist.ProfileImageUrl = contactmodel.ProfileImageUrl;
                doesContactExist.ImageName = contactmodel.ImageName;
              
                await _contactRepository.UpdateAsync(doesContactExist);
                await _contactRepository.SaveChangesAsync();
            }
            return contactmodel;
        }

        public async Task<ContactsViewModel> DeleteContactAsync(int contactid)
        {
            var isContactExist = (await _contactRepository.FindByConditionAsync(x => x.ContactID == contactid)).FirstOrDefault();
            if (isContactExist != null)
            {
                await _contactRepository.DeleteAsync(isContactExist);
                await _contactRepository.SaveChangesAsync();
            }
            return null;
        }

        public async Task<IList<SelectItemIntViewModel>> GetAllContactNameAsync()
        {
            var query = @"select Contacts.ContactID AS ID, CONCAT(Contacts.FirstName, ' ' ,Contacts.MiddleName, ' ', Contacts.LastName) AS Name 
                            FROM Contacts
                            left join Employee on Contacts.ContactID = Employee.ContactID where Employee.ContactID is null ";
            var employee = await _applicationReadDbConnection.QueryAsync<SelectItemIntViewModel>(query);
            return employee.ToList();
        }

        public async Task<IList<SelectItemIntViewModel>> GetAllContactNameForUpdateAsync()
        {
            var query = @"select Contacts.ContactID AS ID, CONCAT(Contacts.FirstName, ' ' ,Contacts.MiddleName, ' ', Contacts.LastName) AS Name 
                            FROM Contacts
                            left join Employee on Contacts.ContactID = Employee.ContactID where Employee.ContactID is not null ";
            var employee = await _applicationReadDbConnection.QueryAsync<SelectItemIntViewModel>(query);
            return employee.ToList();
        }



    }
}



