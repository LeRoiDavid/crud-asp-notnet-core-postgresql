namespace ApiUser.Entities.Dto;

public class PaginationParams
{
    private const int _maxItemsPerPage = 50;
    private int itemsPerPage = 10;

    public int Page { get; set; } = 1;
    public int ItemsPerPage 
    { 
        get => itemsPerPage; 
        set => itemsPerPage = value > _maxItemsPerPage ? _maxItemsPerPage : value; 
    }
}