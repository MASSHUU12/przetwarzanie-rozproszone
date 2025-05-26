export type ParamType = "number" | "text";
export type HttpMethod = "GET" | "POST" | "PUT" | "DELETE" | "PATCH";

export interface EndpointParam {
  label: string;
  type: ParamType;
  name: string;
}

export interface Endpoint {
  name: string;
  value: string;
  method: HttpMethod;
  params: EndpointParam[];
  protected: boolean;
}

export const endpoints: Endpoint[] = [
  {
    name: "Get Air Quality",
    value: "air-quality",
    method: "GET",
    params: [{ label: "Address", type: "text", name: "addr" }],
    protected: false,
  },
  {
    name: "Suggest Address",
    value: "addresses/suggest",
    method: "GET",
    params: [{ label: "Address", type: "text", name: "q" }],
    protected: false,
  },
];
