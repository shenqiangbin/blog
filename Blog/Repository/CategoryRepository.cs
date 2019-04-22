using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Blog.Repository
{
    public class CategoryRepository
    {
        public int Add(Category model)
        {
            string cmdText = "insert into Category values(?,?,?,?,?,?,?,?);select last_insert_rowid() newid;";
            object[] paramList = {
                    null,  //对应的主键不要赋值了
                    model.Name,
                    model.ParentCategoryId,
                    model.Sort,
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

        public int Update(Category model)
        {
            string sql = @"
update Category set 
Name = ?,
ParentCategoryId = ?,
Sort = ?,
CreateUser = ?,
CreateTime = ?,
UpdateTime = ?,
Enable = ?
    where CategoryId = ?
";
            object[] paramList = {
                    model.Name,
                    model.ParentCategoryId,
                    model.Sort,
                    model.CreateUser,
                    model.CreateTime,
                    model.UpdateTime,
                    model.Enable,
                    model.CategoryId
            };

            int rowCount = SQLiteHelper.ExecuteNonQuery(sql, paramList);
            return rowCount;
        }

        public Category SelectByName(string name)
        {
            string cmdText = "select * from Category where name = ? and enable = 1";
            DataRow row = SQLiteHelper.ExecuteDataRow(cmdText, name);
            return RowToModel(row);
        }

        private Category RowToModel(DataRow row)
        {
            if (row == null)
                return null;

            Category model = new Category();
            model.CategoryId = Convert.ToInt32(row["CategoryId"]);
            model.Name = Convert.ToString(row["Name"]);
            model.ParentCategoryId = Convert.ToInt32(row["ParentCategoryId"]);
            model.Sort = Convert.ToInt32(row["Sort"]);
            model.CreateUser = Convert.ToString(row["CreateUser"]);
            model.CreateTime = Convert.ToDateTime(row["CreateTime"]);
            model.UpdateTime = Convert.ToDateTime(row["UpdateTime"]);
            model.Enable = Convert.ToInt32(row["Enable"]);

            return model;
        }

        public List<Category> GetAll()
        {
            List<Category> list = new List<Category>();

            string sql = "select * from Category where enable = 1";
            DataSet dt = SQLiteHelper.ExecuteDataset(sql);
            foreach (DataRow item in dt.Tables[0].Rows)
            {
                list.Add(RowToModel(item));
            }

            return list;
        }

    }
}