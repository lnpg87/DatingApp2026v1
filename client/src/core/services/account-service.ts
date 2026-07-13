import {inject, Service, signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {RegisterCreds, User} from '../../Types/User';
import {tap} from 'rxjs';

@Service()
export class AccountService {
  private http = inject(HttpClient);
  currentUser = signal<User | null>(null);

  baseUrl = 'https://localhost:5001/api/';

  register(creds: RegisterCreds){
    return this.http.post<User>(this.baseUrl + 'account/register', creds).pipe(
      tap(user => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  login(creds:any){
    return this.http.post<User>(this.baseUrl+'account/login', creds).pipe(
      tap(user => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  setCurrentUser(user: User){
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUser.set(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
}
