import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { TextFieldModule } from '@angular/cdk/text-field';
import { MtxGridModule } from '@ng-matero/extensions/grid';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserManagementComponent } from './user-management/user-management.component';
import { FormLayoutTestingComponent } from './form-layout-testing/form-layout-testing.component';
import { FormlyModule } from '@ngx-formly/core';
import { FormlyMaterialModule } from '@ngx-formly/material';
import { FormlyPresetModule } from '@ngx-formly/core/preset';
import { FormlyWrapperDivComponent } from './formly/wrappers';
import { FormlyFieldFileComponent } from './formly/custom-fields/formly-field-file/formly-field-file.component';
import { FileValueAccessor } from './form-layout-testing/file-value-accessor';
import { LoginComponent } from './login/login.component';
import { TokenInterceptor } from './core/token-interceptor';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { NgxLoadingModule } from 'ngx-loading';
import { GeneratepdfComponent } from './generatepdf/generatepdf.component';


@NgModule({
  declarations: [
    AppComponent,
    UserManagementComponent,
    FormLayoutTestingComponent,
    FormlyFieldFileComponent,
    FileValueAccessor,
    LoginComponent,
    MainLayoutComponent,
    GeneratepdfComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    NoopAnimationsModule,
    HttpClientModule,
    FormsModule,
    MatInputModule,
    MatButtonModule,
    TextFieldModule,
    MatFormFieldModule,
    MtxGridModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormlyMaterialModule,
    FormlyPresetModule,
    SweetAlert2Module.forRoot(),
    NgxLoadingModule.forRoot({}),
    FormlyModule.forRoot({
      presets: [
        {
          name: 'isActive',
          config: {
            key: 'isActive',
            type: 'checkbox',
            templateOptions: {
              label: 'Is Active',
            },
            wrappers: ['div']
          }
        },
      ],
      wrappers: [
        {
          name: 'div',
          component: FormlyWrapperDivComponent,
        },
      ],
      types: [
        {
          name: 'file',
          component: FormlyFieldFileComponent,
          wrappers: ['form-field']
        }
      ],
    })
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
