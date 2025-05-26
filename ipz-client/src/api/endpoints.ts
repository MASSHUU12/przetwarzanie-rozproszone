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
  {
    name: "Leaderboard",
    value: "leaderboard",
    method: "GET",
    params: [
      { label: "Page", type: "number", name: "page" },
      { label: "Per Page", type: "number", name: "per_page" },
    ],
    protected: false,
  },
  {
    name: "Login",
    value: "login",
    method: "POST",
    params: [
      { label: "Email", type: "text", name: "email" },
      { label: "Phone Number", type: "text", name: "phone_number" },
      { label: "Password", type: "text", name: "password" },
    ],
    protected: false,
  },
  {
    name: "Logout",
    value: "logout",
    method: "POST",
    params: [],
    protected: true,
  },
  {
    name: "Register",
    value: "register",
    method: "POST",
    params: [
      { label: "Email", type: "text", name: "email" },
      { label: "Phone Number", type: "text", name: "phone_number" },
      { label: "Password", type: "text", name: "password" },
      {
        label: "Password Confirmation",
        type: "text",
        name: "password_confirmation",
      },
    ],
    protected: false,
  },
  {
    name: "Get User",
    value: "user",
    method: "GET",
    params: [],
    protected: true,
  },
  {
    name: "Delete User",
    value: "user",
    method: "DELETE",
    params: [],
    protected: true,
  },
  {
    name: "Update User",
    value: "user",
    method: "PATCH",
    params: [
      { label: "Email", type: "text", name: "email" },
      { label: "Phone Number", type: "text", name: "phone_number" },
    ],
    protected: true,
  },
  {
    name: "Update Password",
    value: "user/password",
    method: "PATCH",
    params: [
      { label: "Current Password", type: "text", name: "current_password" },
      { label: "New Password", type: "text", name: "new_password" },
      {
        label: "New Password Confirmation",
        type: "text",
        name: "new_password_confirmation",
      },
    ],
    protected: true,
  },
];
