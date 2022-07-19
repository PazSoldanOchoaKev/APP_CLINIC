import { Component, OnInit } from '@angular/core';
import { DataManager } from '@syncfusion/ej2-data';
import { interval } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  public primaryXAxis!: Object;
  public primaryYAxis!: Object;
  public marker!: Object;
  public title: string = "Cantidad de citas";
  public chartData!: Object[];
  public dataManager: DataManager = new DataManager({
    url: 'http://192.168.18.17:45455/api/appointment/chart/pending'
    // url: 'https://ej2services.syncfusion.com/production/web-services/api/Orders'
  });  
  
  constructor() { }

  ngOnInit(): void {
    this.chartData = [
      { Date: "2022-06-15T05:00:00Z", Count: 21 }, { Date: "2022-05-15T05:00:00Z", Count: 24 },
      { Date: "2022-07-15T05:00:00Z", Count: 0 }, { Date: "2022-04-15T05:00:00Z", Count: 38 },
      { Date: "2022-08-15T05:00:00Z", Count: 54 }, { Date: "2022-03-15T05:00:00Z", Count: 57 },
    ];
    this.primaryXAxis = {
      valueType: 'DateTime',
      labelFormat: 'dd/MMM'
    };
    this.primaryYAxis = {
    };
    this.marker = { visible: true, width: 10, height: 10, dataLabel: { visible: true}};
  }

}
