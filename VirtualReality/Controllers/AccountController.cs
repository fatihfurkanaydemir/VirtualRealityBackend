﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualReality.DTOs.Account;
using VirtualReality.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace VirtualReality.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        private readonly IAuthenticatedUserService _authenticatedUserService;
        public AccountController(IAccountService accountService, IAuthenticatedUserService authenticatedUserService)
        {
            _accountService = accountService;
            _authenticatedUserService = authenticatedUserService;   
        }

        [HttpGet("get-user")]
        public async Task<IActionResult> getUser()
        {
          return Ok(await _accountService.GetUser(_authenticatedUserService.UserId));
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateAsync(request));
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            return Ok(await _accountService.RegisterAsync(request));
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery]string userId, [FromQuery]string code)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ConfirmEmailAsync(userId, code));
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            await _accountService.ForgotPassword(model, Request.Headers["origin"]);
            return Ok();
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            
            return Ok(await _accountService.ResetPassword(model));
        }
        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress==null ? "127.0.0.1":HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}