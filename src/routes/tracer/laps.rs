use rocket_okapi::openapi;

use crate::auth;

#[openapi(skip)]
#[get("/laps")]
pub async fn get_laps(_auth: auth::ApiKey<'_>) -> &'static str {
    "Hello, laps!"
}
