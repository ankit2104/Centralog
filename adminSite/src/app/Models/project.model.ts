export class Project {
    id: string;
    name: string;
    created: Date;
    updated: Date;
    logCount: number;
    user_id: string;
}
export class ProjectReturn {
    status: string;
    projectDetails: Project[]
}