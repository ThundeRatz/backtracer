use std::vec;

use backtracer::db::constants::add_group;
use diesel::Connection;

fn main() {
    let url = env!("DATABASE_URL");
    let conn = diesel::PgConnection::establish(&url).unwrap();

    let c = add_group(
        &conn,
        "test_group".to_string(),
        vec![(1, 1.3), (2, 5.9), (3, 6.7)].into_iter().collect(),
    );

    println!("Inserted {:?}", c);
}
