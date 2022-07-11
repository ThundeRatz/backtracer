import { Add, Edit, Refresh } from "@mui/icons-material";
import {
  Container,
  Grid,
  IconButton,
  Paper,
  styled,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
} from "@mui/material";
import { useEffect, useState } from "react";
import { ConstantsService, ConstantTypeResource } from "../api";

export default function Constants() {
  const [data, setData] = useState<ConstantTypeResource[]>([]);
  const hasKey = () => {
    return localStorage.getItem("API_KEY") != null;
  };

  const updateData = async () => {
    if (!hasKey()) {
      return;
    }

    const newData = await ConstantsService.getConstantTypes();
    console.log(newData);
    setData(newData);
  };

  useEffect(() => {
    updateData();
  }, []);

  return (
    <Container>
      <Grid container spacing={2}>
        <Grid item xs={5}>
          <TableContainer component={Paper}>
            <Table>
              <TableHead>
                <TableRow>
                  <TableCell colSpan={2}>
                    <Typography variant="h6">Constant Types</Typography>
                  </TableCell>
                  <TableCell align="right">
                    <IconButton>
                      <Add />
                    </IconButton>
                    <IconButton onClick={updateData}>
                      <Refresh />
                    </IconButton>
                  </TableCell>
                </TableRow>
                <TableRow>
                  <TableCell>ID</TableCell>
                  <TableCell colSpan={2}>Description</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {data.map((d) => (
                  <TableRow key={d.id}>
                    <TableCell>{d.id}</TableCell>
                    <TableCell>{d.description}</TableCell>
                    <TableCell align="right">
                      <IconButton>
                        <Edit />
                      </IconButton>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        </Grid>
        <Grid item xs={7}>
          <TableContainer component={Paper}>
            <Table>
              <TableHead>
                <TableRow>
                  <TableCell colSpan={3}>
                    <Typography variant="h6">Constants</Typography>
                  </TableCell>
                  <TableCell align="right">
                    <IconButton>
                      <Refresh />
                    </IconButton>
                  </TableCell>
                </TableRow>
                <TableRow>
                  <TableCell>ID</TableCell>
                  <TableCell>Placeholder</TableCell>
                  <TableCell>Placeholder</TableCell>
                  <TableCell>Placeholder</TableCell>
                </TableRow>
              </TableHead>
              <TableBody></TableBody>
            </Table>
          </TableContainer>
        </Grid>
      </Grid>
    </Container>
  );
}
