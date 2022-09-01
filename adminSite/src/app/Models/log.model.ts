export class Log {
    _id: number;
    logMessage: string;
    type: string;
    createDate: Date;
}
export class LogReturn{
    status:string;
    logs:Log[]
}