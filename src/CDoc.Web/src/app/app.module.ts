import { NgModule, APP_INITIALIZER } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxExtendedPdfViewerModule } from 'ngx-extended-pdf-viewer';
import { NoSanitizePipe } from 'src/app/utils/no.sanitize.pipe';
import { CookieService } from 'ngx-cookie-service';
import { AppConfigService } from './services/config/app-config.service';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HttpClientModule } from '@angular/common/http';
import { TreeComponent } from './components/viewer/tree/tree.component';
import { TreeLabelComponent } from './components/viewer/tree/tree-label/tree-label.component';
import { PreviewComponent } from './components/viewer/preview/preview.component';
import { BreadcrumbComponent } from './components/viewer/preview/breadcrumb/breadcrumb.component';
import { HtmlPreviewComponent } from './components/viewer/preview/html-preview/html-preview.component';
import { FilePreviewComponent } from './components/viewer/preview/file-preview/file-preview.component';
import { ImagePreviewComponent } from './components/viewer/preview/image-preview/image-preview.component';
import { SettingsComponent } from './components/settings/settings.component';
import { PdfPreviewComponent } from './components/viewer/preview/pdf-preview/pdf-preview.component';
import { CookieAlertComponent } from './components/cookie-alert/cookie-alert.component';
import { ViewerComponent } from './components/viewer/viewer.component';
import { DefaultComponent } from './components/viewer/default/default.component';

import { HeaderModule } from './header/header.module';
import { SourceComponent } from './components/settings/sources/source/source.component';
import { SourcesComponent } from './components/settings/sources/sources.component';
import { SourceInfoComponent } from './components/settings/sources/source/source-info/source-info.component';

@NgModule({
  declarations: [
    AppComponent,
    TreeComponent,
    TreeLabelComponent,
    PreviewComponent,
    BreadcrumbComponent,
    HtmlPreviewComponent,
    FilePreviewComponent,
    ImagePreviewComponent,
    SettingsComponent,
    PdfPreviewComponent,
    NoSanitizePipe,
    CookieAlertComponent,
    ViewerComponent,
    DefaultComponent,
    SourceComponent,
    SourcesComponent,
    SourceInfoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    HttpClientModule,
    NgxExtendedPdfViewerModule, 
    HeaderModule
  ],
  providers: [
    CookieService,
    {
      provide: APP_INITIALIZER,
      multi: true,
      deps: [AppConfigService],
      useFactory: (appConfigService: AppConfigService) => {
        return () => {
          return appConfigService.loadAppConfig();
        };
      }
    }
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }