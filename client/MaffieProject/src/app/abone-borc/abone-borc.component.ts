import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Abone } from '../Interfaces/abone.model';
import { aboneBorc } from '../Interfaces/aboneBorc.model';
import { AboneService } from '../Services/abone.service';

@Component({
  selector: 'app-abone-borc',
  templateUrl: './abone-borc.component.html',
  styleUrls: ['./abone-borc.component.css']
})
export class AboneBorcComponent implements OnInit {
  aboneId:number | null | undefined ;
  aboneBorcData:aboneBorc[]=[]
  aboneData: Abone[] =[];
  anlikKur:number| null | undefined ;
  controlDegiskeni: any

  constructor(private readonly aboneService:AboneService, private readonly route:ActivatedRoute) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(
      (params)=>{
        var stringAboneId= params.get('id');
        this.aboneId =+stringAboneId!;
        //aboneBilgileri
        this.aboneService.getAbone(this.aboneId).subscribe(res=>{
          this.aboneData.push(res);
        });
        this.aboneService.getAboneBorc(this.aboneId).subscribe(res=>{
          console.log(res)
            this.aboneBorcData=res;
            this.controlDegiskeni = res.map(x=>x.currencyUnitId)

        })
        this.aboneService.getAnlikKur(this.aboneId).subscribe(res=>{
            this.anlikKur=+res;
          console.log(res);
        })
      })




  }

}
