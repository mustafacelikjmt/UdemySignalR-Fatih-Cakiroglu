import { Component } from '@angular/core';
import { CovidService } from './Services/covid.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'AngularChart';

  constructor(private covidService: CovidService) { }

  //eski kursta son vidyonun başında bıraktık. çünki google charts ı yükliyemiyoruz.


}
