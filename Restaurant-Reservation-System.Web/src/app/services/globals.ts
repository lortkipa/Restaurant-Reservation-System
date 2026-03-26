import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class Globals {
  appFirstWord:string = "Step"
  appSecondWord:string = "Academy"

  apiUrl:string = "https://localhost:7067/api";
}
