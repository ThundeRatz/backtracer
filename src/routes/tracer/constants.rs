use std::collections::HashMap;

use rocket::response::status::Created;
use rocket::serde::json::Json;
use rocket::serde::{Deserialize, Serialize};
use rocket_okapi::JsonSchema;

use rocket_okapi::openapi;
use rocket_sync_db_pools::diesel::prelude::*;

use crate::{
    db,
    models::constant::{Constant, ConstantJson, ConstantType},
    schema::tracer::{constant_types, constants},
};

/// Lists all stored constants in the database
#[openapi(tag = "Constants")]
#[get("/constants")]
pub async fn get_constants(conn: db::DbConn) -> Option<Json<Vec<ConstantJson>>> {
    let ctes: Vec<Constant> = conn.run(move |c| constants::table.load(c)).await.ok()?;

    Some(Json(ctes.into_iter().map(|c| c.into()).collect()))
}

/// Returns an specific constant as Json, or None if the constant is not found
#[openapi(tag = "Constants")]
#[get("/constants/<id>")]
pub async fn get_constant_id(id: i32, conn: db::DbConn) -> Option<Json<Vec<ConstantJson>>> {
    let ctes: Vec<Constant> = conn
        .run(move |c| constants::table.filter(constants::id.eq(id)).load(c))
        .await
        .ok()?;

    if ctes.len() == 0 {
        return None;
    }

    Some(Json(ctes.into_iter().map(|c| c.into()).collect()))
}

#[derive(Debug, Clone, Deserialize, Serialize, JsonSchema)]
pub struct NewConstants {
    pub name: String,
    pub values: HashMap<i16, f32>,
}

#[openapi(tag = "Constants")]
#[post("/constants", data = "<cte>", format = "json")]
pub async fn post_constant(cte: Json<NewConstants>, conn: db::DbConn) -> Option<Created<Json<NewConstants>>> {
    let post_value = cte.clone();

    match conn
        .run(move |c| db::constants::add_constant_group(c, post_value.name, post_value.values))
        .await
    {
        Ok(_) => Some(Created::new("/").body(cte)),
        Err(e) => {
            println!("Error on creation: {:?}", e);
            None
        }
    }
}

/// Lists all stored constants in the database
#[openapi(tag = "Constants")]
#[get("/constant_types")]
pub async fn get_constant_types(conn: db::DbConn) -> Option<Json<Vec<ConstantType>>> {
    match conn.run(move |c| constant_types::table.load(c)).await {
        Ok(c) => Some(Json(c)),
        Err(_) => None,
    }
}
