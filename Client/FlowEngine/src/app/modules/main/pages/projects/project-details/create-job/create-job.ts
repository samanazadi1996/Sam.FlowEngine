import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { GeneralService } from '../../../../../../core/services/general.service';
import { CreateProject } from '../../create-project/create-project';
import { JobService } from '../../../../../../core/services/job.service';

@Component({
  selector: 'app-create-job',
  standalone: false,
  templateUrl: './create-job.html',
  styleUrl: './create-job.scss',
})
export class CreateJob implements OnInit {
  allJobs?: any[] = [];
  filteredJobs?: any[] = [];
  searchText: string = '';
  jobName: string = '';

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,

    private dialogRef: MatDialogRef<CreateProject>,
    private jobService: JobService,
    private cdr: ChangeDetectorRef,
    private generalService: GeneralService) {
  }

  ngOnInit(): void {
    this.jobService.getApiJobGetAllJobs().subscribe(response => {
      if (this.generalService.isSuccess(response)) {
        response.data!.forEach(item => {
          var nameSplited = item.className?.split(".");
          const jobName = nameSplited![nameSplited!.length - 1].replace('Job_', '');

          this.allJobs!.push({
            icon: `/icon/jobs/${item.className}.svg`,
            name: jobName,
            originalName: item.className,
            displayName: jobName,
            selected: false
          });
        });

        this.filteredJobs = [...this.allJobs!];
        this.cdr.detectChanges();
      }
    })
  }

  onSearchChange(): void {
    if (!this.allJobs) return;

    if (!this.searchText.trim()) {
      this.filteredJobs = [...this.allJobs];
      return;
    }

    const searchTerm = this.searchText.toLowerCase();
    this.filteredJobs = this.allJobs.filter(job =>
      job.name.toLowerCase().includes(searchTerm) ||
      job.originalName.toLowerCase().includes(searchTerm)
    );
  }

  clearSearch(): void {
    this.searchText = '';
    this.onSearchChange();
  }

  close() {
    this.dialogRef.close();
  }

  save() {
    var selectedJob = this.allJobs?.filter(p => p.selected);
    if (selectedJob?.length != 1) return;

    this.jobService.postApiJobCreateJob({
      projectId: this.data.projectId,
      className: selectedJob[0].originalName,
      name: this.jobName
    }).subscribe(response => {
      if (this.generalService.isSuccess(response)) {
        this.dialogRef.close();
      }
    })
  }

  selectJob(job: any): void {
    var uniqueNumber = ((this.data.allJobs as any[]).filter(p => p == job.originalName).length) + 1

    this.jobName = `Job_${job.name}_${uniqueNumber}`

    this.allJobs?.forEach(element => {
      element.selected = false
    });
    job.selected = true
  }
}