import { Player } from './player';

export interface Team {
    id: string;
    players: Array<Player>;
    playerIds: Array<string>;
}