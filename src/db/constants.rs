use std::collections::HashMap;

use crate::{models::constant::ConstantGroup, schema::tracer::*};
use diesel::result::Error;
use rocket_sync_db_pools::diesel::prelude::*;

pub async fn get_constants_by_group(_: &PgConnection) {}

pub fn add_constant_group<'a>(
    conn: &PgConnection,
    name: String,
    constants: HashMap<i16, f32>,
) -> Result<(), Error> {
    conn.transaction(|| {
        let gid: i32;

        match diesel::insert_into(constant_groups::table)
            .values(constant_groups::name.eq(name))
            .get_result::<ConstantGroup>(conn)
        {
            Ok(v) => gid = v.id,
            Err(e) => return Err(e),
        }

        for (k, v) in constants {
            if let Err(e) = diesel::insert_into(constants::table)
                .values((
                    constants::group_id.eq(gid),
                    constants::ctype.eq(k),
                    constants::value.eq(v),
                ))
                .execute(conn)
            {
                return Err(e);
            }
        }

        Ok(())
    })
}
