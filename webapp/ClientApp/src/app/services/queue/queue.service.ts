import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class QueueService {

  constructor(private readonly http: HttpClient) { }

  async joinQueue(): Promise<HttpResponse<any>> {
    return await this.http.get(`${window.location.origin}/queue/join`, { observe: 'response' }).toPromise();
  }

  async createQueue(): Promise<HttpResponse<any>> {
    return await this.http.get(`${window.location.origin}/queue/create`, { observe: 'response' }).toPromise();
  }
}
