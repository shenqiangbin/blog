using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Blog.Repository
{
    public class ArticleRepository
    {
        public int Add(Article model)
        {
            if (model.Editor == (int)ArticleEditor.UEditor)
                model.Content = Common.XSSHelper.Sanitize(model.Content);

            string cmdText = @"insert into article (ArticleId, Title, Content, htmlContent, ContentLevel, PublishStatus, KeyWords, UrlTitle, UrlTitleNum, Editor, DisplayCreatedTime, CreateUser, CreatedTime, UpdateTime, Enable) 
                values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?);select last_insert_rowid() newid;";
            object[] paramList = {
                    null,  //对应的主键不要赋值了
                    model.Title,
                    model.Content,
                    model.HtmlContent,
                    model.ContentLevel,
                    model.PublishStatus,
                    model.KeyWords,
                    model.UrlTitle,
                    model.UrlTitleNum,
                    model.Editor,
                    model.DisplayCreatedTime,
                    model.CreateUser,
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
            if (model.Editor == (int)ArticleEditor.UEditor)
                model.Content = Common.XSSHelper.Sanitize(model.Content);

            string sql = @"
update Article set 
title = ?,
content = ?,
htmlContent = ?,
ContentLevel = ?,
PublishStatus = ?,
DisplayCreatedTime = ?,
KeyWords = ?,
UrlTitle = ?,
UrlTitleNum = ?,
Editor = ?,
CreatedTime = ?,
UpdateTime = ?,
Enable = ?
    where ArticleId = ?
";
            object[] paramList = {
                    model.Title,
                    model.Content,
                    model.HtmlContent,
                    model.ContentLevel,
                    model.PublishStatus,
                    model.DisplayCreatedTime,
                    model.KeyWords,
                    model.UrlTitle,
                    model.UrlTitleNum,
                    model.Editor,
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
            model.HtmlContent = Convert.ToString(row["HtmlContent"]);
            model.ContentLevel = Convert.ToInt32(row["ContentLevel"]);
            model.PublishStatus = Convert.ToInt32(row["PublishStatus"]);
            model.DisplayCreatedTime = Convert.ToDateTime(row["DisplayCreatedTime"]);
            model.KeyWords = Convert.ToString(row["KeyWords"]);
            model.UrlTitle = Convert.ToString(row["UrlTitle"]);
            model.UrlTitleNum = Convert.ToString(row["UrlTitleNum"]);
            model.Editor = Convert.ToInt32(row["Editor"]);
            model.CreateUser = Convert.ToString(row["CreateUser"]);
            model.CreatedTime = Convert.ToDateTime(row["CreatedTime"]);
            model.UpdateTime = Convert.ToDateTime(row["UpdateTime"]);
            model.Enable = Convert.ToInt32(row["Enable"]);

            if (model.Editor == (int)ArticleEditor.UEditor)
                model.Content = Common.XSSHelper.Sanitize(model.Content);

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

        public List<Article> GetByColumn(string colName, string val)
        {
            List<Article> list = new List<Article>();

            string cmdText = $"select * from article where {colName} = ? and enable = 1";
            DataSet dt = SQLiteHelper.ExecuteDataset(cmdText, val);

            foreach (DataRow item in dt.Tables[0].Rows)
            {
                list.Add(RowToModel(item));
            }

            return list;
        }

        public ArticleListModelResult GetPaged(ArticleListQuery listModel)
        {
            ArticleListModelResult result = new ArticleListModelResult();
            result.List = new List<Article>();

            StringBuilder builder = new StringBuilder("select * from article where Enable = 1");
            List<object> paraList = new List<object>();

            if (listModel.PublishStatus != PublishStatus.All)
            {
                builder.Append(" and PublishStatus = ?");
                paraList.Add((int)listModel.PublishStatus);
            }

            if (!string.IsNullOrEmpty(listModel.TheUserData))
            {
                builder.Append(" and createUser = ? ");
                paraList.Add(listModel.TheUserData);
            }

            if (!string.IsNullOrEmpty(listModel.Search))
            {
                builder.Append(" and (title like ? or content like ? or HtmlContent like ?)");
                paraList.Add("%" + listModel.Search + "%");
                paraList.Add("%" + listModel.Search + "%");
                paraList.Add("%" + listModel.Search + "%");
            }

            if (string.IsNullOrEmpty(listModel.Order))
                builder.Append(" order by createdtime desc,articleId asc");
            else
                builder.Append(listModel.Order);

            DataTable dt = SQLiteHelper.ExecutePager(listModel.PageIndex, listModel.PageSize, builder.ToString(), paraList.ToArray());
            foreach (DataRow item in dt.Rows)
            {
                result.List.Add(RowToModel(item));
            }

            StringBuilder countSqlBuilder = new StringBuilder("select count(1) from article where Enable = 1");

            if (listModel.PublishStatus != PublishStatus.All)
                countSqlBuilder.Append(" and PublishStatus = ?");
            if (!string.IsNullOrEmpty(listModel.TheUserData))
                countSqlBuilder.Append(" and createUser = ? ");

            if (!string.IsNullOrEmpty(listModel.Search))
            {
                countSqlBuilder.Append(" and (title like ? or content like ? or HtmlContent like ?)");
            }

            object countNum = SQLiteHelper.ExecuteScalar(countSqlBuilder.ToString(), paraList.ToArray());
            result.TotalCount = Convert.ToInt32(countNum);

            return result;
        }
    }
}