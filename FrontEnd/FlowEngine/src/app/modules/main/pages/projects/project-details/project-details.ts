import { Component, AfterViewInit, ElementRef, ViewChild, OnInit } from '@angular/core';
import { GeneralService } from '../../../../../core/services/general.service';
import { ProjectService } from '../../../../../core/services/project.service';
import { ProjectInterface } from '../../../../../core/services/interfaces/project-interface';
import { ActivatedRoute, Router } from '@angular/router';
import * as joint from 'jointjs';

@Component({
  selector: 'app-project-details',
  standalone: false,
  templateUrl: './project-details.html',
  styleUrl: './project-details.scss',
})
export class ProjectDetails implements OnInit {
  data?: ProjectInterface;
  projectName?: string;
  @ViewChild('paperContainer', { static: true })
  paperContainer!: ElementRef;

  graph!: joint.dia.Graph;
  paper!: joint.dia.Paper;

  constructor(private generalService: GeneralService, private projectService: ProjectService, private route: ActivatedRoute) {
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

    })
  }


  ngAfterViewInit(): void {
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

    let x = 50;
    let y = 50;

    // 🔷 Nodes
    for (const job of this.data.jobs) {
      const rect = new joint.shapes.standard.Rectangle();

    rect.position(x, y);
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

    rect.prop('jobName', job.name);
    rect.addTo(this.graph);
    nodeMap.set(job.name!, rect);

    y += 100;
    }

    // 🔗 Links
    for (const job of this.data.jobs) {
      const fromNode = nodeMap.get(job.name!);
      if (!fromNode) continue;
      var listnextJobj = job.nextJob ?? [];

      if (job.jobParameters["True"]) listnextJobj.push(job.jobParameters["True"]);

      if (job.jobParameters["False"]) listnextJobj?.push(job.jobParameters["False"]);


      listnextJobj?.forEach(next => {
        const toNode = nodeMap.get(next);
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
}
onNodeClick(element: any) {
  console.log('Clicked element:', element);

  const job = element.prop('data');
  console.log(job);

}

}
