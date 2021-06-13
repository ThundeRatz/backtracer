use backtracer::{self, db, run_migrations};
use rocket::fairing::AdHoc;

#[rocket::main]
async fn main() {
    if let Err(e) = backtracer::rocket()
        .attach(db::DbConn::fairing())
        .attach(AdHoc::on_ignite("Diesel Migrations", run_migrations))
        .launch()
        .await
    {
        println!("Whoops! Rocket didn't launch! {:?}", e);
        drop(e);
    }
}
