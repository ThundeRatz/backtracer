pub mod tracer {
    table! {
        tracer.constant_groups (id) {
            id -> Int4,
            name -> Text,
            created -> Timestamp,
        }
    }

    table! {
        tracer.constant_types (id) {
            id -> Int2,
            description -> Text,
        }
    }

    table! {
        tracer.constants (group_id, ctype) {
            group_id -> Int4,
            ctype -> Int2,
            value -> Float4,
        }
    }

    table! {
        tracer.rules (id) {
            id -> Int4,
            name -> Varchar,
            url -> Varchar,
        }
    }

    joinable!(constants -> constant_groups (group_id));
    joinable!(constants -> constant_types (ctype));

    allow_tables_to_appear_in_same_query!(
        constant_groups,
        constant_types,
        constants,
        rules,
    );
}
