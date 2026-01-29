import { Component, OnInit } from '@angular/core';
import { GeneralService } from '../../../../../core/services/general.service';
import { ProjectService } from '../../../../../core/services/project.service';
import { ProjectInterface } from '../../../../../core/services/interfaces/project-interface';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-project-detail',
  standalone: false,
  templateUrl: './project-detail.html',
  styleUrl: './project-detail.scss',
})
export class ProjectDetail implements OnInit {
  data?: ProjectInterface;
  projectName?: string;

  constructor(private generalService: GeneralService, private projectService: ProjectService, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.projectName = this.route.snapshot.paramMap.get('projectName')??'';
    this.loadData()
  }

  loadData() {
    this.projectService.getApiProjectGetUserProjectByName(this.projectName).subscribe(response => {
      if (this.generalService.isSuccess(response))
        this.data = response.data;
    })
  }

}
