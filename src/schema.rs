table! {
    constants (id) {
        id -> Int8,
        created -> Timestamp,
        values -> Array<Int4>,
        alias -> Varchar,
    }
}

table! {
    rules (id) {
        id -> Int8,
        name -> Varchar,
        url -> Varchar,
    }
}

allow_tables_to_appear_in_same_query!(
    constants,
    rules,
);
