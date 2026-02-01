import { PointInterface } from './point-interface';
import { ProjectInterface } from './project-interface';

export interface ProjectJobInterface {

  id: number;
  className?: string;
  name?: string;
  jobParameters?: any;
  nextJob?: number[];
  projectId: number;
  position: PointInterface;
  project: ProjectInterface;

}
