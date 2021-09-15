use crate::schema::tracer::*;
use rocket::serde::{Deserialize, Serialize};
use rocket_okapi::JsonSchema;

#[derive(Debug, Clone, Queryable, Insertable, Serialize, Deserialize, JsonSchema)]
#[serde(crate = "rocket::serde")]
#[table_name = "laps"]
pub struct Lap {
    pub id: i32,
    pub name: String,
    pub duration: i32,
    pub constants: i32,
}
