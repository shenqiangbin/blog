using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Blog.Repository
{
    public class ArticleCategoryRepository
    {
        public int Add(ArticleCategory model)
        {
            string cmdText = "insert into ArticleCategory values(?,?,?,?,?,?);select last_insert_rowid() newid;";
            object[] paramList = {
                    model.CategoryId,
                    model.ArticleId,
                    model.CreateUser,
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

        public void RemoveByArticleId(int articleId)
        {
            //string sql = "update ArticleCategory set enable = 0 where articleId = ?";
            string sql = "delete from ArticleCategory where articleId = ?";
            SQLiteHelper.ExecuteNonQuery(sql, articleId);
        }

        //        public int Update(ArticleCategory model)
        //        {
        //            string sql = @"
        //update ArticleCategory set 
        //Name = ?,
        //CreateUser = ?,
        //CreateTime = ?,
        //UpdateTime = ?,
        //Enable = ?
        //    where ArticleCategoryId = ?
        //";
        //            object[] paramList = {
        //                    model.Name,
        //                    model.CreateUser,
        //                    model.CreateTime,
        //                    model.UpdateTime,
        //                    model.Enable,
        //                    model.ArticleCategoryId
        //            };

        //            int rowCount = SQLiteHelper.ExecuteNonQuery(sql, paramList);
        //            return rowCount;
        //        }

        //public ArticleCategory SelectByName(string name)
        //{
        //    string cmdText = "select * from ArticleCategory where name = ? and enable = 1";
        //    DataRow row = SQLiteHelper.ExecuteDataRow(cmdText, name);
        //    return RowToModel(row);
        //}

        //private ArticleCategory RowToModel(DataRow row)
        //{
        //    if (row == null)
        //        return null;

        //    ArticleCategory model = new ArticleCategory();
        //    model.ArticleCategoryId = Convert.ToInt32(row["ArticleCategoryId"]);
        //    model.Name = Convert.ToString(row["Name"]);
        //    model.CreateUser = Convert.ToString(row["CreateUser"]);
        //    model.CreateTime = Convert.ToDateTime(row["CreateTime"]);
        //    model.UpdateTime = Convert.ToDateTime(row["UpdateTime"]);
        //    model.Enable = Convert.ToInt32(row["Enable"]);

        //    return model;
        //}

        //public List<ArticleCategory> GetAll()
        //{
        //    List<ArticleCategory> list = new List<ArticleCategory>();

        //    string sql = "select * from ArticleCategory where enable = 1";
        //    DataSet dt = SQLiteHelper.ExecuteDataset(sql);
        //    foreach (DataRow item in dt.Tables[0].Rows)
        //    {
        //        list.Add(RowToModel(item));
        //    }

        //    return list;
        //}
    }
}