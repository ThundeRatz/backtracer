use rocket_sync_db_pools::{database, diesel};

#[database("data")]
pub struct DbConn(diesel::PgConnection);
