import { Product } from './DTO/product';
import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http'
import { throwError } from 'rxjs';
import {retry, catchError} from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class DataService {
  private REST_API_Server = "https://localhost:44336";
  constructor(private httpClient : HttpClient) { }
  handleError(error: HttpErrorResponse){
    let errorMessage = "Unknown error!";
    if(error.error instanceof ErrorEvent){
      errorMessage = `Error: ${error.error?.message}`;
    }
    else{
      errorMessage = `Error code: ${error.status}\nMessage: ${error.message}`
    }
    window.alert(errorMessage)
    return throwError(errorMessage);
  }

  public sendGetRequest(){
    return this.httpClient
    .get(`${this.REST_API_Server}/Products/GetProducts`)
    .pipe(retry(2), catchError(this.handleError));
  }

  public getProductsById(id: number){
    return this.httpClient
    .get(`${this.REST_API_Server}/Products/GetProductById?id=${id}`)
    .pipe(retry(2), catchError(this.handleError));
  }
  public postProduct(product: Product){
    return this.httpClient.put<Product>(`${this.REST_API_Server}/Products/UpdateProduct`, product)
    .pipe(catchError(this.handleError));
  }
}
