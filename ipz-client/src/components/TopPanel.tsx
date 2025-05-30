import { Box, Button, Stack, TextField } from "@mui/material";
import { useState, type ChangeEvent, type FormEvent } from "react";
import { endpoints, type Endpoint, type HttpMethod } from "../api/endpoints";
import EndpointsByCategory from "./EndpointsByCategory";
import ScrollableContainer from "../common/ScrollableContainer";

interface TopPanelProps {
  onSubmit: (
    endpoint: string,
    params: Record<string, string>,
    method: HttpMethod,
    protectedEndpoint: boolean,
    token?: string,
  ) => void;
}

function TopPanel({ onSubmit }: TopPanelProps) {
  const [selected, setSelected] = useState<Endpoint>(endpoints[0]);
  const [values, setValues] = useState<Record<string, string>>({});
  const [isProtected, setIsProtected] = useState<boolean>(
    endpoints[0].protected,
  );
  const [token, setToken] = useState<string>("");

  const handleEndpointSelect = (endpoint: Endpoint) => {
    setSelected(endpoint);
    setIsProtected(endpoint.protected);
    setToken("");
    setValues({});
  };

  const handleInputChange = (name: string, value: string) => {
    setValues((prev) => ({ ...prev, [name]: value }));
  };

  const handleTokenChange = (e: ChangeEvent<HTMLInputElement>) => {
    setToken(e.target.value);
  };

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();
    onSubmit(
      selected.value,
      values,
      selected.method,
      isProtected,
      isProtected ? token : undefined,
    );
  };

  return (
    <ScrollableContainer maxHeight="100%">
      <Box
        sx={{
          display: "flex",
          alignItems: "center",
          p: 2,
        }}
      >
        <Stack direction="row" spacing={1}>
          <EndpointsByCategory onEndpointClick={handleEndpointSelect} />
        </Stack>
        <Box sx={{ flex: 1 }} />
        <Box
          component="form"
          onSubmit={handleSubmit}
          sx={{ display: "flex", gap: 1 }}
        >
          {selected.params.map((param) => (
            <TextField
              key={param.name}
              label={param.label}
              size="small"
              type={param.type}
              value={values[param.name] || ""}
              onChange={(e: ChangeEvent<HTMLInputElement>) =>
                handleInputChange(param.name, e.target.value)
              }
              name={param.name}
            />
          ))}
          {isProtected && (
            <TextField
              label="Bearer Token"
              size="small"
              type="password"
              value={token}
              onChange={handleTokenChange}
              name="bearerToken"
              sx={{ minWidth: 220 }}
            />
          )}
          <Button type="submit" variant="contained">
            Send
          </Button>
        </Box>
      </Box>
    </ScrollableContainer>
  );
}

export default TopPanel;
