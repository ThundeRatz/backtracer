<script context="module" lang="ts">
  export const prerender = true;
</script>

<script lang="ts">
  import Button, { Label, Icon } from "@smui/button";
</script>

<main>
  <picture>
    <img src="logo.png" alt="Backtracer Logo" />
  </picture>
  <p>Tracer's backend and API</p>
  <div class="buttons">
    <Button rel="external" href="/docs/index.html" variant="unelevated" class="button-shaped-round">
      <Icon class="material-icons">feed</Icon>
      <Label>Docs</Label>
    </Button>
    <Button
      rel="external"
      href="https://github.com/ThundeRatz/backtracer"
      variant="unelevated"
      class="button-shaped-round"
    >
      <Icon class="material-icons">code</Icon>
      <Label>GitHub</Label>
    </Button>
  </div>

  <div class="line" />
  <div class="marker left" />
  <div class="marker right " />
</main>

<style>
  main {
    text-align: center;
    width: 100vw;
    height: 100vh;
    margin: 0 auto;
    background-color: black;
    display: flex;
    align-content: center;
    justify-content: center;
    align-items: center;
    flex-direction: column;
  }

  img {
    max-width: 90%;
    height: auto;
  }

  .buttons {
    margin-top: 5rem;
  }

  .line {
    position: absolute;
    bottom: 50px;
    width: 100vw;
    height: 30px;
    background-color: white;
  }

  .marker {
    position: absolute;
    bottom: 100px;
    width: 30px;
    height: 50px;
    background-color: white;
  }

  .marker.left {
    left: 100px;
  }

  .marker.right {
    right: 100px;
  }

  p {
    color: white;
    font-size: 1.6rem;
    font-family: "Dancing Script", cursive;
  }
</style>
