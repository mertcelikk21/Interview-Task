import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Abone } from '../Interfaces/abone.model';
import { Subject,Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Kdv } from '../Interfaces/kdv.model';
import { CurrencyUnit } from '../Interfaces/currencyUnit.model';
import { aboneBorc } from '../Interfaces/aboneBorc.model';
import { TuketilenHesapModel } from '../Interfaces/tuketilenHesap.model';
import { girilenUcretModel } from '../Interfaces/girilenUcret.model';
@Injectable({
  providedIn: 'root'
})
export class AboneService {
  baseUrl="https://localhost:44392/"


  constructor(private http:HttpClient) { }

  getAbones():Observable<Abone[]>{
    return this.http.get<Abone[]>(this.baseUrl+'Abone');
  }

  getAbone(id:number | null):Observable<Abone>{
    return this.http.get<Abone>(this.baseUrl+'Abone/'+id)
  }

  getKdvList():Observable<Kdv[]>{
    return this.http.get<Kdv[]>(this.baseUrl+'Abone'+'/kdv');
  }

  getCurrencyUnitList():Observable<CurrencyUnit[]>{
    return this.http.get<CurrencyUnit[]>(this.baseUrl+'Abone'+'/money');
  }

  postAbone(abone:Abone){
    return this.http.post<Abone>(this.baseUrl+'Abone',abone);
  }

  updateAbone(aboneId:number,abone:Abone){
    return this.http.put<Abone>(this.baseUrl+'Abone/'+aboneId,abone);
  }

  getAboneBorc(aboneId:number):Observable<aboneBorc[]>{
    return this.http.get<aboneBorc[]>(this.baseUrl+'Abone/'+'borc/'+aboneId);
  }

  postTuketilenHesap(calculation:TuketilenHesapModel,aboneId:number){
    return this.http.post(this.baseUrl+'Abone/'+'TuketilenHesap/'+aboneId,calculation)
  }

  getFirstIndex(aboneId:number | null){
    return this.http.get(this.baseUrl+'Abone/'+'firstIndex/'+aboneId);
  }

  getLastData():Observable<aboneBorc>{
    return this.http.get<aboneBorc>(this.baseUrl+'Abone/'+'LastData');
  }
  postGirilenUcret(calculation:girilenUcretModel,aboneId:number){
    return this.http.post<girilenUcretModel>(this.baseUrl+'Abone/'+'GirilenPara/'+aboneId,calculation);
  }
  getAnlikKur(aboneId:number){
    return this.http.get(this.baseUrl+'Abone/'+'kur/'+aboneId);
  }


}
