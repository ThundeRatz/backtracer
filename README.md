![backtracer](docs/logo.png)

This has nothing to do with backtracing, it's just tracer's backend and API :)

---

[![Swagger Docs](https://validator.swagger.io/validator/?url=https%3A%2F%2Fapi.thunderatz.org%2Ftracer%2Fopenapi.json)](https://api.thunderatz.org/tracer)

Backtracer is the backend for [Monitracer](https://github.com/ThundeRatz/Monitracer), [Tracer's](https://thunderatz.org/projects/robots/tracer) app.
It works as an API to save and retrive the robot's information, including constants, rules and testing info. This is in a **very** early stage of development.
API docs can be found on the badge above.

## Getting started

Live version is at https://api.thunderatz.org/tracer, for local development and testing:

```bash
# make sure you have rust installed, run this and follow instructions
curl --proto '=https' --tlsv1.2 -sSf https://sh.rustup.rs | sh

# we need a postgres database running, for testing we can use docker and the provided stack file
docker swarm init  # If it's not already initialized
docker stack deploy -c stack.yml postgres

# Rename/copy Rocket.toml.example to Rocket.toml and adjust databse info if needed
cp Rocket.toml.example Rocket.toml

# Run the app, migrations are run automatically with diesel_migrations
cargo run
```

App runs by default at `localhost:8000`, go to `localhost:8000/tracer` to see the docs and try the API

---

As stated, this is in very early stages, there's no authentication and only a few endpoints,
this README will be updated when more features are added

---

<img src="https://static.thunderatz.org/teamassets/logo-simples.png" width="200px" />
