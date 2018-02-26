# 说明


## 整理基础库的目的：

做系统画像，对基础库的结构和依赖关系有一个相对完整的描述说明。整理后的文档将作为日后开发团队了解平台、重构平台的权威参考。
继续推进模块化工作，让模块划分更合理，增加内聚，降低耦合
找出基础库中历史遗留的僵尸代码，删除或重构
对选定的需要重构的基础库，采取持续集成实践，为云平台全面持续集成做准备

## 实施方案

1 整理现有基础库列表，包括模块的功能和简要设计说明
2 对现有基础库的依赖关系进行梳理，同时对不合理的部分，进行记录
3 提出重构建议，并进行评审
4 制定迭代计划，结合持续集成实践进行重构

## 一些想法

现有模块项目的创建缺乏参考，随意性大，没有评审过程，也鲜有说明文档，后期学习和维护非常不便，建议增加必要的约定和规范。
原有系统的设计思路，对“模块化”的理解不够，很多项目没有从业务的角度进行划分。顶层的问题导致代码很难内聚，这不利于系统的演化。
建议把模块项目按某些内聚的职责，比如业务或功能进行简单分类，方便理解和维护，例如：

基础服务模块（通用的底层服务模块，可以按大的独立功能服务划分项目类库）
业务功能模块（具有明显的业务特征，这些应用模块一般最好识别，例如Area.Xxx）
界面组件模块（界面可重用性强，有一致性要求，可能有全局定制风格的需求，需要由产品设计层统一考虑，包括风格的统一和主题的多样性，也可以避免一次次重复造轮子）


## 现有基础库列表（V3.3）
-------------

Accounts 账号 
Activity 大赛
AnalyticalEngine 分析引擎
Catalogs 资源目录
ClassEntity 班级
Comment 多级回复评论
CommonPlayerModule 播放器组件
Dics 字典
Ds 组织站点
DynamicModuleWidget 动态组件

Evals 量表打分
Examination 调查问卷
FMSTopologyStructure FMS拓扑结构
Friends 好友
Gateway 网关服务
Groups 圈子
HistoryRecords 历史记录
Labels 标签
Loggers 日志存储
Messages 消息系统

Misc 杂项
Mobile 移动端
My “我”的数据中心
News 公告
Packages 模块的安装卸载
Rbac 基于角色的权限控制
RecordScheduling 服务器录像调度服务
Resources 资源
Settings 通用设置
SolrNet 全站检索

Sso 单点登录
StreamingMedia 流媒体
Timetables 课表
Users 用户信息
VideoClip 视频切片
VideoMeetings 第三方视频会议


## 现有基础库梳理（V3.3）

Accounts 账号
Activity 大赛 （是否已经废弃，是否独立的业务模块？）
AnalyticalEngine 分析引擎
Catalogs 资源目录
ClassEntity 班级
Comment 多级回复评论
CommonPlayerModule 播放器组件（需要梳理，是否已经废弃？）
Dics 字典
Ds 组织站点
DynamicModuleWidget 动态组件（需要梳理，UI组件，是否要重新考虑和整体设计）


Evals 量表打分（需要重新确认，是否已经废弃，应作为独立的业务模块）
Examination 调查问卷（是否可以作为独立的业务模块）
FMSTopologyStructure FMS拓扑结构
Friends 好友
Gateway 网关服务
Groups 圈子
HistoryRecords 历史记录
Labels 标签
Loggers 日志存储
Messages 消息系统

Misc 杂项
Mobile 移动端
My “我”的数据中心
News 公告
Packages 模块的安装卸载
Rbac 基于角色的权限控制
RecordScheduling 服务器录像调度服务
Resources 资源
Settings 通用设置
SolrNet 全站检索

Sso 单点登录 （是否独立的业务模块？）
StreamingMedia 流媒体
Timetables 课表
Users 用户信息
VideoClip 视频切片
VideoMeetings 第三方视频会议

像是量表打分，调查问卷这类模块，如何划分合理？有UI，有持久化，有业务


## 模块梳理（V3.4）

Accounts 账号
Activity 活动（建议重构，作为业务模块的一部分）
AnalyticalEngine 分析引擎
Classes 班级
Comments 评论
CommonPlayerModule

Catalogs 资源目录

Permissions 权限




| 模块代码                 	| 模块名称           	| 重构建议 	|
|--------------------------	|--------------------	|----------	|
| Accounts                 	| 账号               	|          	|
| Activity                 	| 大赛               	|          	|
| AnalyticalEngine         	| 分析引擎           	|          	|
| Catalogs                 	| 资源目录           	|          	|
| ClassEntity              	| 班级               	|          	|
| Comment                  	| 多级回复评论       	|          	|
| CommonPlayerModule       	| 播放器组件         	|          	|
| Dics                     	| 字典               	|          	|
| Ds                       	| 组织站点           	|          	|
| DynamicModuleWidget      	| 动态组件           	|          	|
| Evals                    	| 量表打分           	|          	|
| Examination              	| 调查问卷           	|          	|
| FMSTopologyStructure?FMS 	| 拓扑结构           	|          	|
| Friends                  	| 好友               	|          	|
| Gateway                  	| 网关服务           	|          	|
| Groups                   	| 圈子               	|          	|
| HistoryRecords           	| 历史记录           	|          	|
| Labels                   	| 标签               	|          	|
| Loggers                  	| 日志存储           	|          	|
| Messages                 	| 消息系统           	|          	|
| Misc                     	| 杂项               	|          	|
| Mobile                   	| 移动端             	|          	|
| My                       	| “我”的数据中心     	|          	|
| News                     	| 公告               	|          	|
| Packages                 	| 模块的安装卸载     	|          	|
| Rbac                     	| 基于角色的权限控制     	|          	|
| RecordScheduling         	| 服务器录像调度服务 	    |          	|
| Resources                	| 资源               	|          	|
| Settings                 	| 通用设置           	|          	|
| SolrNet                  	| 全站检索           	|          	|
| Sso                      	| 单点登录           	|          	|
| StreamingMedia           	| 流媒体             	|          	|
| Timetables               	| 课表               	|          	|
| Users                    	| 用户信息           	|          	|
| VideoClip                	| 视频切片           	|          	|
| VideoMeetings            	| 第三方视频会议     	|          	|

