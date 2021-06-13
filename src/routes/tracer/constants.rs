use rocket::response::Debug;
use rocket::serde::json::Json;
use rocket_sync_db_pools::diesel::prelude::*;

use crate::{db, models::constant::Constant, schema::constants};

pub type Result<T, E = Debug<diesel::result::Error>> = std::result::Result<T, E>;

/// Lists all stored constants in the database
#[get("/constants")]
pub async fn get_constants(conn: db::DbConn) -> Result<Json<Vec<Constant>>> {
    let ctes: Vec<Constant> = conn.run(move |c| constants::table.load(c)).await?;

    Ok(Json(ctes))
}

/// Returns an specific constant as Json, or None if the constant is not found
#[get("/constants/<id>")]
pub async fn get_constant_id(id: i64, conn: db::DbConn) -> Option<Json<Constant>> {
    let cte: Constant = conn
        .run(move |c| constants::table.find(id).first(c))
        .await
        .ok()?;

    Some(Json(cte))
}
