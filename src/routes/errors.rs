use std::collections::HashMap;

use okapi::openapi3::Responses;
use rocket::http::Status;
use rocket::request::Request;
use rocket::response::content::Json;
use rocket::response::{self, status, Responder};
use rocket_okapi::util::set_content_type;
use rocket_okapi::{self, gen::OpenApiGenerator, response::OpenApiResponderInner};
use serde_json::json;

#[derive(Debug)]
pub struct Errors {
    pub errors: HashMap<String, String>,
}

impl Errors {
    pub fn new(errs: Vec<(String, String)>) -> Self {
        Self {
            errors: errs.into_iter().collect(),
        }
    }
}

impl From<diesel::result::Error> for Errors {
    fn from(err: diesel::result::Error) -> Errors {
        match err {
            diesel::result::Error::DatabaseError(k, d) => Errors::new(vec![(
                format!("{:?}", k),
                format!("{:?}", d).replace('"', "").replace('\\', ""),
            )]),
            _ => Errors::new(vec![("Unknown Error".to_string(), err.to_string())]),
        }
    }
}

impl<'r> Responder<'r, 'r> for Errors {
    fn respond_to(self, req: &Request) -> response::Result<'r> {
        status::Custom(Status::BadRequest, Json(json!({ "errors": self.errors }))).respond_to(req)
    }
}

impl OpenApiResponderInner for Errors {
    fn responses(_: &mut OpenApiGenerator) -> rocket_okapi::Result<Responses> {
        let mut responses = Responses::default();
        set_content_type(&mut responses, 500)?;
        Ok(responses)
    }
}
