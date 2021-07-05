use crate::schema::tracer::*;
use rocket::serde::{Deserialize, Serialize};
use rocket_okapi::JsonSchema;

#[derive(Debug, Clone, Queryable, Insertable, Serialize, Deserialize, JsonSchema)]
#[serde(crate = "rocket::serde")]
#[table_name = "rules"]
pub struct Rule {
    pub id: i32,
    pub name: String,
    pub url: String,
}
