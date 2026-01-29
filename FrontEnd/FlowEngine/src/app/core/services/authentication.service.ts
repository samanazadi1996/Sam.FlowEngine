import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  private storagekey = 'profile';
  private profile: any;
  isLoggedIn = false;

  constructor(private router: Router) {
    var data = localStorage.getItem(this.storagekey);

    if (data) {
      this.profile = JSON.parse(data);
      this.isLoggedIn = true;
    }
  }

  login(model: Object) {
    console.log(model);

    var obj = Object(model);

    localStorage.setItem(
      this.storagekey,
      JSON.stringify(obj.data)
    );

    this.profile = obj.data;
    this.isLoggedIn = obj.success;
  }

  logout(redirect?: string | null) {
    localStorage.removeItem(this.storagekey);
    this.profile = null;
    this.isLoggedIn = false;
    this.router.navigate([redirect ?? '/']);
  }
  getProfileImage() {
    if (!this.profile) {
      this.logout()
      return '';
    }

    return this.profile.profileImage;
  }
  getUserName() {
    if (!this.profile) {
      this.logout()
      return '';
    }

    return this.profile?.userName;
  }
  getToken() {
    if (!this.profile) {
      this.logout()
      return '';
    }

    return this.profile.jwToken;
  }
  isInRole(role: string) {
    return this.profile.roles.includes(role);
  }

}
