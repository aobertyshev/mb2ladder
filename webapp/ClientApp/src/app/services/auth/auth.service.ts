import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Register } from 'src/app/models/register';
import { Login } from 'src/app/models/login';
import { ToolsService } from '../tools/tools.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private readonly http: HttpClient, private readonly tools: ToolsService) { }

  async register(model: Register): Promise<HttpResponse<any>> {
    return await this.http.post(`${window.location.origin}/auth/register`, model, { observe: 'response' }).toPromise();
  }

  async signIn(model: Login): Promise<HttpResponse<any>> {
    return await this.http.post(`${window.location.origin}/auth/signIn`, model, { observe: 'response' }).toPromise();
  }

  isAuthenticated(): boolean {
    const cookie = this.tools.getCookie('MBIILadder.ExpiryDate');
    if (!!cookie) {
      const now = this.tools.convertDateToUTC(new Date());
      const expiry = new Date(decodeURIComponent(cookie));
      return now < expiry;
    }
    return false;
  }

  async signOut(): Promise<any> {
    this.tools.deleteCookie('MBIILadder.ExpiryDate');
    return await this.http.get(`${window.location.origin}/auth/signOut`, { observe: 'response' }).toPromise();
  }
}
