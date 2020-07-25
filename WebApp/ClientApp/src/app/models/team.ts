import { Player } from './player';

export interface Team {
    id: string;
    playerIds: Array<Player>;
}