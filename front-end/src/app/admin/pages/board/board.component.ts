import { Component, ElementRef, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { MENU } from '../../menu';
import { PatientService } from '../../services/patient.service';
import { ToastrService } from 'ngx-toastr';
import { Doctor } from '../../../pages/models/doctor';
import { AppointmentService } from '../../../pages/general/services/appointment.service';
import { DoctorService } from '../../../pages/general/services/doctor.service';
import { ReloadService } from '../../../shared/service/reload.service';
import { TotalEarningsService } from '../../services/total-earnings.service';
import { DeleteModalComponent } from '../delete-modal/delete-modal.component';
import { EventService } from '../../../pages/general/services/event.service';
import { BookingService } from '../../../pages/general/services/booking.service';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent implements OnInit, OnDestroy {

  appointments: any[] = [];
  infoBoxes: any[] = [];
  doctorsData: Doctor[] = [];
  numOfAppointments: number = 0;
  numOfDoctors: number = 0;
  numOfPatients: number = 0;
  totalAmountEarning: number = 0;
  selectedAppointmentId!: number;
  menuItems = MENU;
  events: any[] = [];
  bookings: any[] = [];
  private subscriptions: Subscription[] = [];  // Store subscriptions

  constructor(
    private appointmentService: AppointmentService,
    private doctorService: DoctorService,
    private reload: ReloadService,
    private toaster: ToastrService,
    private patientService: PatientService,
    private totalEarningService: TotalEarningsService,
    private eventService: EventService,
    private bookingService: BookingService
  ) {}

  ngAfterViewInit(): void {
    this.reload.initializeLoader();
  }

  ngOnInit(): void {
    this.loadAppointments();
    this.fetchPatientLength();
    this.optimizeWidget();
    this.getTotalEarning();
    this.loadEvents();
    this.loadBookings();
  }

  loadAppointments(): void {
    const appointmentSub = this.appointmentService.getAppointments().subscribe(
      (data) => {
        this.appointments = data;
        this.numOfAppointments = this.appointments.length;
        this.optimizeWidget();
      },
      (error) => {
        console.error('Error fetching appointments:', error);
      }
    );
    this.subscriptions.push(appointmentSub);
  }

  loadBookings(): void {
    this.bookingService.getBookingItems().subscribe({
      next: (bookings) => {
        this.bookings = bookings;
        console.log('Bookings loaded:', this.bookings);
      },
      error: (error) => {
        console.error('Error loading bookings:', error);
      }
    });
  }

  loadEvents(): void {
    this.eventService.getAllEvents().subscribe({
      next: (events) => {
        this.events = events;
        console.log('Events:', this.events);
      },
      error: (error) => {
        console.error('Error fetching events:', error);
      }
    });
  }
  fetchPatientLength(): void {
    const patientSub = this.patientService.getAllPatient().subscribe({
      next: (data) => {
        this.numOfPatients = data.length;
        this.optimizeWidget();
      },
      error: (err) => {
        console.error('Error fetching patients:', err);
      }
    });
    this.subscriptions.push(patientSub);
  }

  getTotalEarning(): void {
    const earningSub = this.totalEarningService.getTotalEarnings().subscribe({
      next: (data) => {
        this.totalAmountEarning = data.totalEarnings;
      },
      error: (err) => {
        console.error('Error fetching total earnings:', err);
      }
    });
    this.subscriptions.push(earningSub);
  }

  optimizeWidget(): void {
    this.infoBoxes = [
      {
        bgClass: 'bg-blue',
        iconClass: 'fas fa-users',
        text: 'Appointments',
        number: this.numOfAppointments,
        progress: 45,
        description: '45% Increase in 28 Days',
      },
      {
        bgClass: 'bg-orange',
        iconClass: 'fas fa-user',
        text: 'New Patients',
        number: this.numOfPatients,
        progress: 40,
        description: '40% Increase in 28 Days',
      },
      {
        bgClass: 'bg-purple',
        iconClass: 'fas fa-syringe',
        text: 'Doctors',
        number: this.numOfDoctors,
        progress: 85,
        description: '85% Increase in 28 Days',
      },
      {
        bgClass: 'bg-success',
        iconClass: 'fas fa-dollar-sign',
        text: 'PrimeCare Earning',
        number: this.totalAmountEarning,
        progress: 50,
        description: '50% Increase in 28 Days',
      },
    ];
  }

  openDeleteModal(id: number) {
    this.selectedAppointmentId = id;
    this.deleteModal.showModal();
  }

  @ViewChild(DeleteModalComponent) deleteModal!: DeleteModalComponent;

  onDeleteAppointment(id: number) {
    const deleteSub = this.appointmentService.deleteBookingById(id).subscribe({
      next: () => {
        this.toaster.success("Appointment deleted successfully");
        this.loadAppointments();
      },
      error: (err) => {
        console.error('Error deleting appointment:', err);
        this.toaster.error("Error deleting appointment");
      }
    });
    this.subscriptions.push(deleteSub);
  }

  @ViewChild('editModal', { static: false }) editModal!: ElementRef;
  appointmentDate: string = '';
  appointmentTime: string = '';

  onEditevent(id: number, appointmentDate: string): void {
    this.selectedAppointmentId = id;
    this.appointmentDate = appointmentDate.split('T')[0];
    this.appointmentTime = appointmentDate.split('T')[1]?.substring(0, 5) || '';

    const modalElement = document.getElementById('editModal');
    if (modalElement) {
      modalElement.classList.remove('hidden');
    }
  }

  closeModal(): void {
    const modalElement = document.getElementById('editModal');
    if (modalElement) {
      modalElement.classList.add('hidden');
    }
  }

  saveAppointment(): void {
    const updatedAppointment = {
      id: this.selectedAppointmentId,
      appointmentTakenDate: this.appointmentDate + 'T' + this.appointmentTime,
    };

    if (!this.selectedAppointmentId) {
      this.toaster.error("No appointment selected!");
      return;
    }

    const editSub = this.appointmentService.editeBooking(this.selectedAppointmentId, updatedAppointment).subscribe({
      next: () => {
        this.toaster.success("Appointment Updated successfully");
        this.loadAppointments();
      },
      error: (err) => {
        console.error('Error Updating appointment:', err);
        this.toaster.error("Error Updating appointment");
      }
    });
    this.subscriptions.push(editSub);

    this.closeModal();
  }

  // Clean up when component is destroyed
  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe());
    console.log("BoardComponent destroyed and subscriptions cleaned up.");
  }
}
