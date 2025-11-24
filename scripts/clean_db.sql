BEGIN;

DELETE FROM "football"."stat";

UPDATE "football"."game"
SET
    "state" = NULL,
    "quarter" = NULL,
    "quarter_seconds_remaining" = NULL;

COMMIT;
