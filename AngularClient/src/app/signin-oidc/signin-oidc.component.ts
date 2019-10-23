import { Component, OnInit } from '@angular/core';
import { OpenIdConnectService } from '../shared/open-id-connect.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signin-oidc',
  templateUrl: './signin-oidc.component.html',
  styleUrls: ['./signin-oidc.component.css']
})
export class SigninOidcComponent implements OnInit {

  constructor(private openIdConnectService: OpenIdConnectService,
              private router: Router) { }

  ngOnInit() {
    this.openIdConnectService.handleCallback();
    this.router.navigate(['./']);
  }

}
