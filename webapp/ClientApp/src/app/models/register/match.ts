import { Team } from './team';

export interface Match {
    id: string;
    teams: Array<Team>;
    maps: Array<string>;
    date: Date;
    dateCreated: Date;
    dateUpdated: Date;
    score: string;
}