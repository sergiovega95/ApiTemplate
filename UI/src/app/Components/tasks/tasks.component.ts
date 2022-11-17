import { Component } from '@angular/core';
import { TodoApiService } from 'src/app/Services/todo-api.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css'],
})
export class TasksComponent {

  constructor(private todoservice: TodoApiService) {}

  async addTask(taskDescription: string) {
    let userEnteredValue = taskDescription;
    document.getElementById('clearbtn')?.classList.add('active');
    await this.todoservice.CreateTask(taskDescription);
  }

  inputKeyup(taskDescription: string) {
    let userEnteredValue = taskDescription;

    userEnteredValue.trim().length === 0
      ? document.getElementById('addBtn')?.classList.remove('active')
      : document.getElementById('addBtn')?.classList.add('active');
  }
}
