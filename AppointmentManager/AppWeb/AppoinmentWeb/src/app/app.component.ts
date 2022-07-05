import { Component, ViewChild } from '@angular/core';
import { EventRenderedArgs, EventSettingsModel, ScheduleComponent } from '@syncfusion/ej2-angular-schedule';
import { DataManager, WebApiAdaptor } from '@syncfusion/ej2-data';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AppoinmentWeb';
  startHour = '09:00';
  endHour = '18:00';

  private dataManager: DataManager = new DataManager({
    url: 'http://192.168.18.17:45455/api/appointment/schedule',
    adaptor: new WebApiAdaptor(),
    crossDomain: false
  });

  public eventSettings: EventSettingsModel = { dataSource: this.dataManager };
}
