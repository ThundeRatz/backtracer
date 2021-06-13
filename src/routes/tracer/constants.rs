use rocket::response::status::Created;
use rocket::serde::json::Json;
use rocket_okapi::openapi;
use rocket_sync_db_pools::diesel::prelude::*;

use crate::{db, models::constant::Constant, schema::constants};

/// Lists all stored constants in the database
#[openapi(tag = "Constants")]
#[get("/constants")]
pub async fn get_constants(conn: db::DbConn) -> Option<Json<Vec<Constant>>> {
    match conn.run(move |c| constants::table.load(c)).await {
        Ok(c) => Some(Json(c)),
        Err(_) => None,
    }
}

/// Returns an specific constant as Json, or None if the constant is not found
#[openapi(tag = "Constants")]
#[get("/constants/<id>")]
pub async fn get_constant_id(id: i64, conn: db::DbConn) -> Option<Json<Constant>> {
    let cte: Constant = conn
        .run(move |c| constants::table.find(id).first(c))
        .await
        .ok()?;

    Some(Json(cte))
}

#[openapi(tag = "Constants")]
#[post("/constants", data = "<cte>", format = "json")]
pub async fn post_constant(
    cte: Json<Constant>,
    conn: db::DbConn,
) -> Option<Created<Json<Constant>>> {
    let post_value = cte.clone();

    match conn
        .run(move |c| {
            diesel::insert_into(constants::table)
                .values(&post_value)
                .execute(c)
        })
        .await
    {
        Ok(_) => Some(Created::new("/").body(cte)),
        Err(e) => {
            println!("Error on creation: {:?}", e);
            None
        }
    }
}
