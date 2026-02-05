import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-job-sleep-form',
  standalone: false,
  templateUrl: './job-sleep-form.html',
  styleUrl: './job-sleep-form.scss',
})
export class JobSleepForm {
  @Input() model: any;

}
