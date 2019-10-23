import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SigninOidcComponent } from './signin-oidc/signin-oidc.component';
import { OpenIdConnectService } from './shared/open-id-connect.service';

@NgModule({
  declarations: [
    AppComponent,
    SigninOidcComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [OpenIdConnectService],
  bootstrap: [AppComponent]
})
export class AppModule { }
