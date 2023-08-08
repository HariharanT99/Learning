import { Component } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { DataTablesModule } from 'angular-datatables';
import { BrowserModule } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'angular-datatable';
  public empData: test[] = [];
  public temp: any = false;
  constructor(private http: HttpClient) {}

  allUsers: any = [];

  ngOnInit(): void {
    //   this.http.get('https://jsonplaceholder.typicode.com/posts').subscribe((resp: any) => {
    //   let mo:{button:string,action:string}={
    //     button:"<button>Click</button>",action:"<a href='https://www.google.com/search?gs_ssp=eJzj4tTP1TcwMU02T1JgNGB0YPBiS8_PT89JBQBASQXT&q=google&rlz=1C1YTUH_enIN1027IN1027&oq=go&gs_lcrp=EgZjaHJvbWUqEwgBEC4YgwEYxwEYsQMY0QMYgAQyBggAEEUYOTITCAEQLhiDARjHARixAxjRAxiABDINCAIQABiDARixAxiABDINCAMQABiDARixAxiABDINCAQQABiDARixAxiABDIGCAUQRRg9MgYIBhBFGD0yBggHEEUYPNIBCDExOTVqMGo3qAIAsAIA&sourceid=chrome&ie=UTF-8'>tab</a>"}
    //   resp.map((x:any)=> {..x})
    //   let modified=resp
    //   .forEach((element:any) => {

    //       return {...element,button:"<button>Click</button>"}

    //   });
    //   this.empData =modified ;
    // this.temp = true;
    // });

    let temp: test = {
      id: 2,
      name: 'jaga',
      message: 'anbin iyal visudhe',
      button: 'erer',
    };
    let temp1: test = {
      id: 1,
      name: 'vijay',
      message: 'anbin iyal visudhe',
      button: '<button>ddd</button>',
    };
    let temp2: test = {
      id: 3,
      name: 'vijadfy',
      message: 'anbin iyal visudhe',
      button: '<button>ddd</button>',
    };
    this.empData.push(temp);
    this.empData.push(temp1);
    this.empData.push(temp2);

    //  let t=this.getColumn();
  }

  getColumn() {
    //   let key = Object.keys(column);
    //   let value = Object.keys(column);
    //  for (let index = 0; index < key.length; index++) {
    //   const element = array[index];
    //}
  }
  clickData(data:any){
    alert(data);
  }
}
export interface test {
  id: number;
  name: string;
  message: string;
  button: string;
}

export enum column {
  'Id' = 'id',
  'Name' = 'name',
  'User message' = 'message',
  'ActionButtom' = 'button',
}
