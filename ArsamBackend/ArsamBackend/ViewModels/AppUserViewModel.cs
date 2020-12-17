﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ArsamBackend.Controllers;
using ArsamBackend.Models;
using ArsamBackend.Services;
using ArsamBackend.Utilities;

namespace ArsamBackend.ViewModels
{
    public class OutputAppUserViewModel
    {

        public OutputAppUserViewModel(AppUser user)
        {
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Description = user.Description;
            Fields = CategoryService.ConvertCategoriesToList(user.Fields);
            CreatedEvents = user.CreatedEvents.ToList().Select(x => new OutputAbstractViewModel(x)).ToList();
            InEvents = user.InEvents.ToList().Select(x => new OutputAbstractViewModel(x)).ToList();
            Image = user.ImageLink;
            Tickets = user.Tickets.Select(x => new TicketProfileViewModel(x)).ToList();
            Balance = user.Balance;
        }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public long Balance { get; set; }
        public ICollection<int> Fields { get; set; }
        public ICollection<OutputAbstractViewModel> InEvents { get; set; }
        public ICollection<OutputAbstractViewModel> CreatedEvents { get; set; }
        public ICollection<TicketProfileViewModel> Tickets { get; set; }



    }

    public class UpdateProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public List<int> Fields { get; set; }
    }

    public class EventOutputAppUserViewModel
    {
        public EventOutputAppUserViewModel(AppUser user)
        {
            Email = user.Email;
        }

        public string Email { get; set; }
    }

    public class TicketOutputAppUserViewModel
    {
        public TicketOutputAppUserViewModel(AppUser user)
        {
            Email = user.Email;
            Tickets = user.Tickets.Select(x => x.Id).ToList();
        }

        public string Email { get; set; }
        public List<int> Tickets { get; set; }
    }
}
