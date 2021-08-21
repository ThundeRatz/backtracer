use rocket::http::Status;
use rocket::request::{FromRequest, Outcome, Request};
use serde::Deserialize;

pub struct ApiKey<'r>(&'r str);

#[derive(Debug)]
pub enum ApiKeyError {
    Missing,
    Invalid,
}

#[derive(Deserialize)]
pub struct Config {
    api_key: String,
}

#[rocket::async_trait]
impl<'r> FromRequest<'r> for ApiKey<'r> {
    type Error = ApiKeyError;

    async fn from_request(req: &'r Request<'_>) -> Outcome<Self, Self::Error> {
        let config = req.rocket().state::<Config>().unwrap();

        fn is_valid(key: &str, api_key: &str) -> bool {
            key == api_key
        }

        match req.headers().get_one("x-api-key") {
            None => Outcome::Failure((Status::Unauthorized, ApiKeyError::Missing)),
            Some(key) if is_valid(key, &config.api_key) => Outcome::Success(ApiKey(key)),
            Some(_) => Outcome::Failure((Status::Unauthorized, ApiKeyError::Invalid)),
        }
    }
}
