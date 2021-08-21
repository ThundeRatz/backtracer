use std::vec;

use backtracer::db::constants;
use diesel::Connection;

fn main() {
    let url = env!("DATABASE_URL");
    let conn = diesel::PgConnection::establish(&url).unwrap();

    constants::add_type(&conn, 1, "test type 1".to_string()).unwrap();
    constants::add_type(&conn, 2, "test type 2".to_string()).unwrap();
    constants::add_type(&conn, 3, "test type 3".to_string()).unwrap();

    let c = constants::add_group(
        &conn,
        "test_group".to_string(),
        vec![(1, 1.3), (2, 5.9), (3, 6.7)].into_iter().collect(),
    );

    println!("Inserted {:?}", c);
}
