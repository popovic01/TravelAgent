import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { User } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public user: User = new User();

  constructor(private authService: AuthService, 
    private router: Router, 
    public snackBar: MatSnackBar) { }

  ngOnInit(): void {
  }

  login() {    
    this.authService.login(this.user).subscribe(x => {
      if (x?.status == 200) {
        localStorage.setItem('token', x.transferObject);
        this.router.navigate(['/']);
      }
      else {
        this.snackBar.open(x.message, 'OK', {duration: 2500});
      }
      
    }, error => {
      this.snackBar.open(error?.error?.title, 'OK', {duration: 2500});
    });
  }


}
