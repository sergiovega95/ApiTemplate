import { Task } from "./Task";

export interface GetAllTaskResponse {
    statusCode: number;
    data: Task[];
}

