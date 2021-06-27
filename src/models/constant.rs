use crate::schema::*;
use chrono::{NaiveDateTime, Utc};
use rocket::serde::{Deserialize, Serialize};
use rocket_okapi::JsonSchema;

fn naive_now() -> NaiveDateTime {
    Utc::now().naive_utc()
}

#[derive(Debug, Clone, Queryable, Insertable, Serialize, Deserialize, JsonSchema)]
#[serde(crate = "rocket::serde")]
#[table_name = "constants"]
pub struct Constant {
    #[serde(skip_deserializing)]
    pub id: Option<i64>,

    #[serde(skip_deserializing)]
    #[serde(default = "naive_now")]
    pub created: NaiveDateTime,

    pub values: Vec<i32>,
    pub alias: String,
}
