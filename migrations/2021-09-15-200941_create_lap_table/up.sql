CREATE TABLE "tracer"."laps" (
    "id" SERIAL NOT NULL PRIMARY KEY,
    "name" TEXT NOT NULL,
    "duration" INT NOT NULL,
    "constants" INT NOT NULL REFERENCES "tracer"."constant_groups" ON DELETE RESTRICT
);
