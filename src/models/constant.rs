use crate::schema::*;
use chrono::NaiveDateTime;
use rocket::serde::{Deserialize, Serialize};

#[derive(Debug, Clone, Queryable, Insertable, Serialize, Deserialize)]
#[serde(crate = "rocket::serde")]
#[table_name = "constants"]
pub struct Constant {
    pub id: i64,
    pub created: NaiveDateTime,
    pub values: Vec<i32>,
    pub alias: String,
}
