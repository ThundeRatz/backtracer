CREATE SCHEMA "tracer";
CREATE TABLE "tracer"."rules" (
    id serial NOT NULL PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE,
    url VARCHAR(255) NOT NULL
);
CREATE TABLE "tracer"."constant_types" (
    "id" smallint NOT NULL PRIMARY KEY,
    "description" TEXT NOT NULL UNIQUE
);
CREATE TABLE "tracer"."constant_groups" (
    "id" serial NOT NULL PRIMARY KEY,
    "name" TEXT NOT NULL UNIQUE,
    "created" TIMESTAMP NOT NULL DEFAULT NOW()
);
CREATE TABLE "tracer"."constants" (
    "group_id" int NOT NULL REFERENCES "tracer"."constant_groups" ON DELETE RESTRICT,
    "ctype" smallint NOT NULL REFERENCES "tracer"."constant_types",
    "value" float4 NOT NULL,
    PRIMARY KEY ("group_id", "ctype")
);
