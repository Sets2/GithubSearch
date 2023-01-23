namespace GithubSearch.Models
{
    public class Pagination
    {
        public int pageNumber;          // Номер страницы (из параметров пагинации)
        public int pageSize;            // Размер страницы (из параметров пагинации)
        public int totalPages;          // Всего страниц при такой пагинации
        public int totalCount;          // Всего репозиториев 
        public int totalGitResponse;    // Всего записей в БД
    }
}
