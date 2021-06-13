mod constants;
mod laps;
mod rules;

use rocket::Route;

pub fn routes() -> Vec<Route> {
    routes![
        rules::get_rules,
        constants::get_constants,
        constants::get_constant_id,
        laps::get_laps,
    ]
}
