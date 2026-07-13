import {Component, Input, input, signal} from '@angular/core';
import {Register} from '../accounts/register/register';
import {User} from '../../Types/User';

@Component({
  selector: 'app-home',
  imports: [
    Register
  ],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  protected registerMode = signal(false);

  showRegister(value: boolean) {
    this.registerMode = signal(value);
  }
}
