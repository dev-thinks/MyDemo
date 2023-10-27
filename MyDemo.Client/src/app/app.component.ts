// import { AfterViewInit, Component, OnInit } from '@angular/core';
// import { LoadingService } from './services/loading.service';

// @Component({
//   selector: 'app-root',
//   templateUrl: './app.component.html',
//   styleUrls: ['./app.component.scss']
// })
// export class AppComponent {
//   title = 'MyDemo.Client';
// }


import { Component, OnInit, AfterViewInit } from '@angular/core';
import { PreloaderService } from './services/preloader.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, AfterViewInit {
  constructor(private preloader: PreloaderService) {}

  title = 'MyDemo.Client';

  ngOnInit() {
  }

  ngAfterViewInit() {
    this.preloader.hide();
  }
}
