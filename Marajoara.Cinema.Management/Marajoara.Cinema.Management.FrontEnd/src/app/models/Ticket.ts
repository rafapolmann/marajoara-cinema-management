export interface TicketFlat {
    ticketID: number,
    purchaseDate: Date,
    code: string,
    seatNumber: number,
    used: boolean,
    userAccountID: number,
    userAccountName: string,
    sessionID: number,
    sessionPrice: number,
    sessionSessionDate: Date,
    sessionMovieID: number,
    sessionMovieTitle: string,
    sessionMovieIs3D: boolean,
    sessionMovieIsOriginalAudio: boolean,
    sessionCineRoomID: number,
    sessionCineRoomName: string,
}

export interface TicketCommand{
    sessionID:number,
    seatNumber:number,
    userAccountID:number,
}

