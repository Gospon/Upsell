import { createSelector, createFeatureSelector } from '@ngrx/store';
import { ProductsState } from './products.reducer';

export const selectFeatureState =
  createFeatureSelector<ProductsState>('products');

export const selectShowAddNewListingDialog = createSelector(
  selectFeatureState,
  (state: ProductsState) => state.showAddNewListingDialog
);

export const selectCategories = createSelector(
  selectFeatureState,
  (state: ProductsState) => state.categories
);

export const selectAuthenticate = createSelector(
  selectFeatureState,
  (state: ProductsState) => state.authenticate
);
