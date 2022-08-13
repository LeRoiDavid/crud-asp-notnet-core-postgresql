namespace ApiUser.Entities.Dto;

public class PaginationMetadata<T>
{
    public PaginationMetadata(int totalCount, int currentPage, int itemsPerPage, List<T> _items)
    {
        TotalCount = totalCount;
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling(totalCount / (double)itemsPerPage);
        Items = _items;
    }

    public int CurrentPage { get; private set; }
    public int TotalCount { get; private set; }
    public int TotalPages { get; private set; }
    
    public bool HasPrevious => CurrentPage > 1;
    
    public bool HasNext => CurrentPage < TotalPages;
    public List<T> Items { get; private set; }
}