import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ReloadService } from '../../../shared/service/reload.service';
import { SpecializationService } from '../services/specialization.service';
import { PatientService } from '../../../admin/services/patient.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-Home',
  templateUrl: './Home.component.html',
  styleUrls: ['./Home.component.css']
})
export class HomeComponent implements  OnInit , AfterViewInit {

  constructor(private reload : ReloadService    ) { } 

  ngAfterViewInit(): void {   
    this.reload.initializeLoader();
  }

  ngOnInit(): void {
  }
  
}
