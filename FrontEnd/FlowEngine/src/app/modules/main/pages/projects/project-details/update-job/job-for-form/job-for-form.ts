import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-job-for-form',
  standalone: false,
  templateUrl: './job-for-form.html',
  styleUrl: './job-for-form.scss',
})
export class JobForForm {
  @Input() model: any;

}
