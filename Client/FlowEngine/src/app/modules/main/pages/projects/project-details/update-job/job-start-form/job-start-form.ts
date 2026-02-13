import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-job-start-form',
  standalone: false,
  templateUrl: './job-start-form.html',
  styleUrl: './job-start-form.scss',
})
export class JobStartForm {
  @Input() model: any;
}
