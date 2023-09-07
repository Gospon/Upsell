import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NewListingDialog } from './containers/new-listing-dialog.component';
import { DialogModule } from 'primeng/dialog';
import { StoreModule } from '@ngrx/store';
import { productsReducer } from './store/products.reducer';
import { NewListingFormComponent } from './components/new-listing-form/new-listing-form.component';
import { InputTextModule } from 'primeng/inputtext';
import { CheckboxModule } from 'primeng/checkbox';
import { RadioButtonModule } from 'primeng/radiobutton';
import { DropdownModule } from 'primeng/dropdown';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ButtonModule } from 'primeng/button';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { GalleriaModule } from 'primeng/galleria';
import { FileUploadModule } from 'primeng/fileupload';
import { ProductHttpService } from './services/product-http-service';
import { HttpClientModule } from '@angular/common/http';
import { AuthenticationFormComponent } from './components/authentication-form/authentication-form.component';
import { AuthenticationHttpService } from './services/authentication-http-service';

@NgModule({
  declarations: [
    NewListingDialog,
    NewListingFormComponent,
    AuthenticationFormComponent,
  ],
  imports: [
    BrowserModule,
    DialogModule,
    InputTextModule,
    CheckboxModule,
    RadioButtonModule,
    DropdownModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ButtonModule,
    InputTextareaModule,
    GalleriaModule,
    FileUploadModule,
    HttpClientModule,
    StoreModule.forFeature('products', productsReducer),
  ],
  exports: [NewListingDialog],
  providers: [ProductHttpService, AuthenticationHttpService],
})
export class ProductsModule {}
