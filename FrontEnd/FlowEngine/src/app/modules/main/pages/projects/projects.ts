import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ProjectService } from '../../../../core/services/project.service';
import { GeneralService } from '../../../../core/services/general.service';
import { GetUserProjectsResponseInterface } from '../../../../core/services/interfaces/get-user-projects-response-interface';

@Component({
  selector: 'app-projects',
  standalone: false,
  templateUrl: './projects.html',
  styleUrl: './projects.scss',
})
export class Projects implements OnInit {
  data?: GetUserProjectsResponseInterface[];

  constructor(private projectService: ProjectService, private generalService: GeneralService,private cdr: ChangeDetectorRef) {

  }
  ngOnInit(): void {
    this.loadData();
  }
  loadData() {
    this.projectService.getApiProjectGetUserProjects()
      .subscribe(response => {

        if (this.generalService.isSuccess(response)) {
          this.data = response.data
            this.cdr.detectChanges();
        }

      })

  }
stopStart(item: GetUserProjectsResponseInterface) {
  const action$ = item.started
    ? this.projectService.postApiProjectStopProject({ projectName: item.projectName })
    : this.projectService.postApiProjectStartProject({ projectName: item.projectName });

  action$.subscribe(response => {
    if (this.generalService.isSuccess(response)) {
      this.loadData();
    }
  });
}
}
