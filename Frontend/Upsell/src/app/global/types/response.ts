export type Response<T> = {
  data: T;
  success: boolean;
  errorMessage?: string;
};
