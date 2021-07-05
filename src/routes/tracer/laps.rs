use rocket_okapi::openapi;

#[openapi(skip)]
#[get("/laps")]
pub async fn get_laps() -> &'static str {
    "Hello, laps!"
}
