import { ChangeDetectorRef, Component, HostListener } from '@angular/core';
import { timer, Subscription } from 'rxjs';

@Component({
  selector: 'app-main',
  standalone: false,
  templateUrl: './main.html',
  styleUrl: './main.scss',
})
export class Main {
  mobnavigationactive = false;
  lock = false;
  private subscription?: Subscription;

  constructor(private cdr: ChangeDetectorRef) {}

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
}
