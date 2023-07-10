import { Injectable, inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { Observable, map } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const _accountService = inject(AccountService);
  const toastr = inject(ToastrService);
  return _accountService.currentUser$.pipe(
    map(user => {
      if(user) return true;
      else {
        toastr.error('You shall not pass!');
        return false;
      }
    })
  )
}
