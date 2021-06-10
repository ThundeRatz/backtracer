use backtracer;

#[rocket::main]
async fn main() {
    if let Err(e) = backtracer::rocket().launch().await {
        println!("Whoops! Rocket didn't launch!");
        drop(e);
    }
}
