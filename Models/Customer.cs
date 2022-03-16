using Online.DTOs;

namespace Online.Models;

public record Customer
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public long Mobile { get; set; }

    public CustomerDTO asDto => new CustomerDTO
    {
        Id = Id,
        Name = Name,
        Gender = Gender,
        DateOfBirth = DateOfBirth,
        Email = Email,
        Mobile = Mobile,
    };
}