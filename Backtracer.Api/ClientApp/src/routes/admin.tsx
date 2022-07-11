import { Lock, LockOpen } from "@mui/icons-material";
import {
  AppBar,
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  IconButton,
  styled,
  TextField,
  Toolbar,
  Typography,
} from "@mui/material";
import { useState } from "react";
import { Outlet } from "react-router-dom";

const Main = styled(Box)(({ theme }) => ({
  minHeight: "100vh",
  backgroundColor: theme.palette.background.default,
}));

export default function Admin() {
  const [openDialog, setOpenDialog] = useState(false);
  const [apiKey, setApiKey] = useState("");

  const hasKey = () => {
    const key = localStorage.getItem("API_KEY");
    return key != null && key !== "";
  };

  const cancelDialog = () => {
    setOpenDialog(false);
  };

  const confirmApiKey = () => {
    localStorage.setItem("API_KEY", apiKey);
    setOpenDialog(false);
  };

  return (
    <Main>
      <AppBar sx={{ marginBottom: "1em" }} position="static">
        <Toolbar>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            Backtracer
          </Typography>
          <IconButton
            size="large"
            color={hasKey() ? "inherit" : "error"}
            onClick={() => setOpenDialog(true)}
          >
            {hasKey() ? <Lock /> : <LockOpen />}
          </IconButton>
        </Toolbar>
      </AppBar>

      <Dialog onClose={cancelDialog} open={openDialog}>
        <DialogTitle>Set API Key</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            margin="dense"
            id="key"
            label="API Key"
            type="password"
            fullWidth
            variant="standard"
            value={apiKey}
            onChange={(e) => setApiKey(e.target.value)}
            color="secondary"
          />
        </DialogContent>
        <DialogActions>
          <Button color="secondary" onClick={cancelDialog}>
            Cancel
          </Button>
          <Button color="secondary" onClick={confirmApiKey}>
            Confirm
          </Button>
        </DialogActions>
      </Dialog>

      <Outlet />
    </Main>
  );
}
