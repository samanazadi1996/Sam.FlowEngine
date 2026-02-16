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
                children: this.getChild(val, [key]),
                value: String(val)
              });
            }

            console.log(treeData);

            this.dataSource.data = treeData;
          }

          this.cdr.detectChanges();
        }
      });
  }
  private getValue(data: any) {
    try {
      return JSON.stringify(data)
    } catch (error) { }
    
    try {
      return String(data);
    } catch (error) { }

    return '';
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
          name: ("${" + newPath.join('.') + "}").replaceAll(".[", "["),
          children: this.getChild(val, newPath),
          value: this.getValue(val)
        };
      });
    }

    if (Array.isArray(value)) {
      return [
        {
          name: "${" + [...parentPath, "Length()"].join('.') + "}",
          children: [],
          value: this.getValue(value?.length)
        },
        ...value.map((item, index) => {
          const newPath = [...parentPath, `[${index}]`];
          return {
            name: ("${" + newPath.join('.') + "}").replaceAll(".[", "["),
            children: this.getChild(item, newPath),
            value: this.getValue(value)
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
  value?: string;
  children?: TreeNode[];
}

