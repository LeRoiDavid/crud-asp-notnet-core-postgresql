using Microsoft.EntityFrameworkCore;

namespace ApiUser.Services;

using ApiUser.Entities;
using ApiUser.Helpers;

public interface IUserService
{
    IEnumerable<User> GetAll( String? query, int page = 1, int take = 10);
    User GetById(int id);
    User Create(User user);
    User Update(int id, User user);
    void Delete(int id);
    int Count(String? query);
}

public class UserService : IUserService
{
    private DatabaseContext _context;
    private int take = 10;
    private int page = 1;

    public UserService(DatabaseContext context)
    {
        this._context = context;
    }

    public User Create(User user)
    {
        if (this._context.Users.Any(u => u.Email == user.Email))
        {
            throw new Exception("User already exists");
        }
        this._context.Users.Add(user);
        this._context.SaveChanges();
        return user;
    }

    public void Delete(int id)
    {
        var user = this._context.Users.Find(id);

        if (user == null) throw new KeyNotFoundException("User not found in database");

        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    public IEnumerable<User> GetAll( String? query, int page = 1, int take = 10)
    { 

        page = page < 1 ? this.page : page;
        take = take < 1 ? this.take : take;
        //String q = String.Empty()

        if (query == null)
        {
            query = "";
        }
        
        return this._context
                   .Users
                   .Where(u => u.FirtName.ToLower().StartsWith(query.ToLower()) || u.LastName.ToLower().StartsWith(query.ToLower())) // .Where(u => u.FirtName.StartsWith(query)|| u.LastName.StartsWith(query))
                   .Skip((page -1) * take)
                   .Take(take)
                   .ToList();
    }

    public User GetById(int id)
    {
        var user = this._context.Users.Find(id);

        if (user == null) throw new KeyNotFoundException("User not found in database");

        return user;
    }

    public User Update(int id, User user)
    {
        var userFound = this.getOneById(id);

        if (userFound != null) throw new KeyNotFoundException("User not found in database");

        userFound.FirtName = user.FirtName;
        userFound.LastName = user.LastName;
        userFound.Email = user.Email;
        userFound.Address = user.Address;

        _context.Users.Update(userFound);
        _context.SaveChanges();

        return userFound;
    }

    public User? getOneById(int id)
    {
        var user = this._context.Users.Find(id);
        return user;
    }

    public int Count(String? queury)
    {
        var count = 0;
        if (queury != null)
        {
            count = this._context
                        .Users
                        .Where(u => u.FirtName.ToLower().StartsWith(queury.ToLower()) || u.LastName.ToLower().StartsWith(queury.ToLower()))
                        .Count(u => true);
        } else
        {
            count = this._context.Users.Count(u => true);
        }

        return count;
    }
}