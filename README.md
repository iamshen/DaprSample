# 使用 Dapr 构建分布式应用测试项目

## 分层

业务逻辑分为两层,分别为 领域(`Domain`) 层和 应用(`Application`) 层,它们包含不同类型的业务逻辑.

- 领域层:只实现领域业务逻辑,与用例无关。
- 应用层:基于领域层来实现满足用例的业务逻辑.用例可以看作是用户界面(UI)或外部应用程序的交互。
- 展现层:包含应用程序的UI元素。
- 基础设施层:通过对第三方库的集成或抽象,来满足其它层的非核心业务逻辑的实现。







### dapr postgres 配置表

```postgresql
-- # 
CREATE TABLE IF	NOT EXISTS gold_dapr.appsettings ( KEY VARCHAR NOT NULL, VALUE VARCHAR NOT NULL, VERSION VARCHAR NOT NULL, METADATA JSON );

-- # 在配置表上创建 触发器 TRIGGER。创建 TRIGGER 的函数示例如下

CREATE OR REPLACE FUNCTION configuration_event() RETURNS TRIGGER AS $$
    DECLARE 
        data json;
        notification json;
    
    BEGIN

        IF (TG_OP = 'DELETE') THEN
            data = row_to_json(OLD);
        ELSE
            data = row_to_json(NEW);
        END IF;
        
        notification = json_build_object(
                          'table',TG_TABLE_NAME,
                          'action', TG_OP,
                          'data', data);

        PERFORM pg_notify('config',notification::text);
        RETURN NULL; 
    END;  
$$ LANGUAGE plpgsql;


-- # 创建触发器，在标为数据的字段中封装数据



-- 订阅配置通知时，应使用作为 pg_notify 属性提及的通道。
-- 由于这是一个通用创建的触发器，请将此触发器映射到配置表

CREATE TRIGGER config
AFTER INSERT OR UPDATE OR DELETE ON "gold_dapr"."appsettings"
FOR EACH ROW EXECUTE PROCEDURE "gold_dapr"."configuration_event"();

```