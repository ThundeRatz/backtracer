import { Box, styled } from "@mui/material";

export const CenteredContent = styled(Box)(({ theme }) => ({
  textAlign: "center",
  width: "100vw",
  height: "100vh",
  margin: "0 auto",
  backgroundColor: theme.palette.background.default,
  display: "flex",
  alignContent: "center",
  justifyContent: "center",
  alignItems: "center",
  flexDirection: "column",
}));
