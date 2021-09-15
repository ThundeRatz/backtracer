use rocket_sync_db_pools::{database, diesel};

pub mod constants;
pub mod laps;

#[database("data")]
pub struct DbConn(pub diesel::PgConnection);
