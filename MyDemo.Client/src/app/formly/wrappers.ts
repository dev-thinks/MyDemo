import { Component } from '@angular/core';
import { FieldWrapper } from '@ngx-formly/core';

/**
 * This is just an example for wrapping field into a div
 */
@Component({
  selector: 'formly-wrapper-div',
  template: `
    <div>
      <ng-container #fieldComponent></ng-container>
    </div>
  `,
})
export class FormlyWrapperDivComponent extends FieldWrapper {}
