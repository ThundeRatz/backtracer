use std::collections::HashMap;

use crate::{models::constant::*, schema::tracer::*};
use diesel::result::Error;
use rocket_sync_db_pools::diesel::prelude::*;

// Constant (groups and actual values)
pub fn get_all(conn: &PgConnection) -> Result<Vec<ConstantGroup>, Error> {
    constant_groups::table.load(conn)
}

pub fn get_one_by_name(conn: &PgConnection, name: &str) -> Result<HashMap<i16, f32>, Error> {
    let mut hm = HashMap::new();

    let ctes = constants::table
        .left_join(constant_groups::table.on(constants::group_id.eq(constant_groups::id)))
        .select((constants::ctype, constants::value))
        .filter(constant_groups::name.eq(name))
        .load::<(i16, f32)>(conn)?;

    for (k, v) in ctes {
        hm.insert(k, v);
    }

    Ok(hm)
}

pub fn add_group(
    conn: &PgConnection,
    name: String,
    constants: HashMap<i16, f32>,
) -> Result<ConstantGroup, Error> {
    conn.transaction::<ConstantGroup, Error, _>(|| {
        let cgroup = diesel::insert_into(constant_groups::table)
            .values(constant_groups::name.eq(name))
            .get_result::<ConstantGroup>(conn)?;

        let new_constants: Vec<Constant> = constants
            .into_iter()
            .map(|(k, v)| Constant {
                group_id: cgroup.id,
                ctype: k,
                value: v,
            })
            .collect();

        diesel::insert_into(constants::table)
            .values(&new_constants)
            .execute(conn)?;

        Ok(cgroup)
    })
}

// Constant Types
pub fn get_types(conn: &PgConnection) -> Result<Vec<ConstantType>, Error> {
    constant_types::table.load(conn)
}

pub fn add_type(conn: &PgConnection, id: i16, description: String) -> Result<ConstantType, Error> {
    conn.transaction::<ConstantType, Error, _>(|| {
        let ctype = diesel::insert_into(constant_types::table)
            .values((
                constant_types::id.eq(id),
                constant_types::description.eq(description),
            ))
            .get_result::<ConstantType>(conn)?;

        Ok(ctype)
    })
}
