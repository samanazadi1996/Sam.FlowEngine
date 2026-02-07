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
  headers: any[] = []

  @Input() model: any;

  ngOnInit(): void {
    this.headers = (JSON.parse(this.model.jobParameters['Headers']) as any[])
      .map(x => ({ key: x.Key, value: x.Value, readonly: true }))

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
    this.model.jobParameters['Headers'] = JSON.stringify(this.headers.map(x => ({ Key: x.key, Value: x.value })))
  }
}
