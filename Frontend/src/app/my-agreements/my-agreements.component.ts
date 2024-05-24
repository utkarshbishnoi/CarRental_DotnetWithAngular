import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AgreementService } from '../services/agreement.service';

@Component({
  selector: 'app-my-agreements',
  templateUrl: './my-agreements.component.html',
  styleUrls: ['./my-agreements.component.css'],
})
export class MyAgreementsComponent {
  agreements: any;

  constructor(
    private agreementService: AgreementService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.agreementService
      .getAgreementsByUserId(this.agreementService.userId)
      .subscribe((data) => {
        this.agreements = data;
        console.log(this.agreementService.userId);
      });
  }
  RequestForReturn(id: any) {
    this.agreementService.deleteAgreementByUser(id).subscribe({
      next: (response) => {
        this.router.navigate(['/my-agreements']);
      },
    });
  }
}
