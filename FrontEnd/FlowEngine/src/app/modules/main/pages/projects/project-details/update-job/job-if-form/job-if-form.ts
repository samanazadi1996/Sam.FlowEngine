import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { GeneralService } from '../../../../../../../core/services/general.service';
import { JobService } from '../../../../../../../core/services/job.service';
import { IdTitleDtoInterface } from '../../../../../../../core/services/interfaces/id-title-dto-interface';

@Component({
  selector: 'app-job-if-form',
  standalone: false,
  templateUrl: './job-if-form.html',
  styleUrl: './job-if-form.scss',
})
export class JobIfForm implements OnInit {
  @Input() model: any;

  projectJobs?: IdTitleDtoInterface[];

  constructor(
    private generalService: GeneralService,
    private jobService: JobService,
    private cdr: ChangeDetectorRef,

  ) {
  }

  ngOnInit(): void {
    this.jobService.getApiJobGetAllJobsByProjectId(this.model.projectId)
      .subscribe(response => {
        if (this.generalService.isSuccess(response)) {
          this.projectJobs = response.data?.filter(p => p.id != this.model.id);
          this.cdr.detectChanges();
        }
      })
  }

}
