import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Endpoints } from '../types/endpoints.enums';
import { Response } from 'src/app/global/types/response';
import { Category } from 'src/app/products/types/category.type';

@Injectable({
  providedIn: 'root',
})
export class NavigationHttpService {
  constructor(private http: HttpClient) {}

  getCategories(): Observable<Response<Category[]>> {
    return this.http.get<Response<Category[]>>(
      `${environment.baseUrl}${Endpoints.categories}`
    );
  }
}
