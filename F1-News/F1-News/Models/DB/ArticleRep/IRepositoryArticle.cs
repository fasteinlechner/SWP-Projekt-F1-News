using System.Collections.Generic;
using System.Threading.Tasks;

namespace F1_News.Models.DB.ArticleRep {
    public interface IRepositoryArticle {

        Task ConnectAsync();
        Task DisconnectAsync();
        Task<Article> GetArticleByIDAsync(int id);
        Task<bool> DeleteArticleAsync(int id);
        Task<List<Article>> GetAllArticlesAsync();
        Task<bool> InsertAsync(Article article);
        Task<bool> UpdateAsync(Article newArticle);
    }
}