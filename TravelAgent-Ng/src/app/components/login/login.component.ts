import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  login() {    
    this.authService.login(this.user).subscribe(x => {
      if (x?.status == 200) {
        localStorage.setItem('token', x.transferObject);
        this.router.navigate(['/']);
      }
      else {
        this.toastr.error(x.message);
      }
      
    }, error => {
      this.toastr.error(error?.error?.title);
    });
  }

}
