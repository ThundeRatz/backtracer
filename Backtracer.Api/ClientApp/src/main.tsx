import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { ThemeProvider } from "@mui/material/styles";

import App from "./App";
import Admin from "./routes/admin";
import Constants from "./routes/constants";
import NotFound from "./routes/404";
import theme from "./theme";
import { OpenAPI } from "./api/core/OpenAPI";

import "./index.css";

OpenAPI.HEADERS = async () => {
  return {
    "x-api-key": localStorage.getItem("API_KEY") ?? "",
  };
};

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <ThemeProvider theme={theme}>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<App />}></Route>
          <Route path="/admin" element={<Admin />}>
            <Route path="constants" element={<Constants />}></Route>
          </Route>
          <Route path="*" element={<NotFound />} />
        </Routes>
      </BrowserRouter>
    </ThemeProvider>
  </React.StrictMode>
);
