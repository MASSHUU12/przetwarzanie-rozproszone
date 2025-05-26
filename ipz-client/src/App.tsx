import { Box } from "@mui/system";
import { useState } from "react";
import { instance } from "./api/api";
import type { HttpMethod } from "./api/endpoints";
import SplitPane from "./common/SplitPane";
import Note from "./components/Note";
import ServerResponse from "./components/ServerResponse";
import TopPanel from "./components/TopPanel";

function App() {
  const [data, setData] = useState<unknown>([]);
  const [loading, setLoading] = useState<boolean>(false);

  const handleEndpointSubmit = async (
    endpoint: string,
    params: Record<string, string>,
    method: HttpMethod,
    protectedEndpoint: boolean,
    token?: string,
  ) => {
    try {
      setLoading(true);

      const config = {
        headers: {} as Record<string, string>,
      };

      if (protectedEndpoint && token) {
        config.headers["Authorization"] = `Bearer ${token}`;
      }

      let response;
      if (method === "GET") {
        response = await instance.get(`/${endpoint}`, {
          ...config,
          params,
        });
      } else {
        response = await instance.request({
          url: `/${endpoint}`,
          method,
          data: params,
          ...config,
        });
      }

      setData(response.data);
    } catch (error: unknown) {
      setData(error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Box sx={{ height: "100vh", bgcolor: "#f4f6f8" }}>
      <SplitPane
        direction="vertical"
        initialSize="50%"
        minPrimary={250}
        minSecondary={250}
      >
        <TopPanel onSubmit={handleEndpointSubmit} />
        <SplitPane
          direction="horizontal"
          initialSize="50%"
          minPrimary={250}
          minSecondary={250}
          style={{ height: "100%" }}
        >
          <ServerResponse body={data} loading={loading} />
          <Note />
        </SplitPane>
      </SplitPane>
    </Box>
  );
}

export default App;
