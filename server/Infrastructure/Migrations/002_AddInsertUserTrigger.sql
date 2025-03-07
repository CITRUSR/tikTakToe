CREATE OR REPLACE FUNCTION add_user_stats()
RETURNS TRIGGER
AS
$$
BEGIN 
    INSERT INTO user_stats (user_id)
    VALUES (NEW.id);
    RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER add_stats AFTER INSERT ON users
FOR EACH ROW EXECUTE FUNCTION add_user_stats();