import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { girilenUcretModel } from '../Interfaces/girilenUcret.model';
import { AboneService } from '../Services/abone.service';

@Component({
  selector: 'app-tutar-calculation',
  templateUrl: './tutar-calculation.component.html',
  styleUrls: ['./tutar-calculation.component.css']
})
export class TutarCalculationComponent implements OnInit {
  firstIndex:any;
  amount:any;
  aboneId:number | null | undefined ;
  stringAboneId:string |null | undefined

  calculateForm = new FormGroup({
    amount:new FormControl('',
    [
    Validators.required
    ]),
    firstIndex: new FormControl('')
  })
  constructor(private readonly aboneService:AboneService,private readonly route:ActivatedRoute,private _snackbar:MatSnackBar, private router : Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(
      (params)=>{
         this.stringAboneId = params.get('id');
        this.aboneId =+this.stringAboneId!;
        this.aboneService.getFirstIndex(this.aboneId).subscribe(res=>{
          this.firstIndex=res;

        })
      }
    )
  }



  onSubmit(){
    this.aboneService.postGirilenUcret(this.calculateForm.value,this.aboneId!).subscribe(res=>{

      console.log(res);
      this._snackbar.open('Hesaplama İşlemi Başarılı','Ok',{
        duration:3000
    })
    this.router.navigateByUrl("/abone-borc/"+this.aboneId);

    },error=>{
        this._snackbar.open('Hesaplama İşlemi Başarılı','Ok',{
        duration:3000
    })
    })
  }


}
