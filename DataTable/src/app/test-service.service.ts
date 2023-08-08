import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TestServiceService {

constructor() { }
public createInstance = new Subject<DataTables.Api>();

}
