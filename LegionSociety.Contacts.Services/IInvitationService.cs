using LegionSociety.Contacts.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LegionSociety.Contacts.Services
{
    public interface IInvitationService
    {
        Invitation Create(string emailAddress);
    }
}
