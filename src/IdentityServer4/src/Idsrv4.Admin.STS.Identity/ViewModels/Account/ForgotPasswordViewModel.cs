using System.ComponentModel.DataAnnotations;
using Idsrv4.Admin.Shared.Configuration.Configuration.Identity;

namespace Idsrv4.Admin.STS.Identity.ViewModels.Account;

public class ForgotPasswordViewModel
{
    [Required] public LoginResolutionPolicy? Policy { get; set; }

    [EmailAddress] public string Email { get; set; }

    public string Username { get; set; }
}