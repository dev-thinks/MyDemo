import { Component } from '@angular/core';
import { AbstractControl, FormGroup, ValidationErrors } from '@angular/forms';
import { FormlyFieldConfig } from '@ngx-formly/core';


export function webSiteNameValidator(control: AbstractControl): ValidationErrors {
  if (control.value) {
    var name = control.value;
    console.log(control.value);
    if (name != null && name != "coder blog") {
      return { error: { message: 'The website name must be "coder blog"!' } };
    }
  }
  return {};
}

export function uploadFileValidator(control: AbstractControl): ValidationErrors {
  if (control.value) {
    var fileName = control.value[0].name;
    console.log(fileName);
    var fileExt = fileName.substring(fileName.lastIndexOf('.') + 1, fileName.length).trim() || fileName;
    if (fileExt.toUpperCase() != "PDF" && fileExt.toUpperCase() != "ZIP" &&
    fileExt.toUpperCase() != "JPG" && fileExt.toUpperCase() != "PNG") {
      return { attachment: { message: 'Only support .pdf,.zip,.jpg,.png file!' } };
    }
  }
  return {};
}

@Component({
  selector: 'app-form-layout-testing',
  templateUrl: './form-layout-testing.component.html',
  styleUrls: ['./form-layout-testing.component.scss']
})


export class FormLayoutTestingComponent {

  form = new FormGroup({});
  model = { email: 'email@gmail.com' };

  public fields: FormlyFieldConfig[] = [
    { type: '#isActive'},
    {
      key: 'website_name',
      type: 'input',
      props: {
        label: 'Website Name',
        placeholder: 'Enter website name',
        required: true,
      },
      validators: {
        validation: [webSiteNameValidator]
      },
    },
    {
      key: 'email',
      type: 'input',
      props: {
        label: 'Email address',
        placeholder: 'Enter email',
        pattern: /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/,
        required: true,
      },
      validation: {
        messages: {
          pattern: (error: any, field: FormlyFieldConfig) => `"${field?.formControl?.value}" is not a valid email Address`,
        },
      },
    },
    {
      key: 'attachment',
      type: 'file',
      templateOptions: {
        required: true,
      },
      validators: {
        validation: [uploadFileValidator]
      },
    },
  ];

  public onSubmit() {
    if (this.form.valid) {
      console.log('success submit!');
    } else {
      console.log('validate failed');
    }

  }

}
