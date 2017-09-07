using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Blog.Repository
{
    public class ArticleRepository
    {        
        public int Add(Article model)
        {
            string cmdText = "insert into article values(?,?,?,?,?,?,?,?,?);select last_insert_rowid() newid;";
            object[] paramList = {
                    null,  //对应的主键不要赋值了
                    model.Title,
                    model.Content,
                    model.ContentLevel,
                    model.PublishStatus,
                    model.DisplayCreatedTime,
                    model.CreatedTime,
                    model.UpdateTime,
                    model.Enable
            };
            object result = SQLiteHelper.ExecuteScalar(cmdText, paramList);

            int intResult;
            if (int.TryParse(result.ToString(), out intResult))
                return intResult;
            return 0;
        }

        public Article GetById(string articleId)
        {
            string cmdText = "select * from article where articleid = ?";
            DataRow row = SQLiteHelper.ExecuteDataRow(cmdText, articleId);
            return RowToModel(row);
        }

        private Article RowToModel(DataRow row)
        {
            if (row == null)
                return null;

            Article model = new Article();
            model.ArticleId = Convert.ToInt32(row["ArticleId"]);
            model.Title = Convert.ToString(row["Title"]);
            model.Content = Convert.ToString(row["Content"]);

            return model;
        }
    }
}