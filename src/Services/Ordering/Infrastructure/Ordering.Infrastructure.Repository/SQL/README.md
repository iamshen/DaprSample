# SQL 初始化流程

## 创建模式

```postgresql
-- 创建 业务模式
CREATE SCHEMA gold_work;

## 设置默认模式

```postgresql
-- 设置方式为在当前所在的数据库执行
-- ALTER ROLE 用户名(例如 postgres ) SET search_path=gold_work
ALTER ROLE postgres SET search_path=gold_work;

-- 验证（需要断开会话，重新链接）
show search_path ;
```

## 创建业务表

SQL 初始化详情点击查看以下文件

[gold_work.sql](gold_work.sql)

