using VirtualReality.DTOs.Account;
using VirtualReality.Wrappers;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualReality.Models;

namespace VirtualReality.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request);
        Task<Response<string>> RegisterAsync(RegisterRequest request);
        Task<Response<string>> ConfirmEmailAsync(string userId, string code);
        Task ForgotPassword(ForgotPasswordRequest model, string origin);
        Task<Response<string>> ResetPassword(ResetPasswordRequest model);
        Task<ApplicationUser> GetUser(string Id);

    }
}
