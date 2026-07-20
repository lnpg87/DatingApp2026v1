namespace API.DTOs;

public class UserSeedDto
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public required string Gender { get; set; }
    public required string DateOfBirth { get; set; }
    public required string DisplayName { get; set; }
    public required string Created { get; set; }
    public required string LastActive { get; set; }
    public string? Description { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public string? ImageUrl { get; set; }
}
