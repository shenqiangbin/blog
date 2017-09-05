using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Repository
{
    public class ArticleRepository
    {        
        public int Add(Article model)
        {
            string cmdText = "insert into article values(?,?,?,?,?,?,?);select last_insert_rowid() newid;";
            object[] paramList = {
                    null,  //对应的主键不要赋值了
                    model.Title,
                    model.Content,
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
    }
}