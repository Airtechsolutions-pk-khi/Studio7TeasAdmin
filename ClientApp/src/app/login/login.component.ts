import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LoginService } from '../_services/login.service';
import { first } from 'rxjs/operators';
import { LocalStorageService } from '../_services/local-storage.service';
import { ToastService } from '../_services/toastservice';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  constructor(private formBuilder: FormBuilder,
    public service: LoginService,
    public ts: ToastService,
    private router: Router,
    private ls: LocalStorageService) { }

  ngOnInit() {
    this.createForm();
  }
  onSubmit() {

    this.loginForm.markAllAsTouched();
    if (this.loginForm.invalid) {
      return;
    }

    
    this.service.login(this.f.username.value, this.f.password.value)
      .pipe(first())
      .subscribe(
        data => {
          
          if (data != null) {
            
            this.loadLocations(data["brandID"]);
            this.ls.setSelectedBrand(data);
            
      
          }
          else {
            this.ts.showError("Error", "Username or password is not correct.");
          }

        },
        error => {
          this.ts.showError("Error", "Something went wrong.");
        });  
  }

  get f() { return this.loginForm.controls; }
  private createForm() {

    this.loginForm = this.formBuilder.group({

      username: ['', Validators.required],
      password: ['', Validators.required],

    });
  }
  private loadLocations(bid) {
    
    this.service.getAllLocations(bid).subscribe((res: any) => {
      
      if(res.length>0){
        this.ls.setLocation(res);
        this.ls.setSelectedLocation(res[0]);
        this.router.navigate(["/admin/dashboard"]);
      }
      else{
        this.ls.setLocation(null);
        this.ls.setSelectedLocation(null);
      }
    });
  }

 
}
