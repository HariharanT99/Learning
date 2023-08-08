import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';import {DataTablesModule} from 'angular-datatables';
import { HttpClientModule } from '@angular/common/http';
import { DataTableTestComponent } from './data-table-test/data-table-test.component';
import { AppDemoNgTemplateRefComponent } from './app-demo-ng-template-ref/app-demo-ng-template-ref.component';
import { TestDirective } from './test.directive';


@NgModule({
  declarations: [
    AppComponent,
    DataTableTestComponent,
    AppDemoNgTemplateRefComponent,
    TestDirective 

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    DataTablesModule,
    HttpClientModule
  ],exports: [ TestDirective ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
