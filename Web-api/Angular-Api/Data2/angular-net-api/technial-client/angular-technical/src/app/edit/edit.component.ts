import { DataService } from './../data.service';
import { Component, OnInit } from '@angular/core';
import { Product } from '../DTO/product';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  product: Product = new Product();
  constructor(private dataService: DataService,
    private activatedRoute: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    let productId = this.activatedRoute.snapshot.queryParams["product"];
    let IdNumber: number = +productId;
    this.dataService.getProductsById(IdNumber).subscribe((data: any) =>{
      console.log(data);
      this.product = data;
    } );
  }

  ngSubmitFormData(event: any){
    // event.preventDefault();
    let productId = this.activatedRoute.snapshot.queryParams["product"];
    let IdNumber: number = +productId;
    const submitProduct: Product = {
      identifier: IdNumber,
      alojamiento : event.target.txtAlojamiento.value,
      direccion : event.target.txtdireccion.value,
      observaciones : event.target.txtObservaciones.value,
      codAlojamiento : event.target.txtcodAlojamiento.value,
    };
    console.log(submitProduct);
    this.dataService.postProduct(submitProduct).subscribe(data =>{
      this.router.navigate(['dashboard'])
    })
  }
}
