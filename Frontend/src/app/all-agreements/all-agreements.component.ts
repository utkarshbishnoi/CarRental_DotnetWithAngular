import { Component } from '@angular/core';
import { AgreementService } from '../services/agreement.service';
import { Router } from '@angular/router';
import { RentalAgreement } from '../models/rentalagreement.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-all-agreements',
  templateUrl: './all-agreements.component.html',
  styleUrls: ['./all-agreements.component.css']
})
export class AllAgreementsComponent {
  agreements: any;
  agreement:RentalAgreement={
    carId:0,
    startDate:new Date,
    endDate:new Date,
    userId:0
  }

  constructor(
    private agreementService: AgreementService,
    private router: Router,
    private toastr:ToastrService
  ) {}

  ngOnInit(): void {
    this.agreementService
      .getAllAgreements()
      .subscribe((data) => {
        this.agreements = data;
        console.log(this.agreementService.userId);
      });
  }
  EditAgreementByAdmin(id:any){
    this.agreementService.updateAgreementByAdmin(id,this.agreement)
    .subscribe({
      next:(response)=>{
        this.toastr.success("Edit Agreement successfully!");
          
          this.router.navigate(['/all-agreements']);
      }
    })

  }
  RequestForReturn(id: any) {
    this.agreementService.deleteAgreementByAdmin(id).subscribe({
      next: (response) => {
        this.router.navigate(['/all-agreements']);
      },
    });
  }
}
