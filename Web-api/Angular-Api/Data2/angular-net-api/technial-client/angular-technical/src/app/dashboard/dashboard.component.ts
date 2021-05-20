import { Router } from '@angular/router';
import { Subject } from '../../../node_modules/angular-datatables/node_modules/rxjs/internal/Subject';
import { DataService } from './../data.service';
import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit, OnDestroy {

  products = [] as any;
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(private dataService : DataService, private router: Router) { }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 20
    };
    this.dataService.sendGetRequest().subscribe((data: any) => {
      console.log(data);
      this.products = data;
      this.dtTrigger.next();
    })
  }

  ngOnDestroy(){
    this.dtTrigger.unsubscribe();
  }
  sendMeToEditPage(identifier: any){
    console.log(identifier);
    this.router.navigate(['edit'], {queryParams: {product: identifier.toString()}})
  }

}
