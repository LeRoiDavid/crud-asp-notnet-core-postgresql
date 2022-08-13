namespace ApiUser.Entities;

using ApiUser.Entities.Enums;

public class Todo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TodoStatus Status { get; set; }

    public DateTime Created { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User User { get; set; }

    public int UserId { get; set; }
}