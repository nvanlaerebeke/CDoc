import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SettingsComponent } from './components/settings/settings.component';
import { ViewerComponent } from './components/viewer/viewer.component';
import { PreviewComponent } from './components/viewer/preview/preview.component';
import { DefaultComponent } from './components/viewer/default/default.component';
import { ItemResolver } from './components/viewer/item-resolver';

const routes: Routes = [
  { path: '', redirectTo: 'view', 'pathMatch': 'full' },
  { path: 'settings', component: SettingsComponent },
  { 
    path: 'view', 
    component: ViewerComponent,
    children: [ {
      path: '',
      component: DefaultComponent
    }, {
      path: '**',
      resolve: {
        data: ItemResolver
      },
      data: { name: "page"},
      component: PreviewComponent
    }
  ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { enableTracing: false })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
