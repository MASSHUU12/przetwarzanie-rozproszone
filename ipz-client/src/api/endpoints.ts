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
  category: string;
}

export const endpoints: Endpoint[] = [
  {
    name: "Get Air Quality",
    value: "air-quality",
    method: "GET",
    params: [{ label: "Address", type: "text", name: "addr" }],
    protected: false,
    category: "Air Quality",
  },
  {
    name: "Suggest Address",
    value: "addresses/suggest",
    method: "GET",
    params: [{ label: "Address", type: "text", name: "q" }],
    protected: false,
    category: "Address",
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
    category: "Leaderboard",
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
    category: "Authentication",
  },
  {
    name: "Logout",
    value: "logout",
    method: "POST",
    params: [],
    protected: true,
    category: "Authentication",
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
    category: "Authentication",
  },
  {
    name: "Get User",
    value: "user",
    method: "GET",
    params: [],
    protected: true,
    category: "User",
  },
  {
    name: "Delete User",
    value: "user",
    method: "DELETE",
    params: [],
    protected: true,
    category: "User",
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
    category: "User",
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
    category: "User",
  },
];
