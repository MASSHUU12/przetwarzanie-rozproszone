import { Paper, TextField, Typography } from "@mui/material";

export default function Note() {
  return (
    <Paper
      elevation={3}
      sx={{
        height: "100%",
        p: 2,
        display: "flex",
        flexDirection: "column",
      }}
    >
      <Typography variant="h6" gutterBottom>
        Notes
      </Typography>
      <TextField
        multiline
        fullWidth
        sx={{ height: "100%", flex: 1 }}
        InputProps={{
          sx: {
            height: "100%",
            alignItems: "stretch",
            "& .MuiInputBase-input": {
              height: "100% !important",
              boxSizing: "border-box",
              overflow: "auto",
            },
            "& textarea": {
              height: "100% !important",
              boxSizing: "border-box",
              overflow: "auto",
              resize: "none",
            },
          },
        }}
      />
    </Paper>
  );
}
