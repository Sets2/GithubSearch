namespace GithubSearch.Models
{
    public class Pagination
    {
        public int pageNumber { get; set; }          // Номер страницы (из параметров пагинации)
        public int pageSize { get; set; }            // Размер страницы (из параметров пагинации)
        public int totalPages { get; set; }          // Всего страниц при такой пагинации
        public int totalCount { get; set; }          // Всего репозиториев 
        public int totalGitResponse { get; set; }    // Всего записей в БД
    }
}
