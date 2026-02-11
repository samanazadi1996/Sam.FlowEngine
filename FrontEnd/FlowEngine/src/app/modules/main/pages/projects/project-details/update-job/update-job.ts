import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { JobService } from '../../../../../../core/services/job.service';
import { GeneralService } from '../../../../../../core/services/general.service';
import { ProjectJobDtoInterface } from '../../../../../../core/services/interfaces/project-job-dto-interface';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IdTitleDtoInterface } from '../../../../../../core/services/interfaces/id-title-dto-interface';
import { ProjectService } from '../../../../../../core/services/project.service';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';

@Component({
  selector: 'app-update-job',
  standalone: false,
  templateUrl: './update-job.html',
  styleUrl: './update-job.scss',
})
export class UpdateJob implements OnInit {

  treeControl = new NestedTreeControl<TreeNode>(node => node.children);
  dataSource = new MatTreeNestedDataSource<TreeNode>();

  model?: ProjectJobDtoInterface;
  projectJobs?: IdTitleDtoInterface[];
  dataTemplate: any[] = [];
  constructor(
    private dialogRef: MatDialogRef<UpdateJob>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private cdr: ChangeDetectorRef,
    private jobService: JobService,
    private projectService: ProjectService,
    private generalService: GeneralService) {
  }

  hasChild = (_: number, node: TreeNode) =>
    !!node.children && node.children.length > 0;

  ngOnInit(): void {
    this.loadData()
  }

  loadData() {
    this.projectService.getApiProjectGetProjectDataTemplate(this.data.projectId)
      .subscribe(response => {
        if (this.generalService.isSuccess(response)) {
          if (response.data) {
            for (const [key, value] of Object.entries(response.data)) {
              this.dataTemplate.push({ key: key, value: String(value) });
            }
          }
          this.loadDataTemplate()
          this.cdr.detectChanges();
        }
      })
    this.jobService.getApiJobGetAllJobsByProjectId(this.data.projectId)
      .subscribe(response => {
        if (this.generalService.isSuccess(response)) {
          this.projectJobs = response.data?.filter(p => p.id != this.data.id);
          this.cdr.detectChanges();
        }
      })
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
    const parameters: { [key: string]: string | null } = {};

    if (this.model?.jobParameters) {
      for (const [key, value] of Object.entries(this.model.jobParameters)) {
        parameters[key] = value ? String(value) : null;
      }
    }

    this.jobService.putApiJobUpdateJob({
      id: this.model!.id,
      name: this.model!.name,
      parameters: parameters,
      nextJob: this.model!.nextJob
    }).subscribe(response => {
      if (this.generalService.isSuccess(response)) {
        this.dialogRef.close(this.data);
      }
    });
  }

  loadDataTemplate() {
    this.dataSource.data = []
    this.dataTemplate.forEach(item => {
      this.dataSource.data.push({
        name: "${" + item.key + "}",
        children: this.getChild(item.value, [item.key])
      })
    });
  }



  getChild(value: any, parentPath: string[] = []): TreeNode[] | undefined {
    if (typeof value === 'string') {
      try {
        value = JSON.parse(value);
      } catch {
        return undefined;
      }
    }
    if (value == null) {
      return undefined;
    }

    if (typeof value === 'object' && !Array.isArray(value)) {
      return Object.entries(value).map(([key, val]) => {
        const newPath = [...parentPath, key];
        return {
          name: "${" + newPath.join('.') + "}",
          children: this.getChild(val, newPath)
        };
      });
    }

    if (Array.isArray(value)) {
      return [
        {
          name: "${" + [...parentPath, "Length()"].join('.') + "}",
          children: []
        },
        ...value.map((item, index) => {
          const newPath = [...parentPath, `[${index}]`];
          return {
            name: ("${" + newPath.join('.') + "}").replaceAll(".[", "["),
            children: this.getChild(item, newPath)
          };
        })]
    }

    return undefined;
  }



  delete() {
    this.jobService.deleteApiJobDeleteJob(this.model!.id).subscribe(response => {
      if (this.generalService.isSuccess(response)) {
        this.dialogRef.close(this.data);
      }
    });
  }




}

interface TreeNode {
  name: string;
  children?: TreeNode[];
}

