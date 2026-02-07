import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { readonly } from '@angular/forms/signals';

@Component({
  selector: 'app-job-http-request-form',
  standalone: false,
  templateUrl: './job-http-request-form.html',
  styleUrl: './job-http-request-form.scss',
})
export class JobHttpRequestForm implements OnInit {
  httpMethods = ['Get', 'Post', 'Put', 'Delete', 'Patch', 'Options', 'Head', 'Query', 'Connect', 'Trace',]
  headers: { key: string; value: string; readonly: boolean }[] = [];

  @Input() model: any;

  ngOnInit(): void {
    this.loadHeadersFromDictionary();
  }

  private loadHeadersFromDictionary() {
    const headersDict = JSON.parse(this.model.jobParameters['Headers'] ?? '{}');

    // تبدیل دیکشنری به آرایه برای نمایش در رابط کاربری
    this.headers = Object.entries(headersDict).map(([key, value]) => ({
      key,
      value: value as string,
      readonly: true
    }));
  }

  addHeader() {
    this.headers.push({ key: '', value: '', readonly: false })
    this.onchange()
  }

  deleteHeader(item: any) {
    this.headers = this.headers.filter(p => p != item)
    this.onchange()

  }
  editHeader(item: any) {
    item.readonly = !item.readonly
    this.onchange()
  }

  onchange() {
    const headersDict: { [key: string]: string } = {};

    this.headers.forEach(header => {
      if (header.key.trim()) {
        headersDict[header.key] = header.value;
      }
    });

    this.model.jobParameters['Headers'] = JSON.stringify(headersDict);
  }
}
