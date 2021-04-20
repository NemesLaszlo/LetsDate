import { HttpClient } from '@angular/common/http'
import { Component, OnInit } from '@angular/core'
import { User } from './_models/user'
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'LetsDate-Frontend'
  users: any

  constructor(private http: HttpClient, private accountService: AccountService) {}

  ngOnInit() {
    this.getUsers();
    this.setCurrentUser();
  }

  getUsers() {
    this.http.get('http://localhost:5000/api/users').subscribe(response => {
      this.users = response
    }, error => {
      console.log(error)
    })
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (user) {
      this.accountService.setCurrentUser(user);
    }
  }

}
