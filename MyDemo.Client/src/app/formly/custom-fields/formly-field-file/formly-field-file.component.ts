import { Component } from '@angular/core';
import { FieldTypeConfig } from '@ngx-formly/core';
import { FieldType } from '@ngx-formly/material';

@Component({
  selector: 'app-formly-field-file',
  templateUrl: './formly-field-file.component.html',
  styleUrls: ['./formly-field-file.component.scss']
})
export class FormlyFieldFileComponent extends FieldType<FieldTypeConfig> {}
