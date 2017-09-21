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

        public int Update(Article model)
        {
            string sql = @"
update Article set 
title = ?,
content = ?,
ContentLevel = ?,
PublishStatus = ?,
DisplayCreatedTime = ?,
CreatedTime = ?,
UpdateTime = ?,
Enable = ?
    where ArticleId = ?
";
            object[] paramList = {
                    model.Title,
                    model.Content,
                    model.ContentLevel,
                    model.PublishStatus,
                    model.DisplayCreatedTime,
                    model.CreatedTime,
                    model.UpdateTime,
                    model.Enable,
                    model.ArticleId
            };

            int rowCount = SQLiteHelper.ExecuteNonQuery(sql, paramList);
            return rowCount;
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
            model.CreatedTime = Convert.ToDateTime(row["CreatedTime"]);
            model.UpdateTime = Convert.ToDateTime(row["UpdateTime"]);
            model.Enable = Convert.ToInt32(row["Enable"]);

            return model;
        }

        public void Remove(int id)
        {
            string sql = "update article set enable = 0 where articleid = ?";
            SQLiteHelper.ExecuteNonQuery(sql, id);
        }

        public Article GetById(string articleId)
        {
            string cmdText = "select * from article where articleid = ?";
            DataRow row = SQLiteHelper.ExecuteDataRow(cmdText, articleId);
            return RowToModel(row);
        }

        public ArticleListModelResult GetPaged(ArticleListQuery listModel)
        {
            ArticleListModelResult result = new ArticleListModelResult();
            result.List = new List<Article>();

            string sql = "select * from article where Enable = 1";

            if (listModel.PublishStatus != PublishStatus.All)
                sql += " and PublishStatus = ?";

            if (string.IsNullOrEmpty(listModel.Order))
                sql += " order by createdtime desc,articleId asc";
            else
                sql += " " + listModel.Order;

            object[] para = { (int)listModel.PublishStatus };

            DataTable dt = SQLiteHelper.ExecutePager(listModel.PageIndex, listModel.PageSize, sql, para);
            foreach (DataRow item in dt.Rows)
            {
                result.List.Add(RowToModel(item));
            }

            string countSql = "select count(1) from article where Enable = 1";

            if (listModel.PublishStatus != PublishStatus.All)
                countSql += " and PublishStatus = ?";

            object countNum = SQLiteHelper.ExecuteScalar(countSql, para);
            result.TotalCount = Convert.ToInt32(countNum);

            return result;
        }
    }
}