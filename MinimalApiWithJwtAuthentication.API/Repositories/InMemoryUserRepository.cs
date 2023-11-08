using MinimalApiWithJwtAuthentication.API.Interfaces;
using MinimalApiWithJwtAuthentication.API.Models;

namespace MinimalApiWithJwtAuthentication.API.Repositories;

public class InMemoryUserRepository : IUserRepository
{
  private readonly List<User> _users = new()
  {
    new User
    {
      Id = 1,
      FirstName = "1",
      LastName = "1",
      Username = "GG",
      Password = "1234"
    },
    new User
    {
      Id = 2,
      FirstName = "2",
      LastName = "2",
      Username = "BG",
      Password = "0987"
    }
  };

  public Task<User?> Get(string username, string password)
  {
    return Task.FromResult(_users.FirstOrDefault(u => username == u.Username && password == u.Password));
  }
}