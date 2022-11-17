import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from '../Interfaces/Task';
import {GetAllTaskResponse} from '../Interfaces/GetAllTaskResponse';

@Injectable({
  providedIn: 'root',
})
export class TodoApiService {
  private baseURL: string = 'https://ztj7tcjien.us-east-1.awsapprunner.com';

  constructor(private httpClient: HttpClient) {}

  GetAuthToken(name: string, email: string): Observable<any> {
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

  GetAllTask(): Observable<GetAllTaskResponse> {
    const request = this.httpClient.get<GetAllTaskResponse>(`${this.baseURL}/api/Task`,{headers:new HttpHeaders({'Authorization':`Bearer ${localStorage.getItem('jwt')}`})});
    return request;
  }

  CreateTask(taskDescription: string): Observable<any> {
    const body = {
      taskDescription: taskDescription,
    };

    const request = this.httpClient.post(`${this.baseURL}/api/Task`, body,{headers:new HttpHeaders({'Authorization':`Bearer ${localStorage.getItem('jwt')}`})});

    return request;
  }

  DeleTask(taskId: number): Observable<any> {
    const request = this.httpClient.delete(
      `${this.baseURL}/api/Task/${taskId}`,
      {headers:new HttpHeaders({'Authorization':`Bearer ${localStorage.getItem('jwt')}`})}
    );

    return request;
  }
}
