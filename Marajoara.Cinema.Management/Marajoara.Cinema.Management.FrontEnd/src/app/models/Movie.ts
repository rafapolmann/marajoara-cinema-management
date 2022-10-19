import {  SessionFlat } from "./Session";

export interface Movie{
    movieID:number,
    title: string,
    description: string,    
    minutes?: number,
    is3D?: boolean,
    isOriginalAudio?: boolean,
    poster?: string,
    posterFile:File,
}

export interface MovieFull{
    movieID:number,
    title: string,
    description: string,    
    minutes: number,
    is3D: boolean,
    isOriginalAudio: boolean,
    poster?: string,
    sessions:SessionFlat[],
}