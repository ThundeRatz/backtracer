CREATE SCHEMA "tracer";
CREATE TABLE "tracer"."rules" (
    "id" SERIAL NOT NULL PRIMARY KEY,
    "name" VARCHAR(255) NOT NULL UNIQUE,
    "url" VARCHAR(255) NOT NULL
);
CREATE TABLE "tracer"."constant_types" (
    "id" SMALLINT NOT NULL PRIMARY KEY,
    "description" TEXT NOT NULL UNIQUE
);
CREATE TABLE "tracer"."constant_groups" (
    "id" SERIAL NOT NULL PRIMARY KEY,
    "name" TEXT NOT NULL UNIQUE,
    "created" TIMESTAMP NOT NULL DEFAULT NOW()
);
CREATE TABLE "tracer"."constants" (
    "group_id" INT NOT NULL REFERENCES "tracer"."constant_groups" ON DELETE RESTRICT,
    "ctype" SMALLINT NOT NULL REFERENCES "tracer"."constant_types",
    "value" float4 NOT NULL,
    PRIMARY KEY ("group_id", "ctype")
);
