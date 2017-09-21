using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Service
{
    public class PermissionService
    {
        public PermissionService()
        {

        }

        private List<Permission> GetPermissionList()
        {
            List<Permission> list = new List<Permission>();

            list.Add(new Permission { Id = 1, Code = "blogList", Desc = "博客列表" });
            list.Add(new Permission { Id = 2, Code = "blogEdit", Desc = "博客新增/编辑" });
            list.Add(new Permission { Id = 3, Code = "dbDownload", Desc = "数据库下载" });
            list.Add(new Permission { Id = 4, Code = "bloglist", Desc = "日志查看" });

            return list;
        }

        private List<UserPermission> GetUserPermissions()
        {
            List<UserPermission> list = new List<UserPermission>();

            string email = "guest@163.com";

            list.Add(new UserPermission { Email = email, PermissionCode = Permission.BlogList, OnlyAccessSelf = false });
            list.Add(new UserPermission { Email = email, PermissionCode = Permission.BlogEdit, OnlyAccessSelf = true });
            list.Add(new UserPermission { Email = email, PermissionCode = Permission.DbDownload, OnlyAccessSelf = true });

            return list;
        }

        public bool CanAccess(string email, string permissioncode)
        {
            if (email == "shenqiangbin@163.com")
                return true;

            var userPermissions = GetUserPermissions();
            var list = userPermissions.Where(m => m.Email == email && m.PermissionCode == permissioncode);
            if (list != null && list.Any())
                return true;
            else
                return false;
        }

        public bool OnlyAccessSelf(string email, string permissioncode)
        {
            if (email == "shenqiangbin@163.com")
                return false;

            var userPermissions = GetUserPermissions();
            var list = userPermissions.Where(m => m.Email == email && m.PermissionCode == permissioncode);
            if (list != null && list.Any())
                return list.First().OnlyAccessSelf;
            else
                return false;
        }
    }

    public class Permission
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Desc { get; set; }

        public static string BlogList = "blogList";
        public static string BlogEdit = "blogEdit";
        public static string DbDownload = "dbDownload";
    }

    public class UserPermission
    {
        public string Email { get; set; }
        public string PermissionCode { get; set; }
        /// <summary>
        /// 只能查看自己的数据
        /// </summary>
        public bool OnlyAccessSelf { get; set; }
    }
}