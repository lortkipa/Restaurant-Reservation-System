import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class Globals {
  appFirstWord:string = "Step"
  appSecondWord:string = "Academy"
  appName:string = this.appFirstWord + " " + this.appSecondWord

  apiUrl:string = "https://localhost:7067/api";
}
