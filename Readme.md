# 博客开发记录

------
[地址 https://www.sqber.com/](https://www.sqber.com/)(未开通)

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

（以上 2017-9-4）

添加数据库访问层，因只是一个简单的日志系统，只是采用文件夹分层的形式，而没有使用项目分层。

数据库采用SQLite，主要还是考虑到服务器的问题。一切轻量为主。避免了安装数据库软件。

SQLite的使用：

>* 拷贝Lib目录下的文件到项目，并引用System.Data.SQLite.dll，将sqlite3.def和sqllite3.dll拷贝到生成的bin目录下。
>* 拷贝SqliteHelper文件
>* 拷贝Repository文件，并修改即可。

Multiple types were found that match the controller named 'Home'

添加命名空间，示例：namespaces:  new[] {"MyNamespace.Controllers"}，命名空间从各自的文件中去Copy命名空间。

完成一个Repository。

完成博客的新增界面。编辑器使用的Ueditor。(编辑器的使用在onenote笔记中搜索即可)

（以上 2017-9-5）

使用Autofac实现依赖注入

>* 安装autofac.mvc的NuGet包
>* 新增AutofacHelper类，新项目中直接拷贝过去即可。此项目放在了Common文件夹中
>* 在Global.asax文件夹中调用AutofacHelper中的Inject方法即可。

实现新增博客功能。

枚举的处理：

枚举并没有放到一个单独的地方，而是放在了与之相关的Model里面。感觉关联性更好些，也更容易理解。


（以上 2017-9-6）

完成文章的查看，代码块的美化

（以上 2017-9-7）

前台和后台列表展示，删除功能。

（以上 2017-9-10）

完成编辑和部分页面的样式调整

（以上 2017-9-13）

添加发布功能&按时间倒序&调整布局样式

（以上 2017-9-19）

后台身份验证

（以上 2017-9-20）

后台登录UI调整

（以上 2017-9-20）

数据库备份功能
记录日志（记录到数据库中，有助于查看，日志使用异步形式记录）,日志列表，级别控制，记录总条数限制避免数据量过大。
新增游客用户，并添加相应权限控制。（权限这里，没有权限挑战到无权限页面，而不要弹框提示，跳转类的无法弹框，Ajax的加全局判断，消息提示不要使用alert，否则会阻止跳转，必须点击）

（以上 2017-9-21）

添加标签
（以上 2017-9-26）

AntiXss防止XSS攻击,禁止cookie通过js读取。
详情页URL重写


todo:
后台适配手机（登录+列表+日志+新增文章）

文章详情显示标签
标签分组页展示
详情页添加版本信息
底部添加版本信息
评论，点赞，分享
定义针对搜索引擎的关键词
定义对页面的描述



待整理：
认证，日志
