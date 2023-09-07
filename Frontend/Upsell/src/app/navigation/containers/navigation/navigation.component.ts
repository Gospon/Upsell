import { Component, OnInit } from '@angular/core';
import { NavigationHttpService } from '../../services/navigation-http-service';
import { Subject, map, tap } from 'rxjs';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { Store } from '@ngrx/store';
import {
  loadCategoriesSuccess,
  openAddNewListingDialog,
} from '../../state/navigation.actions';
import { Category } from 'src/app/products/types/category.type';

@Component({
  selector: 'navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.sass'],
})
export class NavigationComponent implements OnInit {
  allCategories: Category[];
  focusedCategories: Category[] = [];
  currentCategoriesSubject = new Subject<Category[]>();
  currentCategories$ = this.currentCategoriesSubject.asObservable();

  constructor(
    private navigationHttpService: NavigationHttpService,
    private sanitizer: DomSanitizer,
    private store: Store
  ) {}

  ngOnInit() {
    this.navigationHttpService
      .getCategories()
      .pipe(
        map((response) => response.data),
        tap((data) => {
          this.allCategories = data;
          const parentCategories = data.filter(
            (category) => category.parentCategoryId === null
          );
          this.currentCategoriesSubject.next(parentCategories);
          this.store.dispatch(loadCategoriesSuccess({ categories: data }));
        })
      )
      .subscribe();
  }

  sanitizeSVG(svgString: string): SafeHtml {
    return this.sanitizer.bypassSecurityTrustHtml(svgString);
  }

  handleCategoryClicked(carousel: any, category: Category) {
    var currentCategories: Category[] = [];
    if (this.categoryFocused(category)) {
      this.removeAllCategoriesFromThisOn(category);
      currentCategories = this.getParentCategories(category);
    } else {
      currentCategories = this.getChildCategories(category);
      this.addCategoryToFocusedCategories(category);
    }

    this.currentCategoriesSubject.next([
      ...this.focusedCategories,
      ...currentCategories,
    ]);

    carousel.step(1, 0);
  }

  removeAllCategoriesFromThisOn(category: Category) {
    const index = this.focusedCategories.findIndex((c) => c.id === category.id);
    if (index >= 0) {
      this.focusedCategories.splice(index);
    }
  }

  addCategoryToFocusedCategories(category: Category) {
    this.focusedCategories = [...this.focusedCategories, category];
  }

  getParentCategories(category: Category) {
    return this.allCategories.filter(
      (c) => c.parentCategoryId === category.parentCategoryId
    );
  }

  getChildCategories(category: Category) {
    return this.allCategories.filter((c) => c.parentCategoryId === category.id);
  }

  categoryFocused(category: Category): boolean {
    return this.focusedCategories.includes(category);
  }

  handleAddNewListingClick() {
    this.store.dispatch(openAddNewListingDialog());
  }
}
