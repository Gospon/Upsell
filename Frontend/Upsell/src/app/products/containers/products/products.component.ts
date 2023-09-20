import { Component, OnInit } from '@angular/core';
import { ProductHttpService } from '../../services/product-http-service';
import { tap } from 'rxjs';
import { Product } from '../../types/product.type';

@Component({
  selector: 'products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.sass'],
})
export class Products implements OnInit {
  constructor(private productHttpService: ProductHttpService) {}
  products: Product[] = [];
  ngOnInit() {
    this.productHttpService
      .getProducts()
      .pipe(
        tap((products) => {
          this.products = products.data;
        })
      )
      .subscribe();
  }
}
