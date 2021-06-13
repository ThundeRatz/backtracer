use rocket::response::Redirect;

#[get("/")]
pub async fn index() -> Redirect {
    Redirect::to("https://thunderatz.org")
}
