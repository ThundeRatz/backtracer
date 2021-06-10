use rocket::Build;

mod routes;

#[macro_use]
extern crate rocket;

pub fn rocket() -> rocket::Rocket<Build> {
    rocket::build().mount("/tracer", routes::tracer::routes())
}
