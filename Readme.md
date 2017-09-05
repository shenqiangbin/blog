# 博客开发记录

------
[地址](https://www.zybuluo.com/mdeditor)

本文旨在记录开发博客的整个过程。

功能
>* 写博客

涉及点：
>* IDE：VS2015企业版（语言：英文）
>* Framework：4.5
>* 技术：MVC WebApi 

新建一个项目名称为Blog，模板选择Empty，勾选MVC和WebApi。

先做了一个静态首页，单独部署，单独于博客之外的项目。

博客项目，即此项目自己生成了很多东西，自动引入了jquery，bootstrap。先全部将文件删除掉了。考虑到自己买的服务器不会太好。

样式决定手写。写好首页的导航。

css样式的引用使用Bundle形式，实现动态压缩css和js文件。

引入Microsoft.AspNet.Web.Optimization。添加BundleConfig.cs文件并在Global.asax中调用。在Views下的web.config文件中添加命名空间的引用。
修改web.config中的debug模式来实现css和js的压缩。

编写首页列表，主要是样式内容。
2017-9-4

添加数据库访问层，因只是一个简单的日志系统，只是采用文件夹分层的形式，而没有使用项目分层。

数据库采用SQLite，主要还是考虑到服务器的问题。一切轻量为主。避免了安装数据库软件。

SQLite的使用：

>* 拷贝Lib目录下的文件到项目，并引用System.Data.SQLite.dll，将sqlite3.def和sqllite3.dll拷贝到生成的bin目录下。
>* 拷贝SqliteHelper文件
>* 拷贝Repository文件，并修改即可。

Multiple types were found that match the controller named 'Home'

添加命名空间，示例：namespaces:  new[] {"MyNamespace.Controllers"}，命名空间从各自的文件中去Copy命名空间。