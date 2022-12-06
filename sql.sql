--don't change the table name or it will break the stuff
CREATE TABLE `bd_character` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `steam` VARCHAR(50) NULL,
  `discord` VARCHAR(50) NULL,
  `data` VARCHAR(1000) NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC));