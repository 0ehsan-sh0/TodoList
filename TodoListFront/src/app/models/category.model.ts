export interface Category {
  id: number;
  name: string;
  description?: string;
  color?: string;
  created_at: string; // ISO date string
  deleted_at?: string;
  username: string;
}