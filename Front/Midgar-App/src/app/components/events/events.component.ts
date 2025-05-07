import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventService } from '../../services/event.service';
import { Events } from '../../models/Events';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit {

  modalRef?: BsModalRef;
  isCollapsed = false;

  public events: Events[] = [];
  public eventsFiltered: Events[] = [];
  public widthImg = 150;
  public marginImg = 2;
  private listedFilter = '';

  public get filter() {
    return this.listedFilter;
  }

  public set filter(value: string) {
    this.listedFilter = value;
    this.eventsFiltered = this.filter ? this.filterEvents(this.filter) : this.events
  }

  // public filterEvents(filterFor: string): Events[] {
  //   const term = filterFor.trim().toLowerCase();
  
  //   return this.events.filter(event =>
  //     Object.keys(event).some(key => {
  //       const val = (event as Record<string, any>)[key];
  //       return val != null
  //         && val.toString().toLowerCase().includes(term);
  //     })
  //   );
  // }

  public filterEvents(filterFor: string): Events[] {
    const filter = this.filter.toLowerCase();
  
    return this.events.filter(event => {
      const lotesAsText = event.lotes.map(l => l.name).join(' ').toLowerCase();
      return (
        event.theme.toLowerCase().includes(filter) ||
        event.local.toLowerCase().includes(filter) ||
        lotesAsText.includes(filter)
      );
    });
  }

  constructor(
    private eventService: EventService, 
    private modalService: BsModalService, 
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  public ngOnInit(): void {
    this.spinner.show();
    this.getEvents();
  }

  public getEvents(): void {

    this.eventService.getEvents().subscribe({
      next: (events: Events[]) => {
        this.events = events;
        this.eventsFiltered = events;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Error loading events.', 'Error!');
      },
      complete: () => this.spinner.hide()
    });
  }

  openModal(confirmModal: TemplateRef<void>) {
    this.modalRef = this.modalService.show(confirmModal, { class: 'modal-sm' });
  }
 
  confirm(): void {
    this.modalRef?.hide();
    this.toastr.success('The event was successfully deleted.', 'Deleted!');
  }
 
  decline(): void {
    this.modalRef?.hide();
  }
}