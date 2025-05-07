import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ReloadService } from '../../../shared/service/reload.service';
import { BookingService } from '../services/booking.service';
import { EventService } from '../services/event.service';
import { CreateBookingDTO } from '../../models/Booking ';

@Component({
  selector: 'app-event-details',
  templateUrl: './event-details.component.html',
  styleUrls: ['./event-details.component.css']
})
export class EventDetailsComponent implements OnInit {

  constructor(private reload: ReloadService,
    private route: ActivatedRoute,
    private eventService: EventService,
    private bookingService: BookingService) { }
  ngAfterViewInit(): void {
    this.reload.initializeLoader();
  }
  private routeSub!: Subscription;
  private eventSub!: Subscription
  eventId: number = 0;
  event: any = {};
  ngOnInit() {

    this.route.paramMap.subscribe(params => {
      this.eventId = +params.get('eventId')!;
      console.log("params ", this.eventId);
    });

    this.eventService.getEventById(this.eventId).subscribe(item => {
      this.event = item;
    }, (error) => {
      console.error('Error fetching product:', error);
    });
  }


  ngOnDestroy(): void {
    if (this.routeSub) {
      this.routeSub.unsubscribe();
    }
    if (this.eventSub) {
      this.eventSub.unsubscribe();
    }
    console.log('event Details Component destroyed');
  }

  bookNow(): void {
    const bookingRequest: CreateBookingDTO = {
      eventId: this.event.id,
      ticketCount: 1,
    };
    this.bookingService.addBooking(bookingRequest);
  }
}
