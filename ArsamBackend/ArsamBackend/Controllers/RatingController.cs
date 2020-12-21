﻿using ArsamBackend.Models;
using ArsamBackend.Services;
using ArsamBackend.Utilities;
using ArsamBackend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArsamBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ILogger<AccountController> logger;
        private readonly IDataProtectionProvider dataProtectionProvider;
        private readonly DataProtectionPurposeStrings dataProtectionPurposeStrings;
        private readonly IJWTService jWTHandler;
        private readonly IMinIOService minIO;
        private readonly IJWTService jwtHandler;

        public RatingController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILogger<AccountController> logger, IDataProtectionProvider dataProtectionProvider, DataProtectionPurposeStrings dataProtectionPurposeStrings, IJWTService jWTHandler, IMinIOService minIO, IJWTService jwtHandler)
        {
            this._context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.dataProtectionProvider = dataProtectionProvider;
            this.dataProtectionPurposeStrings = dataProtectionPurposeStrings;
            this.jWTHandler = jWTHandler;
            this.minIO = minIO;
            this.jwtHandler = jwtHandler;
        }

        [HttpPut]
        public async Task<ActionResult<AppUser>> RateEvent(RateEventViewModel model)
        {
            AppUser user = await jwtHandler.FindUserByTokenAsync(Request.Headers[HeaderNames.Authorization], _context);
            var ev = _context.Events.Find(model.Id);
            if (ev == null) return NotFound("Event not found");
            Rating rating = new Rating()
            {
                Event = ev,
                Stars = model.Stars,
                User = user
            };
            if (!CanRate(ev, user)) return BadRequest("user can not rate the event");
            var temp = ev.Ratings.Where(x => x.User == user).First();
            if (temp != null) ev.Ratings.Remove(temp);
            _context.Ratings.Add(rating);
            ev.AveragedRating = ((ev.AveragedRating * ev.Ratings.Count) + (double)rating.Stars) / (ev.Ratings.Count + 1);
            _context.SaveChanges();
            return Ok(new OutputEventViewModel(ev));
        }

        [NonAction]
        public bool CanRate(Event ev, AppUser user)
        {
            if (ev.IsProject) return false;
            if (DateTime.Now < ev.EndDate) return false;
            if (!ev.EventMembers.Contains(user)) return false;
            return true;
        }

        [NonAction]
        public double ComputeAverageRating(Event ev)
        {
            double sum = 0;
            foreach (Rating rating in ev.Ratings)
            {
                sum += (double) rating.Stars;
            }
            sum /= ev.Ratings.Count;
            return sum;
        }

    }
}
