import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationHttpService } from '../../services/authentication-http-service';
import { openAddNewListingDialog } from 'src/app/navigation/state/navigation.actions';
import { Store } from '@ngrx/store';

@Component({
  selector: 'authentication-form',
  templateUrl: './authentication-form.component.html',
  styleUrls: ['./authentication-form.component.sass'],
})
export class AuthenticationFormComponent implements OnInit {
  authenticationForm: FormGroup;
  isRegistering = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthenticationHttpService,
    private store: Store
  ) {}

  ngOnInit(): void {
    this.buildForm();
  }

  buildForm() {
    this.authenticationForm = this.fb.group({
      firstName: [''],
      lastName: [''],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  toggleRegisterMode() {
    this.authenticationForm.reset();
    this.isRegistering = !this.isRegistering;
  }

  onSubmit() {
    if (this.isRegistering) {
      this.authService.registerUser(this.authenticationForm.value).subscribe(
        (response) => {
          if (response.success) {
            const jwtToken = response.data;
            localStorage.setItem('jwtToken', jwtToken);
            this.store.dispatch(openAddNewListingDialog());
          } else {
            console.error('Registration error:', response.errorMessage);
          }
        },
        (error) => {
          console.error('Registration error:', error);
        }
      );
    } else {
      this.authService.loginUser(this.authenticationForm.value).subscribe(
        (response) => {
          if (response.success) {
            const jwtToken = response.data;
            localStorage.setItem('jwtToken', jwtToken);
            this.store.dispatch(openAddNewListingDialog());
          } else {
            console.error('Login error:', response.errorMessage);
          }
        },
        (error) => {
          console.error('Login error:', error);
        }
      );
    }
  }
}
