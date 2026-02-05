import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-job-sql-server-form',
  standalone: false,
  templateUrl: './job-sql-server-form.html',
  styleUrl: './job-sql-server-form.scss',
})
export class JobSqlServerForm {
  @Input() model: any;

}
