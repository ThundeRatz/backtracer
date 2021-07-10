pub mod constants;
pub mod laps;
pub mod rules;

use rocket::Route;
use rocket_okapi::routes_with_openapi;
use rocket_okapi::swagger_ui::{make_swagger_ui, SwaggerUIConfig};

fn get_docs() -> SwaggerUIConfig {
    SwaggerUIConfig {
        url: "/tracer/openapi.json".to_string(),
        ..Default::default()
    }
}

pub fn routes() -> Vec<Route> {
    let mut routes: Vec<Route> = routes_with_openapi![
        rules::get_rules,
        rules::get_rule_id,
        rules::redirect_to_rule,
        constants::get_constants,
        constants::get_constant,
        constants::post_constant,
        constants::get_constant_types,
        constants::post_constant_types,
        laps::get_laps,
    ];

    let mut swagger_route: Vec<Route> = make_swagger_ui(&get_docs()).into();

    routes.append(&mut swagger_route);

    routes
}
