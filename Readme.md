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