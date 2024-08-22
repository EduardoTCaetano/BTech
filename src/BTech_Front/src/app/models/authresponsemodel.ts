// auth-response.model.ts
export interface AuthResponse {
  userName: string
  emailAddress: string;
  userId: string;
  token?: string;
}
