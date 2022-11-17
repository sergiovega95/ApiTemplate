import { TodoApiService } from 'src/app/Services/todo-api.service';
import { Component, OnInit } from '@angular/core';
import { Task } from 'src/app/Interfaces/Task';
import { GetAllTaskResponse } from 'src/app/Interfaces/GetAllTaskResponse';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css'],
})
export class TasksComponent implements OnInit {
  tasks: Task[] = [];

  constructor(private todoservice: TodoApiService) {}

  ngOnInit(): void {
    this.todoservice
      .GetAuthToken('sergio', 'sergio@gmail.com')
      .subscribe((data: any) => {
        localStorage.setItem('jwt', data);
      });

    this.GetAllTask();
  }

  GetAllTask() {
    this.todoservice.GetAllTask().subscribe((response: GetAllTaskResponse) => {
      console.log(response);
      this.tasks = response.data;
    });
  }

  addTask(taskDescription: string) {
    let userEnteredValue = taskDescription;

    this.todoservice.CreateTask(userEnteredValue).subscribe((respose: any) => {
      this.GetAllTask();
    });

    const input = document.getElementById(
      'taskDescription'
    ) as HTMLInputElement;

    input.value = '';
  }

  deleteTask(taskId: number) {
    this.todoservice.DeleTask(taskId).subscribe((respose: any) => {
      this.GetAllTask();
    });
  }

  inputKeyup(taskDescription: string) {
    let userEnteredValue = taskDescription;

    userEnteredValue.trim().length === 0
      ? document.getElementById('addBtn')?.classList.remove('active')
      : document.getElementById('addBtn')?.classList.add('active');
  }
}
