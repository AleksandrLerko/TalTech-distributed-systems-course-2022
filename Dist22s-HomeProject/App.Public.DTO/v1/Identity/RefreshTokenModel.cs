using System.ComponentModel.DataAnnotations;

namespace App.Public.DTO.v1.Identity;

public class RefreshTokenModel
{
    // [StringLength(36, MinimumLength = 36)] public string Token { get; set; } = Guid.NewGuid().ToString();
    //
    // // UTC
    // public DateTime TokenExpirationDateTime { get; set; } = DateTime.UtcNow.AddDays(7);
    //
    // [StringLength(36, MinimumLength = 36)] public string? PreviousToken { get; set; } = default!;
    //
    // // UTC
    // public DateTime? PreviousTokenExpirationDateTime { get; set; }
    //
    // public Guid AppUserId { get; set; }
    // public AppUser? AppUser { get; set; }
    
    public string Jwt { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;

}