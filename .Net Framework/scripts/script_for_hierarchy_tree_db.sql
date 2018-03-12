-- table creation
USE tree;
GO
CREATE TABLE tree
(
  item_id bigint NOT NULL IDENTITY (1,1),
  parent_id bigint NOT NULL,
  item_name varchar(max) NOT NULL,
  nodes_amount integer DEFAULT 0,
  CONSTRAINT item_id_pk PRIMARY KEY (item_id),
  CONSTRAINT parent_id_fk FOREIGN KEY (parent_id)
      REFERENCES tree (item_id) ON UPDATE NO ACTION ON DELETE NO ACTION
);


-- populate the table 
USE tree;
GO
INSERT INTO tree (parent_id, item_name, nodes_amount) 
VALUES 
(1, 'Creating Digital Images', 4),
(1, 'Resources',2),
(1, 'Evidence',0),
(1, 'Graphic Products',2),
(2, 'Primary Sources', 0),
(2, 'Secondary Sources', 0),
(4,'Process', 0),
(4, 'Final Product', 0);


-- create function for checking current node of tree for existing of searched child with name 'searched_name'
-- searched_id - id of current node, nodes_limit - amount of children of current node
USE tree;
GO
CREATE FUNCTION get_childnode_by_name_and_parent_id
(
@searched_id AS bigint, 
@searched_name AS varchar(max), 
@nodes_limit AS int
)
RETURNS TABLE 
AS
RETURN
(SELECT TOP 1 * FROM

(SELECT TOP (@nodes_limit) * FROM tree
WHERE parent_id = (@searched_id) 
ORDER BY item_id) 
AS nodes_search

WHERE item_name = @searched_name
ORDER BY item_id)
GO
---


-- create function for checking full path from the head to the searched child of the tree
-- array_pathes - array of nodes
USE tree;
GO
CREATE TYPE PathesArray
AS TABLE 
(
Id int,
Path varchar(max)
);
GO

---
USE tree;
GO

CREATE FUNCTION inspect_path_and_get_last_node
(
@array_pathes AS PathesArray READONLY
)
RETURNS @found_row TABLE 
(
  item_id bigint,
  parent_id bigint,
  item_name varchar(max),
  nodes_amount integer
)
AS
BEGIN
DECLARE @counter AS int = 1,
		@length_array_pathes AS int = 0,
		@current_node_path AS varchar(max),
		@ret_item_id AS bigint = 0,
		@ret_parent_id AS bigint = 0,
		@ret_item_name AS varchar(max),
		@ret_nodes_amount AS int = 0;

-- find root address and assign it's id and amount of nodes to args
SELECT TOP (1)
@ret_item_id = item_id,
@ret_nodes_amount = nodes_amount 
FROM tree
WHERE item_id = parent_id 
AND item_name = (SELECT TOP 1 item_name FROM @array_pathes ORDER BY Id);

-- find the length of incoming array
SET @length_array_pathes = (SELECT COUNT(*) FROM @array_pathes);

-- loop over the array 
WHILE @counter <= @length_array_pathes
	BEGIN

	-- set searched path by index in array of pathes
	SET @current_node_path = (SELECT TOP (1) Path 
	FROM @array_pathes WHERE Id = @counter ORDER BY Id);

	-- assign found result to returned variable 
	INSERT INTO @found_row
	SELECT * FROM get_childnode_by_name_and_parent_id(@ret_item_id, @current_node_path, @ret_nodes_amount);

	-- if not found 
	IF ((SELECT COUNT(*) FROM @found_row) != 0)
	-- prepare for the next iteration
		BEGIN

		SELECT TOP(1) @ret_item_id = item_id,
				@ret_parent_id = parent_id,
				@ret_item_name = item_name,
				@ret_nodes_amount = nodes_amount 
				FROM @found_row;

		DELETE FROM @found_row;
		SET @counter = @counter+1;
		END 

	ELSE
	-- prepare for leaving the loop 
		BEGIN

		DELETE FROM @found_row;
		SET @ret_item_id  = 0;
		SET @ret_parent_id  = 0;
		SET @ret_item_name = '';
		SET @ret_nodes_amount = 0;

		SET @counter = @length_array_pathes + 1;
		END

	END

INSERT INTO @found_row (item_id, parent_id, item_name, nodes_amount)
VALUES (@ret_item_id, @ret_parent_id, @ret_item_name, @ret_nodes_amount);

RETURN;
END;
GO