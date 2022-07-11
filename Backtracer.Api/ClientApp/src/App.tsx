import styled from "@emotion/styled";
import Button from "@mui/material/Button";
import FeedIcon from "@mui/icons-material/Feed";
import GitHubIcon from "@mui/icons-material/GitHub";

import { CenteredContent } from "./components";

import LogoImage from "./assets/logo.png";

const Logo = styled.img`
  max-width: 90%;
  height: auto;
`;

const Subtitle = styled.p`
  color: white;
  font-size: 1.3rem;
  font-family: "Roboto";
`;

const Buttons = styled.div`
  margin-top: 5rem;
`;

const Line = styled.div`
  position: absolute;
  bottom: 50px;
  width: 100vw;
  height: 30px;
  background-color: white;
`;

const Marker = styled.div`
  position: absolute;
  bottom: 100px;
  width: 30px;
  height: 50px;
  background-color: white;
`;

const MarkerLeft = styled(Marker)`
  left: 100px;
`;

const MarkerRight = styled(Marker)`
  right: 100px;
`;

export default function App() {
  return (
    <CenteredContent>
      <picture>
        <Logo src={LogoImage}></Logo>
      </picture>
      <Subtitle>Tracer's backend and API</Subtitle>
      <Buttons>
        <Button
          rel="external"
          href="/docs/index.html"
          variant="contained"
          startIcon={<FeedIcon />}
        >
          Docs
        </Button>
        <Button
          rel="external"
          href="https://github.com/ThundeRatz/backtracer"
          variant="contained"
          startIcon={<GitHubIcon />}
        >
          GitHub
        </Button>
      </Buttons>

      <Line />
      <MarkerLeft />
      <MarkerRight />
    </CenteredContent>
  );
}
