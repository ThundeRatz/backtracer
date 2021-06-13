use rocket::response::Debug;
use rocket::serde::json::Json;
use rocket_sync_db_pools::diesel::prelude::*;

use crate::{db, models::rule::Rule, schema::rules};

pub type Result<T, E = Debug<diesel::result::Error>> = std::result::Result<T, E>;

/// Lists all stored rules in the database
#[get("/rules")]
pub async fn get_rules(conn: db::DbConn) -> Result<Json<Vec<Rule>>> {
    let rules: Vec<Rule> = conn.run(move |c| rules::table.load(c)).await?;

    Ok(Json(rules))
}
