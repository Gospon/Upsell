import { createReducer, on } from '@ngrx/store';
import {
  loadCategoriesSuccess,
  openAddNewListingDialog,
} from 'src/app/navigation/state/navigation.actions';
import { closeAddNewListingDialog } from './products.actions';
import { Category } from '../types/category.type';

export interface ProductsState {
  showAddNewListingDialog: boolean;
  categories: Category[];
  authenticate: boolean;
}

export const initialState: ProductsState = {
  showAddNewListingDialog: false,
  categories: [],
  authenticate: false,
};

export const productsReducer = createReducer(
  initialState,
  on(openAddNewListingDialog, (state) => {
    const jwtToken = localStorage.getItem('jwtToken');
    if (!!jwtToken) {
      return {
        ...state,
        authenticate: false,
        showAddNewListingDialog: true,
      };
    } else {
      return { ...state, showAddNewListingDialog: false, authenticate: true };
    }
  }),
  on(closeAddNewListingDialog, (state) => {
    const jwtToken = localStorage.getItem('jwtToken');
    if (!!jwtToken) {
      return {
        ...state,
        showAddNewListingDialog: false,
      };
    } else {
      return { ...state, authenticate: false };
    }
  }),
  on(loadCategoriesSuccess, (state, { categories }) => ({
    ...state,
    categories: categories,
  }))
);
