import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-job-random-form',
  standalone: false,
  templateUrl: './job-random-form.html',
  styleUrl: './job-random-form.scss',
})
export class JobRandomForm {
  @Input() model: any;

}
