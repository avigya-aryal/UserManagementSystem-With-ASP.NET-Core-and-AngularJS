using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.ViewModel;
using ServiceLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ContactService

{
    public interface IContactsService
    {
        Task<IEnumerable<ContactsViewModel>> GellAllContactsAsync();
        Task<ContactsViewModel> GetContactAsync(int ContactID);
        Task<string> InsertContactAsync(ContactsViewModel contactmodel);
        Task<ContactsViewModel> UpdateContactAsync(ContactsViewModel contactmodel);
        Task<ContactsViewModel> DeleteContactAsync(int contactid);

        Task<IList<SelectItemIntViewModel>> GetAllContactNameAsync();
        Task<IList<SelectItemIntViewModel>> GetAllContactNameForUpdateAsync();


    }
}
