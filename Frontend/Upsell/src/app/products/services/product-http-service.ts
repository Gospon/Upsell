import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Endpoints } from '../types/endpoints.enums';
import { Response } from 'src/app/global/types/response';
import { Product } from '../types/product.type';

@Injectable({
  providedIn: 'root',
})
export class ProductHttpService {
  constructor(private http: HttpClient) {}

  addProducts(product: any): Observable<Response<any[]>> {
    return this.http.post<Response<any[]>>(
      `${environment.baseUrl}${Endpoints.addProduct}`,
      product
    );
  }

  getProducts(): Observable<Response<Product[]>> {
    return this.http.get<Response<Product[]>>(
      `${environment.baseUrl}${Endpoints.getProduct}`
    );
  }
}
