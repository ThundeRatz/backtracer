#[get("/laps")]
pub async fn get_laps() -> &'static str {
    "Hello, laps!"
}
