import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Match } from 'src/app/models/register/match';

@Injectable({
  providedIn: 'root'
})
export class MatchService {

  constructor(private readonly http: HttpClient) { }

  async createMatch(match: Match): Promise<HttpResponse<any>> {
    return await this.http.post(`${window.location.origin}/matches/create`, match, { observe: 'response' }).toPromise();
  }
}
