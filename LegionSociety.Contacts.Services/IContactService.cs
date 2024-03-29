﻿using LegionSociety.Contacts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LegionSociety.Contacts.Services
{
    public interface IContactService
    {
        Task<ContactDetailModel> Get(long id);
        Task<ContactEditModel> GetEdit(long id);
        Task<string> Update(ContactEditModel contact);
    }
}
