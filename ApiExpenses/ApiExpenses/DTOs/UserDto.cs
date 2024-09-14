using ApiExpenses.Models;

namespace ApiExpenses.DTOs
{
    public class UserDto
    {
        public string Id { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
