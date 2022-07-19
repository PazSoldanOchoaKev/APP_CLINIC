import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSliderModule } from '@angular/material/slider';
import { AgendaService, DayService, MonthAgendaService, MonthService, ScheduleModule, TimelineMonthService, TimelineViewsService, WeekService, WorkWeekService } from '@syncfusion/ej2-angular-schedule';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CalendarComponent } from './calendar/calendar.component';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import { ChartModule } from '@syncfusion/ej2-angular-charts';
import { CategoryService, DataLabelService, LineSeriesService, StepLineSeriesService, SplineSeriesService, StackingLineSeriesService, DateTimeService,
    SplineAreaSeriesService, MultiColoredLineSeriesService, ParetoSeriesService, ColumnSeriesService } from '@syncfusion/ej2-angular-charts';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    CalendarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatSliderModule,
    ScheduleModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    ChartModule
  ],
  providers: [
    DayService, 
    WeekService, 
    WorkWeekService, 
    MonthService, 
    AgendaService, 
    MonthAgendaService, 
    TimelineViewsService, 
    TimelineMonthService,
    CategoryService, 
    LineSeriesService, 
    StepLineSeriesService, 
    SplineSeriesService, 
    StackingLineSeriesService, 
    DateTimeService,
    SplineAreaSeriesService, 
    MultiColoredLineSeriesService, 
    ParetoSeriesService, 
    ColumnSeriesService,
    DataLabelService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
