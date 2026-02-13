import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { GeneralService } from '../../../../../core/services/general.service';
import { ProjectService } from '../../../../../core/services/project.service';
import { CreateProjectCommandInterface } from '../../../../../core/services/interfaces/create-project-command-interface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-project',
  standalone: false,
  templateUrl: './create-project.html',
  styleUrl: './create-project.scss',
})
export class CreateProject {
  model: CreateProjectCommandInterface = { projectName: "Test-Project" }

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<CreateProject>,
    private projectService: ProjectService,
    private router: Router,
    private generalService: GeneralService) {
    this.model.projectName = `Test_Project_${this.data.countProjects + 1}`
  }

  close() {
    this.dialogRef.close();
  }

  save() {
    this.projectService.postApiProjectCreateProject(this.model).subscribe(response => {
      if (this.generalService.isSuccess(response)) {
        this.router.navigate(['main', 'projects', this.model.projectName]);
      }
    })
    this.dialogRef.close();
  }

}
