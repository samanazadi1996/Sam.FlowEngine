import { ChangeDetectorRef, Component, HostListener, OnInit } from '@angular/core';
import { timer, Subscription } from 'rxjs';
import { AuthenticationService } from '../../core/services/authentication.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-main',
  standalone: false,
  templateUrl: './main.html',
  styleUrl: './main.scss',
})
export class Main implements OnInit {
  getUserAvtar() {
    var profileImage = this.authenticationService.getProfileImage()

    return String(profileImage).includes('.jpg') ?
      `${environment.serverUrl}/profile-images/${profileImage}` :
      profileImage
  }
  mobnavigationactive = false;
  lock = false;
  private subscription?: Subscription;
  profile?: any;

  constructor(private cdr: ChangeDetectorRef, private authenticationService: AuthenticationService) { }
  ngOnInit(): void {
    this.profile = this.authenticationService.getProfile()
  }

  @HostListener('document:click')
  onDocumentClick() {
    if (!this.lock) {
      this.mobnavigationactive = false;
      this.cdr.detectChanges();
    }
  }

  openmobnavigationactive() {
    this.mobnavigationactive = true;
    this.lock = true;

    this.subscription = timer(100).subscribe(() => {
      this.lock = false;
    });
  }

  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  logOut() {
    this.authenticationService.logout()
  }

}
