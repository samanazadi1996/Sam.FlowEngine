import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-job-timer-form',
  standalone: false,
  templateUrl: './job-timer-form.html',
  styleUrl: './job-timer-form.scss',
})
export class JobTimerForm {
  @Input() model: any;

}
