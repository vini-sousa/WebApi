import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit {

  isCollapsed = false;

  public events: any[] = [];
  public eventsFiltered: any = [];
  widthImg = 150;
  marginImg = 2;
  private _filter = '';

  public get filter() {
    return this._filter;
  }

  public set filter(value: string) {
    this._filter = value;
    this.eventsFiltered = this.filter ? this.filterEvents(this.filter) : this.events
  }

  // filterEvents(filterFor: string) : any {
  //   filterFor = filterFor.toLocaleLowerCase();
  //   return this.events.filter(
  //     event => event.theme.toLocaleLowerCase().indexOf(filterFor) !== -1 ||
  //     event.local.toLocaleLowerCase().indexOf(filterFor) !== -1
  //   )
  // }

  filterEvents(filterFor: string): any[] {
    const term = filterFor.trim().toLowerCase();
  
    return this.events.filter(event =>
      Object.keys(event).some(key => {
        const val = event[key];
        return val != null
          && val.toString().toLowerCase().includes(term);
      })
    );
  }

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getEvents();
  }

  public getEvents(): void {

    this.http.get<any[]>('https://localhost:7204/api/events').subscribe(
      response => {
        this.events = response;
        this.eventsFiltered = response;
      },
      error => console.log(error)
    );
  }
}
