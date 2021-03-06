﻿using ArsamBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArsamBackend.ViewModels
{
    public class TicketOutputViewModel
    {
        public TicketOutputViewModel(Ticket ticket)
        {
            Id = ticket.Id;
            Event = ticket.EventId;
            Type = ticket.Type.Name;
            User = ticket.User.UserName;
            TypeId = ticket.Type.ID;
            Price = ticket.Type.Price;
        }

        public int Id { get; set; }
        public int Event { get; set; }
        public string Type { get; set; }
        public int TypeId { get; set; }
        public long Price { get; set; }
        public string User { get; set; }

    }

    public class TicketProfileViewModel
    {
        public TicketProfileViewModel(Ticket t)
        {
            EventName = t.Event.Name;
            TicketTypeName = t.Type.Name;
            Price = t.Type.Price;
            TicketId = t.Id;
            EventEndDate = t.Event.EndDate;
            EventId = t.EventId;
        }
        public int TicketId { get; set; }
        public string EventName { get; set; }
        public string TicketTypeName { get; set; }
        public long Price { get; set; }
        public int EventId { get; set; }
        public DateTime EventEndDate { get; set; }

    }
}
