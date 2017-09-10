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

        private Article RowToModel(DataRow row)
        {
            if (row == null)
                return null;

            Article model = new Article();
            model.ArticleId = Convert.ToInt32(row["ArticleId"]);
            model.Title = Convert.ToString(row["Title"]);
            model.Content = Convert.ToString(row["Content"]);
            model.ContentLevel = Convert.ToInt32(row["ContentLevel"]);
            model.PublishStatus = Convert.ToInt32(row["PublishStatus"]);
            model.DisplayCreatedTime = Convert.ToDateTime(row["DisplayCreatedTime"]);

            return model;
        }

        public Article GetById(string articleId)
        {
            string cmdText = "select * from article where articleid = ?";
            DataRow row = SQLiteHelper.ExecuteDataRow(cmdText, articleId);
            return RowToModel(row);
        }

        public ArticleListModelResult GetPaged(ArticleListModel listModel)
        {
            ArticleListModelResult result = new ArticleListModelResult();
            result.List = new List<Article>();

            string sql = "select * from article where Enable = 1";
            sql += " and PublishStatus = ?";
            object[] para = { (int)listModel.PublishStatus };
            DataTable dt = SQLiteHelper.ExecutePager(listModel.PageIndex, listModel.PageSize, sql, para);
            foreach (DataRow item in dt.Rows)
            {
                result.List.Add(RowToModel(item));
            }

            string countSql = "select count(1) from article where Enable = 1";
            countSql += " and PublishStatus = ?";
            object countNum = SQLiteHelper.ExecuteScalar(countSql, para);
            result.TotalCount = Convert.ToInt32(countNum);

            return result;
        }
    }
}