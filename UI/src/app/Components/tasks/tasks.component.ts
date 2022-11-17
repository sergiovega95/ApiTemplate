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

    this.todoservice.GetAllTask().subscribe((response: GetAllTaskResponse) => {
      this.tasks = response.data;
      console.log(this.tasks);
    });
  }

  addTask(taskDescription: string) {
    let userEnteredValue = taskDescription;
    document.getElementById('clearbtn')?.classList.add('active');
    this.todoservice.CreateTask(userEnteredValue);
  }

  deleteTask(){
    
  }

  inputKeyup(taskDescription: string) {
    let userEnteredValue = taskDescription;

    userEnteredValue.trim().length === 0
      ? document.getElementById('addBtn')?.classList.remove('active')
      : document.getElementById('addBtn')?.classList.add('active');
  }
}