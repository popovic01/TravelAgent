import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { User } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  public user: User = new User();

  constructor(private authService: AuthService, 
    private router: Router, 
    public snackBar: MatSnackBar) { }

  ngOnInit(): void {
  }

  register() {    
    this.authService.register(this.user).subscribe(x => {
      if (x?.status == 200) {
        localStorage.setItem('token', x.transferObject);
        this.router.navigate(['/']);
      }
      else {
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
      }
      
    }, error => {
      this.snackBar.open(error?.message, 'OK', {duration: 2500});
    });
  }

}
