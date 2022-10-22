import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { aboneBorc } from '../Interfaces/aboneBorc.model';
import { Calculation } from '../Interfaces/calculation.model';
import { AboneService } from '../Services/abone.service';

@Component({
  selector: 'app-calculation',
  templateUrl: './calculation.component.html',
  styleUrls: ['./calculation.component.css']
})
export class CalculationComponent implements OnInit {
  aboneId:number | null | undefined ;
  firstIndex:any;
  LastIndex:any
   stringAboneId:string |null | undefined

  calculateForm = new FormGroup({
    firstIndex:new FormControl('',
    [
    Validators.required
    ]),
    LastIndex:new FormControl('',
    [
    Validators.required
    ])
  })

  constructor(private readonly aboneService:AboneService,private readonly route:ActivatedRoute, private readonly router: Router, private _snackBar:MatSnackBar) { }

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
    this.aboneService.postTuketilenHesap(this.calculateForm.value,this.aboneId!).subscribe(res=>{


      this._snackBar.open('Hesaplama İşlemi Başarılı','Ok',{
        duration:3000
      })
      this.router.navigateByUrl("/abone-borc/"+this.aboneId);

    },error=>{
      this._snackBar.open(error.error,'Ok',{
        duration:3000
      })
    })



  }





}
