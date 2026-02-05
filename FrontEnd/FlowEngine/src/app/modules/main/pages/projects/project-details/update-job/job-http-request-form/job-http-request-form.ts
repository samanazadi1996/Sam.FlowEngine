import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-job-http-request-form',
  standalone: false,
  templateUrl: './job-http-request-form.html',
  styleUrl: './job-http-request-form.scss',
})
export class JobHttpRequestForm {
  httpMethods = ['Get', 'Post', 'Put', 'Delete', 'Patch', 'Options', 'Head', 'Query', 'Connect', 'Trace',]

  @Input() model: any;

}
