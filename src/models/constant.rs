use crate::schema::tracer::*;
use chrono::NaiveDateTime;
use rocket::serde::{Deserialize, Serialize};
use rocket_okapi::JsonSchema;

// fn naive_now() -> NaiveDateTime {
//     Utc::now().naive_utc()
// }

#[derive(Debug, Clone, Queryable)]
pub struct Constant {
    pub id: i32,
    // pub created: NaiveDateTime,
    pub group_id: i32,
    pub ctype: i16,
    pub value: f32,
}

#[derive(Debug, Clone, Queryable)]
pub struct ConstantGroup {
    pub id: i32,
    pub name: String,
    pub created: NaiveDateTime,
}

impl From<Constant> for ConstantJson {
    fn from(cte: Constant) -> Self {
        ConstantJson {
            id: cte.id,
            group_id: cte.group_id,
            ctype: cte.ctype,
            value: cte.value,
        }
    }
}

#[derive(Debug, Clone, Serialize, JsonSchema)]
#[serde(crate = "rocket::serde")]
pub struct ConstantJson {
    pub id: i32,
    pub group_id: i32,
    pub ctype: i16,
    pub value: f32,
}

#[derive(Debug, Clone, Queryable, Insertable, Serialize, Deserialize, JsonSchema)]
#[serde(crate = "rocket::serde")]
#[table_name = "constant_types"]
pub struct ConstantType {
    pub id: i16,
    pub description: String,
}
