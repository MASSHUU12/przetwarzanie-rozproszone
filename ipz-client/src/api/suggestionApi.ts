import { instance } from "./api";

export interface SuggestAddressesResponse {
  suggestions: string[];
}

export const suggestAddresses = async (
  q: string,
): Promise<SuggestAddressesResponse | null> => {
  try {
    const res = await instance.get<SuggestAddressesResponse>(
      "/addresses/suggest",
      {
        params: { q },
      },
    );

    return res.data;
  } catch (error: unknown) {
    if (error instanceof Error) {
      console.error(error);
    } else {
      console.error("An unknown error occurred.");
    }
    return null;
  }
};
