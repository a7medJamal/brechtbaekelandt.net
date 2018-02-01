﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using brechtbaekelandt.Data;
using brechtbaekelandt.Extensions;
using brechtbaekelandt.Identity;
using brechtbaekelandt.Models;
using brechtbaekelandt.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace brechtbaekelandt.Controllers
{
    public class AdminController : BaseController
    {
        private readonly BlogDbContext _blogDbContext;

        private readonly ApplicationUserManager _applicationUserManager;

        private const int _postsPerPage = 5;

        public AdminController(BlogDbContext blogDbContext, ApplicationUserManager applicationUserManager) : base(applicationUserManager)
        {
            this._blogDbContext = blogDbContext;
            this._applicationUserManager = applicationUserManager;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new AdminViewModel
            {
                CurrentUser = Mapper.Map<User>(await this._applicationUserManager.FindByNameAsync(HttpContext.User.Identity.Name)),
                Categories = Mapper.Map<ICollection<Models.Category>>(this._blogDbContext.Categories).OrderBy(c => c.Name).ToCollection()
            };

            return View(viewModel);
        }
    }
}