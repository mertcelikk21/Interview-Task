import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Abone } from '../Interfaces/abone.model';
import { CurrencyUnit } from '../Interfaces/currencyUnit.model';
import { Kdv } from '../Interfaces/kdv.model';
import { AboneService } from '../Services/abone.service';

@Component({
  selector: 'app-abone-detail',
  templateUrl: './abone-detail.component.html',
  styleUrls: ['./abone-detail.component.css']
})
export class AboneDetailComponent implements OnInit {

  aboneId:number | null | undefined ;
  aboneData: Abone = {
    id:0,
    address : '',
    currencyUnit : '',
    currencyUnitId:0,
    kdv:0,
    kdvId:0,
    name:'',
    number:'',
    openingDate:'',
    surname:''
  }
  getKdv:number | null | undefined ;
  checkedKdv:number | null | undefined ;
  checkedCurrencyUnit:number | null | undefined ;
  type:any;


  kdvList:Kdv[]=[];
  currencyUnitList:CurrencyUnit[]=[];

  aboneForm = new FormGroup({
    number:new FormControl('',
    [
    Validators.required,
    Validators.minLength(8),
    Validators.maxLength(8)
    ]),
    address:new FormControl('',
    [
    Validators.required,
    Validators.maxLength(100)
    ]),
    name:new FormControl('',
    [
    Validators.required
    ]),
    surname:new FormControl('',
    [
    Validators.required
    ]),
    kdvId:new FormControl('',
    [
    Validators.required,

    ]),
    currencyUnitId:new FormControl('',
    [
    Validators.required
    ]),
  })




  constructor(private readonly aboneService:AboneService, private readonly route:ActivatedRoute, private _snackBar:MatSnackBar, private router : Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(
      (params)=>{
        var stringAboneId= params.get('id');
        this.aboneId =+stringAboneId!;
        this.aboneService.getAbone(this.aboneId).subscribe(
          (success)=>{
              this.aboneData=success;
              console.log(success);
              this.checkedKdv=success.kdvId
              this.checkedCurrencyUnit=success.currencyUnitId
          },
          (error)=>{

          }
        )
      }
    )


      this.aboneService.getKdvList().subscribe(
        (success)=>{
          this.kdvList=success;
          this.getKdv =+success.map(x=>x.kdvRatio)

        }
      )


    this.aboneService.getCurrencyUnitList().subscribe( response=>{
      this.currencyUnitList=response;
    })



  }

  onSubmit(){
    this.aboneService.updateAbone(this.aboneId!,this.aboneForm.value).subscribe(response=>{
      this.router.navigateByUrl('')
      this._snackBar.open('Abone Başarıyla Günncellendi','Ok',{
        duration:3000
      })
      console.log(response);
    },error=>{
      this._snackBar.open("Abone Güncellenemedi, Bilinmeyen Bir hata oluştu",'Ok',{
        duration:3000
      })
    })
  }







}
