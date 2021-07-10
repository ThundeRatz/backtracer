use backtracer::routes::tracer::constants::ConstantGroupJson;

fn main() {
    let nc = ConstantGroupJson {
        name: "name".to_string(),
        values: vec![(1, 1.3), (2, 3.5), (3, 5.6)].into_iter().collect(),
    };

    let string = serde_json::to_string(&nc).unwrap();

    println!("{}", string);

    let nc_parsed = serde_json::from_str::<ConstantGroupJson>(string.as_str()).unwrap();

    println!("{:?}", nc_parsed);
}
