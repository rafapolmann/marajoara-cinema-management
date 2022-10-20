import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AccessLevel } from 'src/app/models/UserAccount';
import { AuthenticationService } from 'src/app/services/authentication.service';


@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private authService: AuthenticationService,
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const user = this.authService.authorizedUserAccount;
        const routeRoles: AccessLevel[] = route.data['role'];
        const hasRoles = !!routeRoles;
        var canActivate: boolean;
        if (user) {
            if (hasRoles)
                canActivate = !!routeRoles.find(lvl => lvl == user.level)
            else
                canActivate = true;
        } else {
            canActivate = false
        }
        if(!canActivate)
            this.authService.logout();
        return canActivate;
    }
}