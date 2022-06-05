using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Models.DB.ArticleRep {
    public class RepositoryArticle : IRepositoryArticle {


        private String connection = DB_Connect.connectionStr;
        private DbConnection conn;
        public async Task ConnectAsync() {
            if(this.conn == null) {
                this.conn = new MySqlConnection(this.connection);
            }
            if(this.conn.State != ConnectionState.Open) {
                await conn.OpenAsync();
            }
        }
       
        public async Task DisconnectAsync() {
            if(this.conn!=null && this.conn.State == ConnectionState.Open) {
                await conn.CloseAsync();
            }
        }

        public async Task<bool> DeleteArticleAsync(int id) {
            if(this.conn?.State == ConnectionState.Open) {
                DbCommand com = this.conn.CreateCommand(); 
                com.CommandText= "delete from article where idarticle = @articleId";

                DbParameter paramId = com.CreateParameter();
                paramId.ParameterName = "articleId";
                paramId.DbType = DbType.Int32;
                paramId.Value = id;

                com.Parameters.Add(paramId);
                return await com.ExecuteNonQueryAsync()==1;
            }
            return false;
        }

        public async Task<List<Article>> GetAllArticlesAsync() {
            List<Article> articles = new List<Article>();
            if(this.conn?.State == ConnectionState.Open) {
                DbCommand com = this.conn.CreateCommand();
                com.CommandText = "select * from article";

                using (DbDataReader reader = await com.ExecuteReaderAsync()) {
                    while (await reader.ReadAsync()) {
                        articles.Add(new Article() {
                            ArticleID = Convert.ToInt32(reader["idarticle"]),
                            Bezeichnung = Convert.ToString(reader["bezeichnung"]),
                            Beschreibung = Convert.ToString(reader["beschreibung"]),
                            Preis = Convert.ToDecimal(reader["preis"]),
                            Elemente = Convert.ToInt32(reader["elemente"]),
                            ImageLink = Convert.ToString(reader["image"])
                        });
                    }
                }
            }
            return articles;
        }

        public async Task<Article> GetArticleByIDAsync(int id) {
            Article article = new Article();
            if (this.conn?.State == ConnectionState.Open) {
                DbCommand com = this.conn.CreateCommand();
                com.CommandText = "select * from article where idarticle = @articleId";

                DbParameter paramId = com.CreateParameter();
                paramId.ParameterName = "articleId";
                paramId.DbType = DbType.Int32;
                paramId.Value = id;

                com.Parameters.Add(paramId);
                
                using (DbDataReader reader = await com.ExecuteReaderAsync()) {
                    if (reader.Read()) {
                        article.ArticleID = Convert.ToInt32(reader["idarticle"]);
                        article.Bezeichnung = Convert.ToString(reader["bezeichnung"]);
                        article.Beschreibung = Convert.ToString(reader["beschreibung"]);
                        article.Preis = Convert.ToDecimal(reader["preis"]);
                        article.Elemente = Convert.ToInt32(reader["elemente"]);
                        article.ImageLink = Convert.ToString(reader["image"]);

                    }
                } 
            }
            return article;
        }

        public bool Insert(Article article)
        {
            //TODO
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAnzAsync(int anz, int id) {
            if(this.conn?.State == ConnectionState.Open) {
                DbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "update article set elemente=@elem where idarticle=@id";

                DbParameter parElem = cmd.CreateParameter();
                parElem.ParameterName = "elem";
                parElem.DbType = DbType.Int32;
                parElem.Value = anz;

                DbParameter parId = cmd.CreateParameter();
                parId.ParameterName = "id";
                parId.DbType = DbType.Int32;
                parId.Value = id;

                cmd.Parameters.Add(parElem);
                cmd.Parameters.Add(parId);

                return await cmd.ExecuteNonQueryAsync() == 1;
            }
            return false;
        }

        public Task<bool> InsertAsync(Article article)
        {
            throw new NotImplementedException();
        }
    }
}
