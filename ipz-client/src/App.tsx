import { Box } from "@mui/system";
import SplitPane from "./common/SplitPane";
import ServerResponse from "./components/ServerResponse";
import { useAirQuality } from "./hooks/useAirQuality";

function App() {
  const { data, loading, refetch } = useAirQuality("Szczecin");

  return (
    <Box sx={{ height: "100vh", bgcolor: "#f4f6f8" }}>
      <SplitPane
        direction="vertical"
        initialSize="50%"
        minPrimary={250}
        minSecondary={250}
      >
        <Box sx={{ height: "100%" }}>Top</Box>
        <ServerResponse body={data} loading={loading} />
      </SplitPane>
    </Box>
  );
}

export default App;
