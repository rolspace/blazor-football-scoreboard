START TRANSACTION;

DELETE FROM `stat`;

UPDATE `game`
SET
    `state` = NULL,
    `quarter` = NULL,
    `quarter_seconds_remaining` = NULL;

COMMIT;
