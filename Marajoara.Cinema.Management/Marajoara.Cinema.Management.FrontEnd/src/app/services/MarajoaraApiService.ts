import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders} from '@angular/common/http'
import { Observable  } from "rxjs";
import { environment } from "src/environments/environment";
@Injectable({
    providedIn:'root'    
})

export class MarajoaraApiService{
    private apiUrl = environment.baseApiUrl;
    constructor(private http:HttpClient){}


    get<T>(controllerUrl:string): Observable<T>{
        return this.http.get<T>(this.apiUrl+controllerUrl);
    }

    post<T>(controllerUrl:string,body: any): Observable<T>{
        return this.http.post<T>(this.apiUrl+controllerUrl, body);
    }    

    put<T>(controllerUrl:string,body: any): Observable<T>{
        return this.http.put<T>(this.apiUrl+controllerUrl, body);
    }    

    delete<T>(controllerUrl:string):Observable<T>{
        return this.http.delete<T>(this.apiUrl+controllerUrl);
    }
}
