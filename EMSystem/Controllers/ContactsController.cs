using DomainLayer.Models;
//using grpc.core;
//using magnum.filesystem;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.ViewModel;
using ServiceLayer;
using ServiceLayer.ContactService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService _contactService;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ContactsController(IContactsService contactService, IWebHostEnvironment webHostEnvironment)
        {
            _contactService = contactService;
            this._webHostEnvironment = webHostEnvironment;
        }


        [HttpPost(nameof(InsertContact))]
        public async Task<IActionResult> InsertContact(ContactsViewModel contactmodel)
        {
            string result = await _contactService.InsertContactAsync(contactmodel);
            if (result != null)
            {

                return Ok("Data Inserted");
            }
            return BadRequest(new { result = "Unable to insert data" });
        }


        [HttpGet(nameof(GetAllContact))]
        public async Task<IActionResult> GetAllContact()
        {
            var result = await _contactService.GellAllContactsAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");
        }




        [HttpGet(nameof(GetContact))]
        public async Task<IActionResult> GetContact(int contactID)
        {
            var result = await _contactService.GetContactAsync(contactID);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");
        }



        [HttpPut(nameof(UpdateContact))]
        public async Task<IActionResult> UpdateContact(ContactsViewModel contact)
        {
            await _contactService.UpdateContactAsync(contact);
            return Ok("Updation Done");
        }

        [HttpDelete(nameof(DeleteContact))]
        public async Task<IActionResult> DeleteContact(int ContactID)
        {
            await _contactService.DeleteContactAsync(ContactID);
            return Ok("Data deleted");
        }

        [HttpGet(nameof(GetAllContactName))]
        public async Task<IActionResult> GetAllContactName()
        {
            var contacts = await _contactService.GetAllContactNameAsync();
            return Ok(contacts);
        }

        [HttpGet(nameof(GetAllContactNameForUpdate))]
        public async Task<IActionResult> GetAllContactNameForUpdate()
        {
            var contacts = await _contactService.GetAllContactNameForUpdateAsync();
            return Ok(contacts);
        }


        [HttpPost(nameof(UploadFiles))]
        public async Task<string> UploadFiles(IFormFile file)
        {
            var contentType = file.ContentType;
            var ext = contentType.Substring(contentType.LastIndexOf('/') + 1);

            var fileName = file.FileName + "." + ext;
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, @"wwwroot\Images", fileName);

            // var ext = Path.GetExtension(file.Name);

            if (ext == "jpeg" || ext == "png")
            {
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }

            var path = "/Images/" + fileName;
            //file.SaveAs(path);
            return path;
        }


    }
}




    


        
 

