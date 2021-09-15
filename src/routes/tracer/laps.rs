use rocket::response::status::Created;
use rocket::serde::json::Json;
use rocket::serde::{Deserialize, Serialize};
use rocket_okapi::openapi;
use rocket_okapi::JsonSchema;

use crate::models::lap::Lap;
use crate::routes::errors::Errors;
use crate::{auth, db};

#[openapi(tag = "Laps")]
#[get("/laps")]
pub async fn get_laps(_auth: auth::ApiKey<'_>, conn: db::DbConn) -> Result<Json<Vec<Lap>>, Errors> {
    match conn.run(move |c| db::laps::get_all(c)).await {
        Ok(laps) => Ok(Json(laps)),
        Err(e) => Err(e.into()),
    }
}

#[derive(Debug, Clone, Deserialize, Serialize, JsonSchema)]
pub struct LapJson {
    pub name: String,
    pub duration: i32,
    pub constant_group_id: i32,
}

#[openapi(tag = "Laps")]
#[post("/laps", data = "<lap>", format = "json")]
pub async fn post_lap(
    _auth: auth::ApiKey<'_>,
    conn: db::DbConn,
    lap: Json<LapJson>,
) -> Result<Created<Json<Lap>>, Errors> {
    let post_value = lap.clone();

    match conn
        .run(move |c| db::laps::add_lap(c, post_value.name, post_value.duration, post_value.constant_group_id))
        .await
    {
        Ok(g) => Ok(Created::new("/").body(Json(g))),
        Err(e) => Err(e.into()),
    }
}
