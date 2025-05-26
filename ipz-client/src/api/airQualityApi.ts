import { instance } from "./api";

export interface Coords {
  latitude: number;
  longitude: number;
}

export interface Station {
  id: number;
  name: string;
  latitude: number;
  longitude: number;
  address: string;
  city: string;
  district: string;
  province: string;
}

export interface Pollutant {
  value: number | null;
}

export interface AirQuality {
  index: string;
  pollutants: {
    pm10?: Pollutant;
    pm25?: Pollutant;
    no2?: Pollutant;
    so2?: Pollutant;
    o3?: Pollutant;
    co?: Pollutant;
  };
}

export interface MeasurementRecord {
  parameter: string;
  code: string;
  value: number;
  unit: string;
  measurementTime: string;
}

export type Measurements = MeasurementRecord[];

export interface AirQualityResponse {
  success: boolean;
  timestamp: string;
  request: Coords;
  station: Station;
  airQuality: AirQuality;
  measurements: Measurements;
  forecasts: unknown[];
}

export const getAirQuality = async (
  address: string,
): Promise<AirQualityResponse | null> => {
  try {
    const response = await instance.get<AirQualityResponse>(
      `/air-quality?addr=${address}`,
    );
    return response.data;
  } catch (error: unknown) {
    if (error instanceof Error) {
      console.error(error);
    } else {
      console.error("An unknown error occurred.");
    }
    return null;
  }
};
