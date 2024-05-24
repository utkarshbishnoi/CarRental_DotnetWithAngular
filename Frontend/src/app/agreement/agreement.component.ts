import { Component } from '@angular/core';
import { RentalAgreement } from '../models/rentalagreement.model';
import { AgreementService } from '../services/agreement.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserloginService } from '../services/userlogin.service';

@Component({
  selector: 'app-agreement',
  templateUrl: './agreement.component.html',
  styleUrls: ['./agreement.component.css'],
})
export class AgreementComponent {
  agreement: RentalAgreement = {
    carId: 0,
    startDate: new Date(),
    endDate: new Date(),
    userId: 0,
  };
  constructor(
    private route: ActivatedRoute,
    private agreementService: AgreementService,
    private router: Router,
    private toastr: ToastrService,
    private authService:UserloginService
  ) {
    this.agreement.userId=this.authService.user.id;
    this.route.params.subscribe((param) => {
      this.agreement.carId = parseInt(param['id']);
    });
  }
  ngOnInit(): void {}
  book() {
    this.agreementService
      .createAgreements(this.agreement)
      .subscribe((agreement) => {
        this.toastr.success('Agreement created successfully!');
        this.router.navigate(['/my-agreements']);
      });
  }
}
