# 项目配置说明


## `Dapr` 配置文件目录


### 1. `resources` 

- 在 Dapr 的上下文中，resources 目录可能用于存储与应用相关的其他资源配置，例如 Kubernetes 部署文件、Docker Compose 文件等。
- 这些配置文件通常与部署和运维相关，而不是 Dapr 组件的配置。


### 2. `components` 

- 存储 Dapr 组件的配置文件，这些配置文件定义了 Dapr 组件的属性和行为，如状态存储、发布订阅、绑定等。
- 这些组件配置文件会被 Dapr 运行时自动加载，并根据配置初始化相应的组件。


#### `dapr postgres` 配置

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