import {
  Directive,
  ElementRef,
  HostListener,
  Input,
  ViewChild,
} from '@angular/core';
import { DataTableDirective } from 'angular-datatables';
import { TestServiceService } from './test-service.service';
import { Subscription, find } from 'rxjs';

@Directive({
  selector: '[appTest]',
})
export class TestDirective {
  @Input()
  appContenteditableModel!: string;
  @ViewChild(DataTableDirective, { static: false })
  datatableElement!: DataTableDirective;
  sub!: Subscription;
  constructor(
    private el: ElementRef,
    private testService: TestServiceService
  ) {}

  ngOnInit(): void {
    this.sub = this.testService.createInstance.subscribe((next) => {
      this.createFilterInstance(next);
    });
  }

  public createFilterInstance(instance: DataTables.Api) {
    if (instance) {
      // instance.columns().each((i, e, f) => {
      //   console.log(e);
      //   console.log(f);
      //   f?.each((i, e, f) => {
      //     console.log(e);
      //     console.log(f);
      //   });
      //   // console.log(h);
      //   // console.log(f.header());
      // });
      instance.columns().every(function () {
        const that = this;
        // console.log(this);
        console.log(this.header());
        console.log(this.header().id);
        let id = this.header().id;
        console.log($('thead tr').last().find(`th#${id}`));
        let ele = $('thead tr').last().find(`th#${id}`).get(0);
        console.log(ele);
        console.log($('#ss', ele));
        $('#ss', ele)
          .off('keyup change clear')
          .on('keyup change clear', function ($event) {
            if (that.search() !== (this as HTMLInputElement).value) {
              that.search((this as HTMLInputElement).value).draw();
            }
          });
        // console.log(this);
        // console.log($('.sorting'));

        // $('.sorting').each((i, e) => {
        //   console.log(e);
        //   console.log($('#ss', e));
        //   $('#ss', e)
        //     .off('keyup change clear')
        //     .on('keyup change clear', function ($event) {
        //       if (that.search() !== (this as HTMLInputElement).value) {
        //         that.search((this as HTMLInputElement).value).draw();
        //       }
        //     });
        // });
        // $('#ss', '.tg tr th').each((index, element) => {
        //   $(element)
        //     .off('keyup change clear')
        //     .on('keyup change clear', function ($event) {
        //       console.log('super cool potato', element, $event);
        //       if (that.search() !== (this as HTMLInputElement).value) {
        //         that.search((this as HTMLInputElement).value).draw();
        //       }
        //     });
        // });
      });
    }
    // $('.sorting').each((i, e) => {
    //   console.log(e);
    //   console.log($('#ss', e));
    //   $('#ss', e)
    //     .off('keyup change clear')
    //     .on('keyup change clear', function ($event) {
    //       if (that.search() !== (this as HTMLInputElement).value) {
    //         that.search((this as HTMLInputElement).value).draw();
    //       }
    //     });
    // });
  }
  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
