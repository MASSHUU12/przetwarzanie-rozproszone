import { useAirQuality } from "./hooks/useAirQuality";

function App() {
  const {
    data: airData,
    loading: airLoading,
    refetch: refetchAirData,
  } = useAirQuality("Szczecin");

  return <>{!airLoading && <p>{airData?.station.address}</p>}</>;
}

export default App;
