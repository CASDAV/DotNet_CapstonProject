--- SQL script to create and populate Orders and InventoryItems tables
-- Ensure you have the necessary privileges to create tables and procedures
-- Tested in MariaDB/MySQL

DELIMITER //

-- Stored procedure to populate Orders and InventoryItems
CREATE PROCEDURE PopulateData()
BEGIN
  DECLARE i INT DEFAULT 1;
  DECLARE j INT;

  WHILE i <= 50 DO
    -- Insert into Orders
    INSERT INTO Orders (CustomerName, DatePlaced)
    VALUES (
      CONCAT('Customer ', i),
      CURDATE() - INTERVAL FLOOR(RAND() * 30) DAY
    );

    -- Get the last inserted Order ID
    SET @orderId = LAST_INSERT_ID();

    -- Insert 3 inventory items for each order
    SET j = 1;
    WHILE j <= 3 DO
      INSERT INTO InventoryItems (Name, Quantity, Location, OrderId)
      VALUES (
        CONCAT('Item ', j, ' of Order ', i),
        FLOOR(RAND() * 100) + 1,
        CONCAT('Shelf ', CHAR(FLOOR(RAND() * 6) + 65)), -- Shelf A-F
        @orderId
      );
      SET j = j + 1;
    END WHILE;

    SET i = i + 1;
  END WHILE;
END;
//

DELIMITER ;

-- Call the procedure to populate data
CALL PopulateData();

-- (Optional) Drop the procedure after execution
DROP PROCEDURE PopulateData;
