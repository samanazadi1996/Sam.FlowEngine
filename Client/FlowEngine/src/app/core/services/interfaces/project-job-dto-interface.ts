import { PointInterface } from './point-interface';

export interface ProjectJobDtoInterface {

  id: number;
  projectId: number;
  className?: string;
  name?: string;
  position: PointInterface;
  jobParameters?: any;
  nextJob?: number[];

}
