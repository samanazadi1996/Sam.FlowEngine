import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../../../../core/services/account.service';
import { AuthenticationService } from '../../../../core/services/authentication.service';
import { GeneralService } from '../../../../core/services/general.service';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {

  loginForm!: FormGroup;
  submitted = false;
  loading = false;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private accountService: AccountService,
    private authenticationService: AuthenticationService,
    private generalService: GeneralService
  ) {
    this.generalService.setTitle('ورود')
  }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
    });
  }


  public login(): void {

    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    const loginData = this.loginForm.getRawValue()

    this.accountService.postApiAccountAuthenticate(loginData)
      .subscribe({
        next: (response) => {
          if (response.success) {
            this.authenticationService.login(response);
            this.router.navigate(['/']);
          } else {
            this.errorMessage = 'نام کاربری یا رمز عبور اشتباه است';
          }
          this.loading = false;
        },
        error: (err) => {
          this.errorMessage = 'خطا در ارتباط با سرور';
          this.loading = false;
        }
      });
  }

}
