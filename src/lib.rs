#[macro_use]
extern crate rocket;

#[macro_use]
extern crate diesel_migrations;

#[macro_use]
extern crate diesel;

use rocket::{Build, Rocket};

pub mod db;
pub mod models;
pub mod schema;

mod routes;

/// Runs all diesel's migrations
pub async fn run_migrations(rocket: Rocket<Build>) -> Rocket<Build> {
    embed_migrations!("migrations");

    let conn = db::DbConn::get_one(&rocket)
        .await
        .expect("database connection");

    conn.run(|c| embedded_migrations::run(c))
        .await
        .expect("diesel migrations");

    rocket
}

/// Returns rocket object with all routes mounted
pub fn rocket() -> Rocket<Build> {
    rocket::build()
        .mount("/", routes::base::routes())
        .mount("/tracer", routes::tracer::routes())
}
