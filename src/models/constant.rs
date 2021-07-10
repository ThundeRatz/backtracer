use crate::schema::tracer::*;
use chrono::NaiveDateTime;
use rocket::serde::{Deserialize, Serialize};
use rocket_okapi::JsonSchema;

#[derive(Debug, Clone, Queryable, Insertable, JsonSchema)]
#[table_name = "constants"]
pub struct Constant {
    pub group_id: i32,
    pub ctype: i16,
    pub value: f32,
}

#[derive(Debug, Clone, Queryable, Serialize, Deserialize, JsonSchema)]
pub struct ConstantGroup {
    pub id: i32,
    pub name: String,
    pub created: NaiveDateTime,
}

#[derive(Debug, Clone, Queryable, Insertable, Serialize, Deserialize, JsonSchema)]
#[serde(crate = "rocket::serde")]
#[table_name = "constant_types"]
pub struct ConstantType {
    pub id: i16,
    pub description: String,
}
