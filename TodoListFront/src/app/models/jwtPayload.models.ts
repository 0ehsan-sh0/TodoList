export interface JwtPayload {
  name: string;
  role: string;
  exp: number;
  iat: number;
  [key: string]: any; // fallback for any other claim
}
