use diesel::{PgConnection, QueryResult};

use crate::{models::lap::*, schema::tracer::*};
use diesel::result::Error;
use rocket_sync_db_pools::diesel::prelude::*;

pub fn get_all(conn: &PgConnection) -> QueryResult<Vec<Lap>> {
    laps::table.load(conn)
}

pub fn add_lap(conn: &PgConnection, name: String, duration: i32, constant_group_id: i32) -> QueryResult<Lap> {
    conn.transaction::<Lap, Error, _>(|| {
        let ctype = diesel::insert_into(laps::table)
            .values((
                laps::name.eq(name),
                laps::duration.eq(duration),
                laps::constants.eq(constant_group_id),
            ))
            .get_result::<Lap>(conn)?;

        Ok(ctype)
    })
}
