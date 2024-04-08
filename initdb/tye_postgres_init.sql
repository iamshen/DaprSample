

-- Create a database for state data.

CREATE DATABASE dapr_manager;


-- 存时间(TTL)。当使用Dapr存储数据时，您可以设置ttlInSeconds元数据属性，
--以指示在多少秒后数据应被视为“过期”。
-- 由于PostgreSQL没有内置对ttl的支持，
-- 因此在Dapr中通过在状态表中添加一列来表示何时认为数据“过期”来实现这一点。
-- “过期”的记录不会返回给调用者，即使它们仍然物理地存储在数据库中。
-- 后台“垃圾收集器”定期扫描状态表


-- 状态表中存储记录过期日期的列expidate在默认情况下没有索引，
--因此每次定期清理都必须执行全表扫描。
--如果您有一个包含大量记录的表，并且其中只有一些记录使用TTL，
-- 那么您可能会发现在该列上创建索引很有用。假设你的状态表名称是state(默认值)，你可以使用这个查询:

CREATE INDEX expiredate_idx
    ON dapr_state
    USING btree (expiredate ASC NULLS LAST);
