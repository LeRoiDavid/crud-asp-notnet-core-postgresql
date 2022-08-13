using System.Diagnostics;
using ApiUser.Entities.Dto;

namespace ApiUser.Controllers;

using Microsoft.AspNetCore.Mvc;
using ApiUser.Services;
using ApiUser.Entities;


/// <summary>
/// This is the user controller 
/// </summary>
[ApiController]
[Route("[controller]")]

public class UsersController : ControllerBase
{
    private IUserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        this._userService = userService;
        this._logger = logger;
    }


    [HttpGet("paginate")]
    public IActionResult GetAll( 
                [FromQuery] PaginationParams @params,
                [FromQuery] String? query
            )
    {
        
        var count = _userService.Count(query);
        
        var users = _userService.GetAll(query, @params.Page, @params.ItemsPerPage).ToList();
        
        var paginationMetadata = new PaginationMetadata<User>(count, @params.Page, @params.ItemsPerPage, users);
        // var users = _userService.GetAll(page, take);
        return Ok(paginationMetadata);
    }
    
    [HttpGet]
    public IActionResult GetAllPaginate(
        [FromQuery] int page,
        [FromQuery] int take
    )
    {
        var users = _userService.GetAll("", page, take);
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult getOne(int id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Create(User user)
    {
        var userCreated = _userService.Create(user);
        return Ok(userCreated);
    }

    /// <summary>
    /// Method permit to create a user
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public IActionResult Create(int id)
    {
        _userService.Delete(id);
        return Ok(new { message = "User deleted", id = id });
    }
    /// <summary>
    /// Update a user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, User model)
    {
        _userService.Update(id, model);
        return Ok(new { message = "User updated" });
    }


}