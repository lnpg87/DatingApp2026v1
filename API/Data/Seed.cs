using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(AppDbContext context)
    {
        if (await context.Users.AnyAsync()) return;

        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var seedData = JsonSerializer.Deserialize<List<UserSeedDto>>(userData, options);

        if (seedData == null) return;

        foreach (var item in seedData)
        {   
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                Id = item.Id,
                Email = item.Email.ToLower(),
                DisplayName = item.DisplayName,
                ImageUrl = item.ImageUrl,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd")),
                PasswordSalt = hmac.Key
            };

            var member = new Member
            {
                Id = item.Id,
                DateOfBirth = DateOnly.Parse(item.DateOfBirth),
                ImageUrl = item.ImageUrl,
                DisplayName = item.DisplayName,
                Created = DateTime.Parse(item.Created),
                LastActive = DateTime.Parse(item.LastActive),
                Gender = item.Gender,
                Description = item.Description,
                City = item.City,
                Country = item.Country,
                AppUser = user
            };

            if (item.ImageUrl != null)
            {
                member.Photos.Add(new Photo
                {
                    Url = item.ImageUrl,
                    MemberId = member.Id
                });
            }

            user.Member = member;

            context.Users.Add(user);
        }

        await context.SaveChangesAsync();
    } 
}