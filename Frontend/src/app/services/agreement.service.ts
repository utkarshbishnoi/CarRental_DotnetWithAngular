import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RentalAgreement } from '../models/rentalagreement.model';

@Injectable({
  providedIn: 'root',
})
export class AgreementService {
  apiurl = 'http://localhost:5000/api/v1/carrentalagreement';
  userId:number=0;
  username:string='';
  user: any;
  agreement:any;

  constructor(private http: HttpClient) {
    this.user = localStorage.getItem('userInfo');
    this.user = JSON.parse(this.user);
    if (this.user) {
      this.userId = this.user.id;
      console.log(this.user.token);
    }
  }

  getAllAgreements(): Observable<RentalAgreement[]> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.user.token}`);
    return this.http.get<RentalAgreement[]>(`${this.apiurl}/agreements`,{headers});
  }

  getAgreementsByUserId(id: any): Observable<RentalAgreement[]> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.user.token}`);
    return this.http.get<RentalAgreement[]>(`${this.apiurl}/user/${id}`,{headers});
  }

  createAgreements(agreement: any): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.user.token}`);
    return this.http.post(`${this.apiurl}/create`, agreement,{headers});
  }

  updateAgreementByUser(
    agreementId: any,
    updateAgreement: any
  ): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.user.token}`);
    return this.http.put(
      `${this.apiurl}/update/${agreementId}`,
      updateAgreement,{headers}
    );
  }

  updateAgreementByAdmin(
    agreementId: any,
    updateAgreement: any
  ): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.user.token}`);
    return this.http.put(
      `${this.apiurl}/admin/update/${agreementId}`,
      updateAgreement,{headers}
    );
  }

  deleteAgreementByUser(agreementId: any): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.user.token}`);
    return this.http.delete(`${this.apiurl}/delete/${agreementId}`,{headers});
  }

  deleteAgreementByAdmin(agreementId: any): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.user.token}`);
    return this.http.delete(`${this.apiurl}/admin/delete/${agreementId}`,{headers});
  }
}
