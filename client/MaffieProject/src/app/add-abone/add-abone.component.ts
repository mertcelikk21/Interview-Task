import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Kdv } from '../Interfaces/kdv.model';
import { AboneService } from '../Services/abone.service';
import {TooltipPosition} from '@angular/material/tooltip';
import { MatRadioButton } from '@angular/material/radio';
import { CurrencyUnit } from '../Interfaces/currencyUnit.model';
import {MatSnackBar} from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-abone',
  templateUrl: './add-abone.component.html',
  styleUrls: ['./add-abone.component.css']
})
export class AddAboneComponent implements OnInit {

  radioValue :number | null | undefined;
  currencyUnitValue :number | null | undefined;
  addressValue : number | null | undefined;
  errorObject:object={
    errors:''
  };

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

  constructor(private readonly aboneService:AboneService, private _snackBar:MatSnackBar, private route : Router) { }

  ngOnInit(): void {

    this.aboneService.getKdvList().subscribe(
      (success)=>{
        this.kdvList=success;

      }
    )

    this.aboneService.getCurrencyUnitList().subscribe( response=>{
      this.currencyUnitList=response;
    }

    )


  }

  onSubmit(){
    this.aboneService.postAbone(this.aboneForm.value).subscribe(response=>{
      this.route.navigateByUrl('');
      this._snackBar.open('Abone Başarıyla Eklendi','Ok',{
        duration:3000
      })
    },error=>{
      this._snackBar.open(error.error,'Ok',{
        duration:3000
      })
    })
  }



}


