import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from '../Interfaces/Task';
import { catchError, retry } from 'rxjs/operators';

@Injectable()
export class TodoApiService {
    
  private baseURL: string = 'https://ztj7tcjien.us-east-1.awsapprunner.com';

  constructor(private httpClient: HttpClient) {}

  async GetAuthToken(name: string, email: string): Promise<Observable<any>> {
    const body = {
      name: 'sergio vega',
      email: 'sergio@gmail.com',
    };

    const request = this.httpClient.post(
      `${this.baseURL}/api/Authentication/token`,
      body
    );

    return request;
  }

  async GetAllTask() {}

  async CreateTask(taskDescription: string) {}

  async DeleTask(taskId: number) {}
}
