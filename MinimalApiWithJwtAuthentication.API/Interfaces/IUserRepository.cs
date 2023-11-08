using MinimalApiWithJwtAuthentication.API.Models;

namespace MinimalApiWithJwtAuthentication.API.Interfaces;

public interface IUserRepository
{
  Task<User?> Get(string userName, string password);
}