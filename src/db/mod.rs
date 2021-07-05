use rocket_sync_db_pools::{database, diesel};

pub mod constants;

#[database("data")]
pub struct DbConn(pub diesel::PgConnection);
