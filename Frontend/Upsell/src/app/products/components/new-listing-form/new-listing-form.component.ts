import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Category } from '../../types/category.type';
import { SelectItem } from 'primeng/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FileUpload } from 'primeng/fileupload';
import { ProductHttpService } from '../../services/product-http-service';
@Component({
  selector: 'new-listing-form',
  templateUrl: './new-listing-form.component.html',
  styleUrls: ['./new-listing-form.component.sass'],
})
export class NewListingFormComponent implements OnInit {
  @ViewChild('fileUpload') fileUpload: FileUpload;

  productForm: FormGroup;
  currency = 'USD';
  currencySymbol = '$';
  conditions: SelectItem[] = [
    { label: 'New', value: 'new' },
    { label: 'Used', value: 'used' },
  ];

  galleryImages: any[] = [];

  base64Images: string[] = [];

  @Input()
  categories: Category[];

  get categoriesForDropdown() {
    return this.categories?.map((category) => ({
      label: category.name,
      value: category.id,
    }));
  }

  constructor(
    private formBuilder: FormBuilder,
    private productHttpService: ProductHttpService
  ) {}

  ngOnInit(): void {
    console.log(this.categories);
    this.buildForm();
  }

  buildForm() {
    this.productForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      category: [null, Validators.required],
      condition: [null, Validators.required],
      price: [
        null,
        [Validators.required, Validators.pattern(/^\d+(\.\d{1,2})?$/)],
      ],
      images: [[]],
    });
  }

  toggleCurrency() {
    if (this.currency === 'USD') {
      this.currency = 'EUR';
      this.currencySymbol = '€';
    } else {
      this.currency = 'USD';
      this.currencySymbol = '$';
    }
  }

  submitListing() {
    if (this.productForm.valid) {
      this.productHttpService.addProducts(this.productForm.value).subscribe();
    } else {
      // this.productForm.markAllAsTouched();
    }
  }

  onImageSelect(event: any) {
    const fileList = [...event.files];
    const promises = fileList.map((file: any) => {
      return new Promise<string>((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
          resolve(reader.result as string);
        };
        reader.onerror = (error) => {
          reject(error);
        };
      });
    });

    Promise.all(promises)
      .then((base64Images) => {
        this.productForm.get('images')?.setValue(base64Images);
        console.log(this.productForm.get('images'));
      })
      .catch((error) => {
        console.error('Error reading files:', error);
      });
  }
}
