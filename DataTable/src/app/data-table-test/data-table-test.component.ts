import { HttpClient } from '@angular/common/http';
import {
  Component,
  Input,
  QueryList,
  TemplateRef,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import { DataTableDirective } from 'angular-datatables';
import { ADTSettings,  } from 'angular-datatables/src/models/settings';
import { Subject } from 'rxjs';
import { AppDemoNgTemplateRefComponent} from '../app-demo-ng-template-ref/app-demo-ng-template-ref.component';

@Component({
  selector: 'app-data-table-test',
  templateUrl: './data-table-test.component.html',
  styleUrls: ['./data-table-test.component.scss'],
})
export class DataTableTestComponent {
  @Input() dataTableBodyData: test[] | test2[] = [];
  @Input() action = true;
  @Input() search = false;
  @Input() orderable = false;
  @Input() columnFilter = false;
  @ViewChildren('#div') viewChildren!: QueryList<any>;
  dtOptions: DataTables.Settings = {};
  // @ViewChild('demoNg') demoNg!: TemplateRef<any>;
  @ViewChild(DataTableDirective, { static: false })
  datatableElement!: DataTableDirective;

  dtTrigger: Subject<ADTSettings> = new Subject<ADTSettings>();

  @ViewChild('demoNg') demoNg: TemplateRef<AppDemoNgTemplateRefComponent> | undefined;
  message=''

  constructor(private http: HttpClient) {}
  // ngOnInit() {
  //   let columns = this.getColums(this.dataTableBodyData);
  //   console.log(columns);
  //   console.log(this.dataTableBodyData);

  //   this.dtOptions = {
  //     data: this.dataTableBodyData,
  //     columns: columns,
  //     responsive: true,
  //     dom: 'Rlfrtip',

  //     //   initComplete: function () {
  //     //     var api = this.api();
  //     //     // Setup - add a text input to each header cell
  //     //     $('.filterhead', api.table().header()).each( function () {
  //     //       var title = $(this).text();
  //     //     $(this).html( '<input type="text" placeholder="Search '+title+'" class="column_search" />' );
  //     //   } );

  //     // }
  //   };
  // }
  ngOnInit() {
    // const dataUrl =
    //   'https://raw.githubusercontent.com/l-lin/angular-datatables/master/demo/src/data/data.json';
    // let columns = this.getColums(this.dataTableBodyData);
    // console.log(columns);
    // this.dtOptions = {
    //   ajax: dataUrl,
    //   columns: [
    //     {
    //       title: 'ID',
    //       data: 'id',
    //     },
    //     {
    //       title: 'First name',
    //       data: 'firstName',
    //     },
    //     {
    //       title: 'Last name',
    //       data: 'lastName',
    //     },
    //   ],

    //   dom: 'Rlfrtip',
    //   responsive: true,

    //   // Use this attribute to enable colreorder
    //   // colReorder: {
    //   //   order: [1, 0, 2],
    //   //   fixedColumnsRight: 2
    //   // }
    // };
    let userData = { token: 'jaga', entryid: '134', song: 'vaali' };
    let t=this.demoNg;
    this.dtOptions = {
      serverSide: true,
      searchDelay: 600, // Set the flag
      ajax: (dataTablesParameters: any, callback) => {
        // dataTablesParameters.columns[0].search.value="s"
        this.http
          .post<any>(
            'https://xtlncifojk.eu07.qoddiapp.com/',
            Object.assign(dataTablesParameters, userData),
            {}
          )
          .subscribe((resp) => {
            callback({
              recordsTotal: resp.recordsTotal,
              recordsFiltered: resp.recordsFiltered,
              data: resp.data,
            });
          });
      },
     
      
      columns: [
        {
          title: 'ID',
          data:'id'
          
          // render: function(data, type, item, meta) {
            
          //   return `<a target="_blank" ${(onclick)=clickData(2)}>'+item["id"]+'</a>' +'<a target="_blank" href="clickData('+item["firstName"]+')">'+item["firstName"]+'</a>` ;
          // },
         
          
        },
        {
          title: 'First name',
          data: 'firstName',
        },
        {
          title: 'Last name',
          data: 'lastName',
        },
      ]
    };
  }
  getColums(datas: test[] | test2[]): columnModel[] {
    let columns: columnModel[] = [];
    let data = datas[1];
    (Object.keys(data) as (keyof typeof data)[]).map((key) => {
      let columnsModel: columnModel = {
        title: key.toString().charAt(0).toUpperCase() + key.toString().slice(1),
        data: key.toString(),
        orderable: this.orderable,
        bSortable: true,
        mData: key.toString(),
      };
      columns.push(columnsModel);
    });
    return columns;
  }
  clickData(data: any) {
    alert(data);
  }

  ngAfterViewInit(): void {
    if (this.columnFilter) {
      this.datatableElement.dtInstance.then((dtInstance: DataTables.Api) => {
        dtInstance.columns().every(function () {
          const that = this;
          $('input', this.footer())
            .off('keyup')
            .on('keyup', function ($event) {
              if (that.search() !== (this as HTMLInputElement).value) {
                that.search((this as HTMLInputElement).value).draw();
              }
            });
        });
      });
    }
  }
}
export interface test {
  id: number;
  name: string;
  message: string;
  button: string;
}
export interface test2 {
  id: number;
  name: string;
  message: string;
  button: string;
  click: string;
}
export interface columnModel {
  title: string;
  data: string;
  orderable: boolean;
  bSortable: boolean;
  mData: string;
}

