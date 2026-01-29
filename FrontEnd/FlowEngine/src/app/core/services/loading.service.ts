import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  private loadingSubject = new BehaviorSubject<boolean>(false);
  loading$ = this.loadingSubject.asObservable();

  private activeRequests = 0;
  private showTimeout: any;
  private hideTimeout: any;
  private readonly minVisibleTime = 500; // حداقل ۰.۵ ثانیه نمایش

  show() {
    this.activeRequests++;

    // اگر هنوز نمایش داده نشده، با تاخیر کوتاه ظاهرش کن
    if (this.activeRequests === 1) {
      clearTimeout(this.hideTimeout);
      this.showTimeout = setTimeout(() => {
        this.loadingSubject.next(true);
      }, 100);
    }
  }

  hide() {
    this.activeRequests = Math.max(0, this.activeRequests - 1);

    if (this.activeRequests === 0) {
      clearTimeout(this.showTimeout);

      // یه تاخیر کوچیک بذار تا چشم کاربر پرش حس نکنه
      this.hideTimeout = setTimeout(() => {
        this.loadingSubject.next(false);
      }, this.minVisibleTime);
    }
  }
}
