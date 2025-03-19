namespace UsersApp.Application.Queries;

public abstract class Pagination
{
    private const int _maxPageSize = 50;
    private const int _minPageSize = 1;
    private const int _minPageNumber = 1;

    private int _pageNumber = 1;
    public int PageNumber
    {
        get
        {
            return _pageNumber;
        }
        set
        {
            _pageNumber = (value is < _minPageNumber) ? _minPageNumber : value;
        }
    }

    private int _pageSize = 10;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value is > _maxPageSize or < _minPageSize) ? _maxPageSize : value;
        }
    }
}