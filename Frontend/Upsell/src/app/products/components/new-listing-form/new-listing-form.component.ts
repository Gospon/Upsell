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
      imageUpload: [[]],
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

  async submitListing() {
    if (this.productForm.valid) {
      await this.fileUpload.upload();
      this.productHttpService.addProducts(this.productForm.value).subscribe();
    } else {
      this.productForm.markAllAsTouched();
    }
  }

  async onImageUpload(event: any) {
    const uploadedImages = event.files;
    const base64Images: string[] = [];

    for (const image of uploadedImages) {
      const base64Image = await this.blobToBase64(image);
      base64Images.push(base64Image);
    }

    this.productForm.get('imageUpload')?.setValue(base64Images);
  }

  blobToBase64(blob: Blob): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onloadend = () => {
        if (typeof reader.result === 'string') {
          resolve(reader.result);
        } else {
          reject(new Error('Failed to convert Blob to Base64'));
        }
      };
      reader.onerror = () => {
        reject(new Error('Error reading Blob as Base64'));
      };
      reader.readAsDataURL(blob);
    });
  }
}
