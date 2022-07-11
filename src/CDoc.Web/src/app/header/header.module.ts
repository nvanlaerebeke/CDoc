import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { HeaderComponent } from './header/header.component';
import { ViewmodeComponent } from './header/viewmode/viewmode.component';
import { SourceComponent } from './header/source/source.component';

@NgModule({
  declarations: [
    HeaderComponent,
    ViewmodeComponent,
    SourceComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule
  ],
  exports: [ HeaderComponent ]
})
export class HeaderModule { }
