namespace FeedOptimizationApp.Shared.Wrapper
{
    public class PaginatedResult<T> : Result
    {
        public PaginatedResult(List<T> data)
        {
            Data = data;
        }

        public List<T> Data { get; set; }

        internal PaginatedResult(bool succeeded, List<T> data = default, List<string> messages = null, int count = 0, int page = 1, int pageSize = 10)
        {
            Data = data;
            CurrentPage = page;
            Succeeded = succeeded;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

        private static PaginatedResult<T> PaginationFailure(List<string> messages)
        {
            return new PaginatedResult<T>(false, default, messages);
        }

        private static PaginatedResult<T> PaginationSuccess(List<T> data, int count, int page, int pageSize)
        {
            return new PaginatedResult<T>(true, data, null, count, page, pageSize);
        }

        public static Task<PaginatedResult<T>> FailureAsync(List<string> messages)
        {
            return Task.FromResult(PaginationFailure(messages));
        }

        public static Task<PaginatedResult<T>> SuccessAsync(List<T> data, int count, int page, int pageSize)
        {
            return Task.FromResult(PaginationSuccess(data, count, page, pageSize));
        }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }
        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;
    }
}