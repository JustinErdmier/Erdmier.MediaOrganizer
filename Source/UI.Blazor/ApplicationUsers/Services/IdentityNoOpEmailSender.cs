using Erdmier.MediaOrganizer.Domain.ApplicationUsers.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Erdmier.MediaStorage.UI.Blazor.ApplicationUsers.Services;

// Remove the "else if (EmailSender is IdentityNoOpEmailSender)" block from RegisterConfirmation.razor after updating with a real implementation.
internal sealed class IdentityNoOpEmailSender : IEmailSender<ApplicationUser>
{
    private readonly IEmailSender emailSender = new NoOpEmailSender();

    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) =>
        emailSender.SendEmailAsync(email,
                                   subject: "Confirm your email",
                                   htmlMessage: $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
        emailSender.SendEmailAsync(email,
                                   subject: "Reset your password",
                                   htmlMessage: $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
        emailSender.SendEmailAsync(email, subject: "Reset your password", htmlMessage: $"Please reset your password using the following code: {resetCode}");
}
