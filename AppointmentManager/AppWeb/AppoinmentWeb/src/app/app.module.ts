import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSliderModule } from '@angular/material/slider';
import { AgendaService, DayService, MonthAgendaService, MonthService, ScheduleModule, TimelineMonthService, TimelineViewsService, WeekService, WorkWeekService } from '@syncfusion/ej2-angular-schedule';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatSliderModule,
    ScheduleModule
  ],
  providers: [
    DayService, 
    WeekService, 
    WorkWeekService, 
    MonthService, 
    AgendaService, 
    MonthAgendaService, 
    TimelineViewsService, 
    TimelineMonthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
