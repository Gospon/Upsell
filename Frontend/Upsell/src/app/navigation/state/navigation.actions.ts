import { createAction, props } from '@ngrx/store';
import { Category } from 'src/app/products/types/category.type';

export const loadCategoriesSuccess = createAction(
  '[Navigation Component] Load Categories Success',
  props<{ categories: Category[] }>()
);

export const openAddNewListingDialog = createAction(
  '[Navigation Component] Open Add New Listing Dialog'
);
