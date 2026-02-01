import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { JobService } from '../../../../../../core/services/job.service';
import { GeneralService } from '../../../../../../core/services/general.service';
import { ProjectJobDtoInterface } from '../../../../../../core/services/interfaces/project-job-dto-interface';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-update-job',
  standalone: false,
  templateUrl: './update-job.html',
  styleUrl: './update-job.scss',
})
export class UpdateJob implements OnInit {
  model?: ProjectJobDtoInterface;

  constructor(
    private dialogRef: MatDialogRef<UpdateJob>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private cdr: ChangeDetectorRef,
    private jobService: JobService, private generalService: GeneralService) {
  }

  ngOnInit(): void {
    this.loadData()
  }

  loadData() {
    this.jobService.getApiJobGetJobById(this.data.id).subscribe(response => {
      if (this.generalService.isSuccess(response)) {
        this.model = response.data;
        this.cdr.detectChanges();
      }
    })
  }
  hasParameter(arg: string) {
    return (this.model!.jobParameters ?? [])[arg] != undefined
  }

  close() {
    this.dialogRef.close();
  }

  save() {
  const parameters: { [key: string]: string } = {};
  
  if (this.model?.jobParameters) {
    for (const [key, value] of Object.entries(this.model.jobParameters)) {
      parameters[key] = String(value);
    }
  }

  this.jobService.putApiJobUpdateJob({
    id: this.model!.id,
    name: this.model!.name,
    parameters: parameters
  }).subscribe(response => {
    if (this.generalService.isSuccess(response)) {
      this.dialogRef.close(this.data);
    }
  });
  }
}
