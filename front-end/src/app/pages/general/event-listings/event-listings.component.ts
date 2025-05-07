import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthServiceService } from '../../auth/auth-services/auth-service.service';
import { ReloadService } from '../../../shared/service/reload.service';
import { SnakebarService } from '../../../shared/service/SnakebarService.service';
import { CreateBookingDTO } from '../../models/Booking ';
import { forkJoin, of } from 'rxjs';
import { switchMap, catchError } from 'rxjs/operators';
import { EventService } from '../services/event.service';
import { BookingService } from '../services/booking.service';

@Component({
  selector: 'app-event-listings',
  templateUrl: './event-listings.component.html'
})
export class EventListingsComponent implements OnInit ,AfterViewInit {
  
  events: any[] = [];
  isLoggedIn = false;
  userBookings: any[] = [];
  isLoading = true;

  constructor(
    private reload: ReloadService,
    private router: Router,
    private authService: AuthServiceService,
    private snakebarService: SnakebarService,
    private bookingService: BookingService,
    private eventService: EventService
  ) { }
  ngAfterViewInit(): void {   
    this.reload.initializeLoader();
  }
  ngOnInit() {
    this.authService.getloggedStatus().subscribe(status => {
      this.isLoggedIn = status;
    });

    this.loadData();
  }
  loadData() {
    this.isLoading = true;
    
    this.eventService.getAllEvents().pipe(
      switchMap(events => {
        this.events = events;
  
        if (this.isLoggedIn) {
          const userId = this.authService.getNameIdentifier();
          return this.bookingService.getBookingItemsForAUser(userId).pipe(
            catchError(err => {
              console.error('Error fetching bookings:', err);
              return of([]); 
            })
          );
        } else {
          return of([]); 
        }
      })
    ).subscribe({
      next: (bookings) => {
        this.userBookings = bookings;
        const userId = this.authService.getNameIdentifier();
  
        this.events = this.events.map(event => {
          if (!this.isLoggedIn) {
            return { ...event, isBooked: false }; 
          }
  
          const isBooked = this.userBookings.some(booking => 
            booking.eventId === event.id && booking.userId === userId
          );
  
          return { ...event, isBooked };
        });
  
        this.isLoading = false;
        console.log('Data loaded:', this.events.length, 'events,', this.userBookings.length, 'bookings');
      },
      error: (err) => {
        console.error('Error loading data:', err);
        this.snakebarService.showSnakeBar('Failed to load events. Please try again.');
        this.isLoading = false;
      }
    });
  }

  
  routeToDetails(eventId: number) {
    console.log("Navigating to event-details", eventId);
    this.router.navigate(['/pages/event-details', eventId]);
  }
    
  bookNow(event: any): void {
    if (this.isLoggedIn) {
      console.log('Booking event:', event);
      
      const bookingRequest: CreateBookingDTO = {
        eventId: event.id,
        ticketCount: 1,
      };
      
      this.bookingService.addBooking(bookingRequest).subscribe({
        next: (response) => {
          console.log('Booking successful:', response);
          event.isBooked = true;
          this.router.navigate(['/pages/booking-success']);
          this.snakebarService.showSnakeBar('Booking successful');
        },
        error: (err) => {
          console.error('Booking error:', err);
          this.snakebarService.showSnakeBar('Failed to book event. Please try again.');
        }
      });
    } else {
      console.log('User is not authenticated');
      this.snakebarService.showSnakeBar("You need to be logged in to continue.");
      this.router.navigate(['/auth/login']);
    }
  }
  
  isEventBooked(eventId: number): boolean {
    return this.userBookings.some(booking => booking.eventId === eventId);
  }
}