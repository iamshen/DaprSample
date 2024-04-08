-- Create a database for state data.
CREATE DATABASE dapr_manager OWNER dapr;

-- 存时间(TTL)。当使用Dapr存储数据时，您可以设置ttlInSeconds元数据属性，
--以指示在多少秒后数据应被视为“过期”。
-- 由于PostgreSQL没有内置对ttl的支持，
-- 因此在Dapr中通过在状态表中添加一列来表示何时认为数据“过期”来实现这一点。
-- “过期”的记录不会返回给调用者，即使它们仍然物理地存储在数据库中。
-- 后台“垃圾收集器”定期扫描状态表


-- 创建配置表
-- https://docs.dapr.io/reference/components-reference/supported-configuration-stores/postgresql-configuration-store/
CREATE TABLE IF NOT EXISTS dapr_appsettings (
  KEY VARCHAR NOT NULL,
  VALUE VARCHAR NOT NULL,
  VERSION VARCHAR NOT NULL,
  METADATA JSON
);

-- 创建配置表的触发器

CREATE OR REPLACE FUNCTION notify_event() RETURNS TRIGGER AS $$
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


CREATE TRIGGER config
AFTER INSERT OR UPDATE OR DELETE ON dapr_appsettings
    FOR EACH ROW EXECUTE PROCEDURE notify_event();
