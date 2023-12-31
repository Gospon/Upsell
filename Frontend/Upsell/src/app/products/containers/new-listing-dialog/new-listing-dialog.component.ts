import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, tap } from 'rxjs';
import { Category } from '../../types/category.type';
import {
  selectAuthenticate,
  selectCategories,
  selectShowAddNewListingDialog,
} from '../../store/products.selectors';
import { closeAddNewListingDialog } from '../../store/products.actions';

@Component({
  selector: 'new-listing-dialog',
  templateUrl: './new-listing-dialog.component.html',
  styleUrls: ['./new-listing-dialog.component.sass'],
})
export class NewListingDialog implements OnInit {
  visible$: Observable<boolean>;
  categories: Category[];

  visible: boolean = false;

  authenticate: boolean = false;

  constructor(private store: Store) {}

  ngOnInit() {
    this.visible$ = this.store.select(selectShowAddNewListingDialog);
    this.visible$.pipe(tap((x) => (this.visible = x))).subscribe();

    this.store
      .select(selectCategories)
      .pipe(tap((categories) => (this.categories = categories)))
      .subscribe();

    this.store
      .select(selectAuthenticate)
      .pipe(tap((authenticate) => (this.authenticate = authenticate)))
      .subscribe();
  }

  hideDialog() {
    this.store.dispatch(closeAddNewListingDialog());
  }
}
