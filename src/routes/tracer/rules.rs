use rocket::{response::Redirect, serde::json::Json};
use rocket_okapi::openapi;
use rocket_sync_db_pools::diesel::prelude::*;

use crate::{auth, db, models::rule::Rule, schema::tracer::rules};

/// Lists all stored rules in the database
#[openapi(tag = "Rules")]
#[get("/rules")]
pub async fn get_rules(
    _auth: auth::ApiKey<'_>,
    conn: db::DbConn,
) -> Result<Json<Vec<Rule>>, Json<&'static str>> {
    match conn.run(move |c| rules::table.load(c)).await {
        Ok(c) => Ok(Json(c)),
        Err(_) => Err(Json("Failed to fetch rules")),
    }
}

/// Lists all stored rules in the database
#[openapi(tag = "Rules")]
#[get("/rules/<id>")]
pub async fn get_rule_id(_auth: auth::ApiKey<'_>, id: i32, conn: db::DbConn) -> Option<Json<Rule>> {
    let rule: Rule = conn
        .run(move |c| rules::table.find(id).first(c))
        .await
        .ok()?;

    Some(Json(rule))
}

/// Lists all stored rules in the database
#[openapi(skip)]
#[get("/rules/<id>/go")]
pub async fn redirect_to_rule(
    _auth: auth::ApiKey<'_>,
    id: i32,
    conn: db::DbConn,
) -> Option<Redirect> {
    let rule: Rule = conn
        .run(move |c| rules::table.find(id).first(c))
        .await
        .ok()?;

    Some(Redirect::to(rule.url))
}
