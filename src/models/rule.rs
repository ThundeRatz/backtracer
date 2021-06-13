use crate::schema::*;
use rocket::serde::{Deserialize, Serialize};

#[derive(Debug, Clone, Queryable, Insertable, Serialize, Deserialize)]
#[serde(crate = "rocket::serde")]
#[table_name = "rules"]
pub struct Rule {
    pub id: i64,
    pub name: String,
    pub url: String,
}
