-- table creation
CREATE TABLE tree
(
  item_id bigint NOT NULL IDENTITY (1,1),
  parent_id bigint NOT NULL,
  item_name text NOT NULL,
  nodes_amount integer DEFAULT 0,
  CONSTRAINT item_id_pk PRIMARY KEY (item_id),
  CONSTRAINT parent_id_fk FOREIGN KEY (parent_id)
      REFERENCES tree (item_id) ON UPDATE NO ACTION ON DELETE NO ACTION
);


-- populate the table 
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
CREATE OR REPLACE FUNCTION tree.if_node_contains_item(IN searched_id bigint, IN searched_name text, IN nodes_limit integer)
RETURNS TABLE(_item_id bigint, _parent_id bigint, _item_name text, _nodes_amount integer) AS
$BODY$

BEGIN
RETURN QUERY 
SELECT * FROM 

(SELECT * FROM tree.tree
WHERE parent_id = searched_id
LIMIT nodes_limit) AS nodes_search

WHERE item_name = searched_name
LIMIT 1;

END;
$BODY$ LANGUAGE plpgsql;


-- create function for checking full path from the head to the searched child of the tree
-- array_pathes - array of nodes
CREATE TYPE node_type 
AS (item_id bigint,
  parent_id bigint,
  item_name text,
  nodes_amount integer);


CREATE OR REPLACE FUNCTION tree.get_node_by_combined_path(array_pathes text[])
  RETURNS node_type AS
$BODY$
DECLARE found_row node_type%ROWTYPE;
BEGIN

SELECT * INTO found_row FROM tree.tree
WHERE item_id = parent_id 
AND item_name = array_pathes[1]
LIMIT 1;

FOR i IN array_lower(array_pathes, 1) .. array_upper(array_pathes, 1)
LOOP

SELECT * INTO found_row FROM tree.if_node_contains_item(found_row.item_id, array_pathes[i], found_row.nodes_amount);

IF NOT FOUND THEN 

found_row.item_id := 0;
found_row.parent_id := 0;
found_row.item_name := '';
found_row.nodes_amount := 0;

RETURN found_row;
END IF;
END LOOP;
RETURN found_row;
END;
$BODY$ LANGUAGE plpgsql;
