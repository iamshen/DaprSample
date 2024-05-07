using System.ComponentModel.DataAnnotations;

namespace Idsrv4.Admin.STS.Identity.ViewModels.Account;

public class LoginWithRecoveryCodeViewModel
{
    [Required] [DataType(DataType.Text)] public string RecoveryCode { get; set; }

    public string ReturnUrl { get; set; }
}