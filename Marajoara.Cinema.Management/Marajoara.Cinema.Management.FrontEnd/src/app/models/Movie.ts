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