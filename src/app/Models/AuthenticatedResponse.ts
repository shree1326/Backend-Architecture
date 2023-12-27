export interface AuthenticatedResponse{
    userName: string;
    jwtToken: string;
    expiration: number;
  }