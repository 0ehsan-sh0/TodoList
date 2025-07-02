export interface Todo {
  id: number;
  title: string;
  description?: string;
  is_done: boolean;
  category_id: number;
}

export interface TodoWithCName {
  id: number;
  title: string;
  description?: string;
  is_done: boolean;
  category_name: string;
  category_id: number;
}