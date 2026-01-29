import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing-module';
import { SharedModule } from '../../core/shared/shared.module';
import { Login } from './pages/login/login';


@NgModule({
  declarations: [
    Login
  ],
  imports: [
    AccountRoutingModule,
    SharedModule
  ]
})
export class AccountModule { }
