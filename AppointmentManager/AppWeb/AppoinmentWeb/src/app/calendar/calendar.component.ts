import { Component, OnInit, ViewChild } from '@angular/core';
import { EventRenderedArgs, EventSettingsModel, ScheduleComponent } from '@syncfusion/ej2-angular-schedule';
import { DataManager, WebApiAdaptor } from '@syncfusion/ej2-data';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {

  @ViewChild('scheduleObj') scheduleObj!: ScheduleComponent;
  title = 'AppoinmentWeb';
  startHour = '09:00';
  endHour = '18:00';
  
  constructor() { }

  ngOnInit(): void {
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
