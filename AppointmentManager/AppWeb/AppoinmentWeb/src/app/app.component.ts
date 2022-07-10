import { Component, ViewChild } from '@angular/core';
import { EventRenderedArgs, EventSettingsModel, ScheduleComponent } from '@syncfusion/ej2-angular-schedule';
import { DataManager, WebApiAdaptor } from '@syncfusion/ej2-data';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  @ViewChild('scheduleObj') scheduleObj!: ScheduleComponent;
  title = 'AppoinmentWeb';
  startHour = '09:00';
  endHour = '18:00';

  constructor() {
  }

  private dataManager: DataManager = new DataManager({
    url: 'http://192.168.18.17:45455/api/appointment/schedule',
    adaptor: new WebApiAdaptor(),
    crossDomain: false
  });

  public eventSettings: EventSettingsModel = { dataSource: this.dataManager };

  public onEventRendered(args: EventRenderedArgs): void {
    const categoryColor: string = args.data['Color'] as string;
    console.log(categoryColor);
    if (!args.element || !categoryColor) {
      return;
    }
    if (this.scheduleObj.currentView === 'Agenda') {
      (args.element.firstChild as HTMLElement).style.borderLeftColor = categoryColor;
    } else {
      args.element.style.backgroundColor = categoryColor;
    }
  }
}
