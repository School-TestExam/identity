CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER TABLE `Identities` ADD `CreatedAt` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00';

ALTER TABLE `Identities` ADD `CreatedBy` longtext CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `Identities` ADD `LastUpdatedBy` longtext CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `Identities` ADD `UpdatedAt` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00';

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240527091608_InitialCreate', '8.0.3');

COMMIT;

