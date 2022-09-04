﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RepositoryLayer.ViewModel
{
    public class ContactsViewModel
    {
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public long ContactNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string ProfileImageUrl { get; set; }
        public string ImageName { get; set; }
     }
}
