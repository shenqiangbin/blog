using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Blog.Repository
{
    public class CommentRepository
    {
        public int Add(Comment model)
        {
            model.Content = new Ganss.XSS.HtmlSanitizer().Sanitize(model.Content);

            string cmdText = @"INSERT INTO Comment (CommentId, ArticleId, UserName, Content,ParentId, CreateTime, UpdateTime, Enable)
                VALUES (?,?,?,?,?,?,?,?);select last_insert_rowid() newid;";
            object[] paramList = {
                            null,  //对应的主键不要赋值了
                            model.ArticleId,
                            model.UserName,
                            model.Content,
                            model.ParentId,
                            model.CreateTime,
                            model.UpdateTime,
                            model.Enable
                    };
            object result = SQLiteHelper.ExecuteScalar(cmdText, paramList);

            int intResult;
            if (int.TryParse(result.ToString(), out intResult))
                return intResult;
            return 0;
        }

        //        public int Update(Comment model)
        //        {
        //            string sql = @"
        //update Comment set 
        //title = ?,
        //content = ?,
        //ContentLevel = ?,
        //PublishStatus = ?,
        //DisplayCreatedTime = ?,
        //CreatedTime = ?,
        //UpdateTime = ?,
        //Enable = ?
        //    where CommentId = ?
        //";
        //            object[] paramList = {
        //                    model.Title,
        //                    model.Content,
        //                    model.ContentLevel,
        //                    model.PublishStatus,
        //                    model.DisplayCreatedTime,
        //                    model.CreatedTime,
        //                    model.UpdateTime,
        //                    model.Enable,
        //                    model.CommentId
        //            };

        //            int rowCount = SQLiteHelper.ExecuteNonQuery(sql, paramList);
        //            return rowCount;
        //        }

        public List<Comment> GetCommentByArticle(string articleId)
        {
            List<Comment> list = new List<Comment>();

            string cmdText = "select * from Comment where articleId = ? and enable = 1  order by createtime asc";
            DataSet dt = SQLiteHelper.ExecuteDataset(cmdText, articleId);
            foreach (DataRow item in dt.Tables[0].Rows)
            {
                list.Add(RowToModel(item));
            }

            return list;
        }

        private Comment RowToModel(DataRow row)
        {
            if (row == null)
                return null;

            Comment model = new Comment();
            model.CommentId = Convert.ToInt32(row["CommentId"]);
            model.ArticleId = Convert.ToInt32(row["ArticleId"]);
            model.UserName = Convert.ToString(row["UserName"]);
            model.Content = Convert.ToString(row["Content"]);
            model.ParentId = Convert.ToInt32(row["ParentId"]);
            model.CreateTime = Convert.ToDateTime(row["CreateTime"]);
            model.UpdateTime = Convert.ToDateTime(row["UpdateTime"]);
            model.Enable = Convert.ToInt32(row["Enable"]);

            if (row["UpdateTime"] != DBNull.Value)
                model.UpdateTime = Convert.ToDateTime(row["UpdateTime"]);

            model.Enable = Convert.ToInt32(row["Enable"]);

            model.Content = new Ganss.XSS.HtmlSanitizer().Sanitize(model.Content);

            return model;
        }

        public void Remove(int id)
        {
            string sql = "update Comment set enable = 0 where Commentid = ?";
            SQLiteHelper.ExecuteNonQuery(sql, id);
        }

        public Comment GetById(string CommentId)
        {
            string cmdText = "select * from Comment where Commentid = ?";
            DataRow row = SQLiteHelper.ExecuteDataRow(cmdText, CommentId);
            return RowToModel(row);
        }

        public List<Comment> GetByParentIds(List<int> ids)
        {
            List<Comment> list = new List<Comment>();

            string cmdText = $"select * from Comment where enable = 1 and parentid in ({string.Join(",", ids)})";
            DataSet ds = SQLiteHelper.ExecuteDataset(cmdText);
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                list.Add(RowToModel(item));
            }

            return list;
        }

        public CommentListModelResult GetPaged(CommentListQuery listModel)
        {
            CommentListModelResult result = new CommentListModelResult();
            result.List = new List<Comment>();

            StringBuilder builder = new StringBuilder("select * from Comment where Enable = 1 and parentId = 0");

            if (!string.IsNullOrEmpty(listModel.ArticleId))
                builder.Append(" and ArticleId = ?");

            if (string.IsNullOrEmpty(listModel.Order))
                builder.Append(" order by createtime desc,CommentId asc");

            object[] para = { listModel.ArticleId };

            DataTable dt = SQLiteHelper.ExecutePager(listModel.PageIndex, listModel.PageSize, builder.ToString(), para);
            foreach (DataRow item in dt.Rows)
            {
                result.List.Add(RowToModel(item));
            }

            StringBuilder countSql = new StringBuilder("select count(1) from Comment where Enable = 1 and parentId = 0");
            if (!string.IsNullOrEmpty(listModel.ArticleId))
                countSql.Append(" and ArticleId = ?");

            object countNum = SQLiteHelper.ExecuteScalar(countSql.ToString(), para);
            result.TotalCount = Convert.ToInt32(countNum);

            return result;
        }
    }
}