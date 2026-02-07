import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-job-schedule-form',
  standalone: false,
  templateUrl: './job-schedule-form.html',
  styleUrl: './job-schedule-form.scss',
})
export class JobScheduleForm {
  @Input() model: any;

}
