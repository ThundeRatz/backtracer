#[get("/")]
pub async fn index() -> &'static str {
    "Hello, tracer!"
}
