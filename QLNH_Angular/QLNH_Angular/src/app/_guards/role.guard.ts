import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';

export const roleGuard: CanActivateFn = (route, state) => {
  const _accountService = inject(AccountService);
  const toastr = inject(ToastrService);
  return _accountService.currentUser$.pipe(
    map(user => {
      if(user)
      {
        const role = _accountService.getRoleFromJwt();
        if(role)
        {
          return true;
        }
        else {
          toastr.error('You shall not pass!');
          return false;
        }
      }
      else {
        toastr.error('You shall not pass!');
        return false;
      }
    })
  )
};
