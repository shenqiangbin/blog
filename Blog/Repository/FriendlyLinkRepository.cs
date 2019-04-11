using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Blog.Repository
{
    public class FriendlyLinkRepository
    {
        public int Add(FriendlyLink model)
        {
            string cmdText = "insert into FriendlyLink values(?,?,?,?,?,?,?,?,?,?);select last_insert_rowid() newid;";
            object[] paramList = {
                    null,
                    model.sitename,
                    model.siteurl,
                    model.sitedesc,
                    model.Sort,
                    model.IsCheck,
                    model.Reason,
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

        public void RemoveByArticleId(int articleId)
        {
            //string sql = "update FriendlyLink set enable = 0 where articleId = ?";
            string sql = "delete from FriendlyLink where articleId = ?";
            SQLiteHelper.ExecuteNonQuery(sql, articleId);
        }

        //        public int Update(FriendlyLink model)
        //        {
        //            string sql = @"
        //update FriendlyLink set 
        //Name = ?,
        //CreateUser = ?,
        //CreateTime = ?,
        //UpdateTime = ?,
        //Enable = ?
        //    where FriendlyLinkId = ?
        //";
        //            object[] paramList = {
        //                    model.Name,
        //                    model.CreateUser,
        //                    model.CreateTime,
        //                    model.UpdateTime,
        //                    model.Enable,
        //                    model.FriendlyLinkId
        //            };

        //            int rowCount = SQLiteHelper.ExecuteNonQuery(sql, paramList);
        //            return rowCount;
        //        }

        //public FriendlyLink SelectByName(string name)
        //{
        //    string cmdText = "select * from FriendlyLink where name = ? and enable = 1";
        //    DataRow row = SQLiteHelper.ExecuteDataRow(cmdText, name);
        //    return RowToModel(row);
        //}

        private FriendlyLink RowToModel(DataRow row)
        {
            if (row == null)
                return null;

            FriendlyLink model = new FriendlyLink();
            model.FriendlyLinkId = Convert.ToInt32(row["FriendlyLinkId"]);
            model.sitename = Convert.ToString(row["sitename"]);
            model.siteurl = Convert.ToString(row["siteurl"]);
            model.sitedesc = Convert.ToString(row["sitedesc"]);
            model.IsCheck = row["IsCheck"] != DBNull.Value ? Convert.ToInt32(row["IsCheck"]) : 0;
            model.Sort = row["Sort"] != DBNull.Value ? Convert.ToInt32(row["Sort"]) : 0;
            model.Reason = Convert.ToString(row["Reason"]);
            model.Enable = Convert.ToInt32(row["Enable"]);

            return model;
        }

        public List<FriendlyLink> GetAll()
        {
            List<FriendlyLink> list = new List<FriendlyLink>();

            string sql = "select * from FriendlyLink where enable = 1 and IsCheck = " + (int)FriendlyLinkIsCheck.OK;
            DataSet dt = SQLiteHelper.ExecuteDataset(sql);
            foreach (DataRow item in dt.Tables[0].Rows)
            {
                list.Add(RowToModel(item));
            }

            return list;
        }
    }
}