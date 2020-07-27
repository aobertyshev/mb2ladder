import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Match } from 'src/app/models/match';

@Injectable({
  providedIn: 'root'
})
export class MatchService {

  constructor(private readonly http: HttpClient) { }

  async createMatch(match: Match): Promise<HttpResponse<any>> {
    return await this.http.post(`${window.location.origin}/matches/create`, match, { observe: 'response' }).toPromise();
  }

  async getMatches(): Promise<Array<Match>> {
    const matches = (await this.http.get<Array<Match>>(`${window.location.origin}/matches/list`, { observe: 'response' }).toPromise()).body;
    matches.forEach(match => {
      match.date = new Date(match.date);
      match.dateCreated = new Date(match.dateCreated);
      match.dateUpdated = new Date(match.dateUpdated);
    })
    return matches;
  }
}
