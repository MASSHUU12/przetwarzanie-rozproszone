import { CircularProgress, Paper, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { Prism as SyntaxHighlighter } from "react-syntax-highlighter";
import { materialLight } from "react-syntax-highlighter/dist/esm/styles/prism";

interface Props {
  loading: boolean;
  body: unknown;
}

export default function ServerResponse({ loading, body }: Props) {
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
        Server Response
      </Typography>
      <Box sx={{ flex: 1, overflow: "auto" }}>
        {loading ? (
          <Box
            sx={{
              height: "100%",
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
            }}
          >
            <CircularProgress />
          </Box>
        ) : (
          <SyntaxHighlighter
            language="json"
            style={materialLight}
            customStyle={{
              background: "transparent",
              fontSize: 14,
              padding: 0,
              margin: 0,
            }}
          >
            {JSON.stringify(body, null, 2)}
          </SyntaxHighlighter>
        )}
      </Box>
    </Paper>
  );
}
