import { HttpClient } from '@angular/common/http';
import {
  Component,
  ContentChild,
  ElementRef,
  Input,
  Output,
  QueryList,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { TestDirective } from '../test.directive';
import { TestServiceService } from '../test-service.service';

@Component({
  selector: 'app-app-demo-ng-template-ref',
  templateUrl: './app-demo-ng-template-ref.component.html',
  styleUrls: ['./app-demo-ng-template-ref.component.scss'],
})
export class AppDemoNgTemplateRefComponent {
  // @ViewChildren('div') panes!: QueryList<any>;
  dtOptions: DataTables.Settings = {};
  // dtOptionsLegacy: DataTables.SettingsLegacy = {
  //   ajax: undefined,
  //   oApi: undefined,

  // };
  persons: any[] = [];
  @ViewChild(DataTableDirective, { static: false })
  datatableElement!: DataTableDirective;
  // @ViewChild('myButton')
  // myButton!: ElementRef;
  // @ViewChild(TestDirective) test!: TestDirective;
  // dtTrigger: Subject<any> = new Subject<any>();
  constructor(
    private http: HttpClient,
    private testService: TestServiceService
  ) {}

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      autoWidth: false,
      columnDefs: [
        {
          targets: 'nosort',
          orderable: false,
        },
        {
          targets: [1, 2],
          width: '15%',
        },
      ],
      serverSide: true,
      processing: true,
      searchDelay: 1200,
      orderCellsTop: true,
      ajax: (dataTablesParameters: any, callback) => {
        this.http
          .post<any>(
            'https://xtlncifojk.eu07.qoddiapp.com/',
            dataTablesParameters,
            {}
          )
          .subscribe((resp) => {
            this.persons = resp.data;
            callback({
              recordsTotal: resp.recordsTotal,
              recordsFiltered: resp.recordsFiltered,
              data: [],
            });
          });
      },
      order: [[0, 'desc']],
      columns: [{ data: '' }, { data: 'firstName' }, { data: 'lastName' }],
    };
  }
  keepOrder = (a: any, b: any) => {
    return a;
  };
  ngAfterViewInit(): void {
    this.datatableElement.dtInstance.then((dtInstance: DataTables.Api) => {
      this.testService.createInstance.next(dtInstance);
    });
  }

  // ngOnDestroy(): void {
  //   this.dtTrigger.unsubscribe();
  // }

  // clickData(data: any) {
  //   alert(data);
  // }
}
