import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private storageKey = 'isLoggedIn';

  constructor() {}

  login(email: string, password: string): boolean {
    localStorage.setItem(this.storageKey, 'true');
    return true;
  }

  logout() {
    localStorage.removeItem(this.storageKey);
  }

  getAuthStatus(): boolean {
    return localStorage.getItem(this.storageKey) === 'true';
  }
}
