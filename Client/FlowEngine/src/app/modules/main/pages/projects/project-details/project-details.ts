import { Component, ElementRef, ViewChild, OnInit, ChangeDetectorRef } from '@angular/core';
import { GeneralService } from '../../../../../core/services/general.service';
import { ProjectService } from '../../../../../core/services/project.service';
import { ActivatedRoute, Router } from '@angular/router';
import * as joint from 'jointjs';
import { ProjectDtoInterface } from '../../../../../core/services/interfaces/project-dto-interface';
import { JobService } from '../../../../../core/services/job.service';
import { MatDialog } from '@angular/material/dialog';
import { UpdateJob } from './update-job/update-job';
import { CreateJob } from './create-job/create-job';

@Component({
  selector: 'app-project-details',
  standalone: false,
  templateUrl: './project-details.html',
  styleUrl: './project-details.scss',
})
export class ProjectDetails implements OnInit {
  data?: ProjectDtoInterface;
  projectName?: string;
  @ViewChild('paperContainer', { static: true })
  paperContainer!: ElementRef;

  graph!: joint.dia.Graph;
  paper!: joint.dia.Paper;

  constructor(private generalService: GeneralService,
    private projectService: ProjectService,
    private jobService: JobService,
    private dialog: MatDialog,
    private cdr: ChangeDetectorRef,
    private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.projectName = this.route.snapshot.paramMap.get('projectName') ?? '';
    this.loadData()
  }

  loadData() {
    this.projectService.getApiProjectGetUserProjectByName(this.projectName).subscribe(response => {
      if (this.generalService.isSuccess(response))
        this.data = response.data;
      this.renderFromData();
      this.cdr.detectChanges();

    })
  }

  ngAfterViewInit(): void {
    this.configureGraph()
  }

  configureGraph() {
    this.graph = new joint.dia.Graph();

    this.paper = new joint.dia.Paper({
      el: this.paperContainer.nativeElement,
      model: this.graph,
      width: '100%',
      height: '70rem',
      gridSize: 10,
      drawGrid: true,
      interactive: true
    });
    this.registerEvents();

  }

  renderFromData() {
    if (!this.data?.jobs) return;

    const elements: joint.dia.Element[] = [];
    const links: joint.dia.Link[] = [];

    const nodeMap = new Map<string, joint.dia.Element>();

    for (const job of this.data.jobs) {
      const rect = new joint.shapes.standard.Rectangle();

      rect.position(job.position.x, job.position.y);
      rect.resize(200, 70);

      rect.markup = [
        { tagName: 'rect', selector: 'body' },
        { tagName: 'text', selector: 'label' },
        { tagName: 'image', selector: 'icon' }   // 👈 image
      ];

      rect.attr({
        body: {
          fill: '#E3F2FD',
          stroke: '#1E88E5',
          rx: 8,
          ry: 8,
          cursor: 'pointer'
        },
        label: {
          text: job.name,
          fill: '#000',
          fontSize: 13,
          fontWeight: 'bold',
          refX: 10,
          refY: '50%',
          textAnchor: 'start',
          textVerticalAnchor: 'middle',
          cursor: 'pointer'
        },
        icon: {
          'xlink:href': `/icon/jobs/${job.className}.svg`,
          width: 32,
          height: 32,
          refX: '100%',
          refX2: -40,
          refY: '50%',
          refY2: -16,
          cursor: 'pointer'
        }
      });

      rect.prop('data', job);
      rect.addTo(this.graph);
      nodeMap.set(job.id + '', rect);

    }

    // 🔗 Links
    for (const job of this.data.jobs) {
      const fromNode = nodeMap.get(job.id + '');
      if (!fromNode) continue;
      var listnextJobj = job.nextJob ?? [];

      if (job.jobParameters["IfTrue"]) listnextJobj.push(job.jobParameters["IfTrue"]);

      if (job.jobParameters["IfFalse"]) listnextJobj?.push(job.jobParameters["IfFalse"]);
      if (job.jobParameters["ForTask"]) listnextJobj?.push(job.jobParameters["ForTask"]);


      listnextJobj?.forEach(next => {
        const toNode = nodeMap.get(next + '');
        if (!toNode) return;

        const link = new joint.shapes.standard.Link();
        link.source(fromNode);
        link.target(toNode);
        link.addTo(this.graph);

        links.push(link);
      });
    }
  }

  registerEvents() {
    this.paper.on('element:pointerclick', (elementView) => {
      const element = elementView as any;
      this.onNodeClick(element.model);
    });
    this.paper.on('element:pointerup', (elementView) => {
      const element = elementView as any;
      this.onNodeDrop(element.model);
    });

  }
  createJob() {
    const dialogRef = this.dialog.open(CreateJob, {
      width: '400px',
      panelClass: 'no-dialog-surface',
      data: {
        projectId: this.data?.id,
        allJobs: this.data?.jobs?.map(x => x.className ) ?? []
      },
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      this.configureGraph()
      this.loadData()
    });

  }
  onNodeClick(element: any) {
    const job = element.prop('data');

    const dialogRef = this.dialog.open(UpdateJob, {
      width: '1200px',
      maxWidth: '1200px',
      panelClass: 'no-dialog-surface',
      data: { ...job },
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      this.configureGraph()
      this.loadData()
    });
  }

  onNodeDrop(element: any) {
    const job = element.prop('data');

    const position = element.position();

    var hasChange = job.position.x != position.x || job.position.y != position.y;
    if (hasChange) {
      const jobId = element.prop('data').id;
      this.jobService.putApiJobUpdatePositionJob({ jobId: jobId, x: position.x, y: position.y })
        .subscribe(response => {
          if (this.generalService.isSuccess(response))
            element.prop('data').position = {
              x: position.x,
              y: position.y
            }
        })
    }
  }
  stopStart() {

    const action$ = this.data!.started
      ? this.projectService.postApiProjectStopProject({ projectName: this.data!.projectName })
      : this.projectService.postApiProjectStartProject({ projectName: this.data!.projectName });

    action$.subscribe(response => {
      if (this.generalService.isSuccess(response)) {
        this.data!.started = !this.data!.started
        this.cdr.detectChanges();
      }
    });

  }

}
