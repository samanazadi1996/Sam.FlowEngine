import { NestedTreeControl } from '@angular/cdk/tree';
import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ProjectService } from '../../../../../../core/services/project.service';
import { GeneralService } from '../../../../../../core/services/general.service';

@Component({
  selector: 'app-data-template',
  standalone: false,
  templateUrl: './data-template.html',
  styleUrl: './data-template.scss',
})
export class DataTemplate implements OnInit {

  @Input() projectId?: number;

  treeControl = new NestedTreeControl<TreeNode>(node => node.children);
  dataSource = new MatTreeNestedDataSource<TreeNode>();

  constructor(private projectService: ProjectService,
    private generalService: GeneralService,
    private cdr: ChangeDetectorRef,
  ) {
  }

  ngOnInit(): void {
    this.loadData()
  }

  loadData() {
    this.projectService.getApiProjectGetProjectDataTemplate(this.projectId)
      .subscribe(response => {
        if (this.generalService.isSuccess(response)) {
          if (response.data) {
            const treeData: TreeNode[] = [];

            for (const [key, value] of Object.entries(response.data)) {
              var val = String(value);
              treeData.push({
                name: "${" + key + "}",
                children: this.getChild(val, [key])
              });
            }

            console.log(treeData);

            this.dataSource.data = treeData;
          }

          this.cdr.detectChanges();
        }
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


  hasChild = (_: number, node: TreeNode) =>
    !!node.children && node.children.length > 0;

}
interface TreeNode {
  name: string;
  children?: TreeNode[];
}

