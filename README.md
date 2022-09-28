# Unity模型点击

#### 介绍
本模型点击工具，采用的射线监测方式，
1、鼠标抬起时判断是否点击到了模型对象
2、会判断当前是否鼠标落下和抬起是否为同一个模型
3、点击的时间间隔少于220毫秒
4、鼠标落下和抬起或者手指落下和抬起的距离不大于10像素


#### 安装教程

内带demo
1.  下载工具
2.  单例类，直接调用接口即可

#### 使用说明
1.  ModelClick（广播事件）：可监听到射线监测到的第一个model对象，并返回对象
2.  RayMaxDistance（属性）：设置当前射线监测的最长有效距离


#### 参与贡献

1.  Fork 本仓库
2.  新建 Feat_xxx 分支
3.  提交代码
4.  新建 Pull Request


#### 特技

1.  使用 Readme\_XXX.md 来支持不同的语言，例如 Readme\_en.md, Readme\_zh.md
2.  Gitee 官方博客 [blog.gitee.com](https://blog.gitee.com)
3.  你可以 [https://gitee.com/explore](https://gitee.com/explore) 这个地址来了解 Gitee 上的优秀开源项目
4.  [GVP](https://gitee.com/gvp) 全称是 Gitee 最有价值开源项目，是综合评定出的优秀开源项目
5.  Gitee 官方提供的使用手册 [https://gitee.com/help](https://gitee.com/help)
6.  Gitee 封面人物是一档用来展示 Gitee 会员风采的栏目 [https://gitee.com/gitee-stars/](https://gitee.com/gitee-stars/)
