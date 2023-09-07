import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Endpoints } from '../types/endpoints.enums';
import { Response } from 'src/app/global/types/response';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationHttpService {
  constructor(private http: HttpClient) {}

  registerUser(registerData: any): Observable<Response<string>> {
    return this.http.post<Response<string>>(
      `${environment.baseUrl}api/Auth/register`,
      registerData
    );
  }

  loginUser(loginData: any): Observable<Response<string>> {
    return this.http.post<Response<string>>(
      `${environment.baseUrl}api/Auth/login`,
      loginData
    );
  }
}
