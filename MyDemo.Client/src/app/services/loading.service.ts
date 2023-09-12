
import { Injectable, OnDestroy } from "@angular/core";
import { Subject } from "rxjs";


@Injectable({
  providedIn: 'root',
})
export class LoadingService implements  OnDestroy {
  // Observable string sources
  private emitChangeSource = new Subject<boolean>();
  // Observable string streams
  changeEmitted$ = this.emitChangeSource.asObservable();

  // Start loading
  start() {
    this.emitChangeSource.next(true);
  }

  // Stop loading
  stop() {
    this.emitChangeSource.next(false);
  }

  ngOnDestroy() {
    // complete and release the subject
    this.emitChangeSource.complete();
  }
}
