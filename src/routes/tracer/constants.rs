use std::collections::HashMap;

use rocket::response::status::Created;
use rocket::serde::json::Json;
use rocket::serde::{Deserialize, Serialize};
use rocket_okapi::openapi;
use rocket_okapi::JsonSchema;

use crate::db;
use crate::models::constant::{ConstantGroup, ConstantType};
use crate::routes::errors::Errors;

/// Lists all stored constants in the database
#[openapi(tag = "Constants")]
#[get("/constants")]
pub async fn get_constants(conn: db::DbConn) -> Result<Json<Vec<ConstantGroup>>, Errors> {
    match conn.run(move |c| db::constants::get_all(c)).await {
        Ok(ctes) => Ok(Json(ctes)),
        Err(e) => Err(e.into()),
    }
}

#[derive(Debug, Clone, Deserialize, Serialize, JsonSchema)]
pub struct ConstantGroupJson {
    pub name: String,
    pub values: HashMap<i16, f32>,
}

#[openapi(tag = "Constants")]
#[post("/constants", data = "<cte>", format = "json")]
pub async fn post_constant(
    cte: Json<ConstantGroupJson>,
    conn: db::DbConn,
) -> Result<Created<Json<ConstantGroup>>, Errors> {
    let post_value = cte.clone();

    match conn
        .run(move |c| db::constants::add_group(c, post_value.name, post_value.values))
        .await
    {
        Ok(g) => Ok(Created::new("/").body(Json(g))),
        Err(e) => Err(e.into()),
    }
}

// Returns an specific constant group as Json, or None if the constant is not found
#[openapi(tag = "Constants")]
#[get("/constants/<name>")]
pub async fn get_constant(
    name: String,
    conn: db::DbConn,
) -> Result<Option<Json<ConstantGroupJson>>, Errors> {
    let name_copy = name.clone();

    let ctes = conn
        .run(move |c| db::constants::get_one_by_name(c, &name))
        .await?;

    if ctes.len() == 0 {
        return Ok(None);
    }

    Ok(Some(Json(ConstantGroupJson {
        name: name_copy,
        values: ctes,
    })))
}

#[openapi(tag = "Constants")]
#[get("/constant_types")]
pub async fn get_constant_types(conn: db::DbConn) -> Option<Json<Vec<ConstantType>>> {
    match conn.run(move |c| db::constants::get_types(c)).await {
        Ok(c) => Some(Json(c)),
        Err(_) => None,
    }
}

#[openapi(tag = "Constants")]
#[post("/constant_types", data = "<ctype>", format = "json")]
pub async fn post_constant_types(
    conn: db::DbConn,
    ctype: Json<ConstantType>,
) -> Result<Created<Json<ConstantType>>, Errors> {
    let post_value = ctype.clone();

    match conn
        .run(move |c| db::constants::add_type(c, post_value.id, post_value.description))
        .await
    {
        Ok(g) => Ok(Created::new("/").body(Json(g))),
        Err(e) => Err(e.into()),
    }
}
