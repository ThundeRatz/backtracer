CREATE TABLE constants (
    id bigserial NOT NULL PRIMARY KEY,
    created timestamp DEFAULT NOW() NOT NULL,
    "values" integer [] NOT NULL,
    alias VARCHAR(255) NOT NULL UNIQUE
);
CREATE TABLE rules (
    id bigserial NOT NULL PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE,
    url VARCHAR(255) NOT NULL
);
