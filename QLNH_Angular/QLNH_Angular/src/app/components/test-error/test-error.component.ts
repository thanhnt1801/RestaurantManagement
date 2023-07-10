import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent {
  baseUrl = 'https://localhost:7251/api/Buggy/';
  model : any = {};
  validationError: string[] = [];
  constructor(private http: HttpClient){}

  get404Error(){
    this.http.get(this.baseUrl + 'not-found').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  
  get400Error(){
    this.http.get(this.baseUrl + 'bad-request').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

  get500Error(){
    this.http.get(this.baseUrl + 'server-error').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

  get401Error(){
    this.http.get(this.baseUrl + 'auth').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

  get400ValidationError(){
    this.http.post('https://localhost:7251/api/Authentication/register', this.model).subscribe({
      next: response => console.log(response),
      error: error => {
        console.error();
        this.validationError = error;
      }
    })
  }
}
