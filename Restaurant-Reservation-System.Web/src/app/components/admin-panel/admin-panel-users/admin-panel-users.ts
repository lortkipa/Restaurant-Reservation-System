import { CommonModule } from '@angular/common';
import { Component, computed, signal } from '@angular/core';
import { UserService } from '../../../services/user-service';
import { UserModel } from '../../../models/user-model';
import { FormsModule } from '@angular/forms';
import { LocalStorageService } from '../../../services/local-storage-service';
import { AlertService } from '../../../services/alert-service';
import { Router } from '@angular/router';
import { RouterUpgradeInitializer } from '@angular/router/upgrade';
import { Observable } from 'rxjs';
import { RoleModel, Roles } from '../../../models/role-model';

@Component({
  selector: 'app-admin-panel-users',
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-panel-users.html',
  styleUrl: './admin-panel-users.scss',
})

export class AdminPanelUsers {
  constructor(
    private userService: UserService,
    private localStorage: LocalStorageService,
    private alert: AlertService,
    private router: Router
  ) { }

  users = signal<UserModel[]>([])
  selectedUser = signal<UserModel | null>(null)
  editMode = signal<boolean>(false)

  username = signal<string>('')
  email = signal<string>('')
  password = signal<string>('')

  search = signal<string>('')
  filteredUsers = computed(() => {
    const term = this.search().toLowerCase().trim()

    if (!term) return this.users()

    return this.users().filter(u =>
      u.username.toLowerCase().includes(term) ||
      u.email.toLowerCase().includes(term)
    )
  })

  ngOnInit(): void {
    const token = localStorage.getItem('token')
    this.userService.GetAll(token != null ? token : '').subscribe((data) =>
      this.users.set(data)
    )
  }

  filterBy = signal<string>('all')
  setFilterBy(data: string) {
    this.filterBy.set(data)
  }

  rolesMap: { [key: number]: Observable<RoleModel[]> } = {};

  getUserRoles(id: number) {
    if (!this.rolesMap[id]) {
      this.rolesMap[id] = this.userService.getUserRoles(id);
    }
    return this.rolesMap[id];
  }

  addRole(id: number, roleName: string, roles: RoleModel[]) {
    const hasRole = roles.some(r => r.name === roleName);
    if (hasRole) return;

    this.alert.confirm("Do You Want to Add Role?").then((res) => {
      if (!res.isConfirmed) return;

      let roleId
      if (roleName == 'Admin')
        roleId = Roles.Admin
      else if (roleName == 'Worker')
        roleId = Roles.Worker
      else
        roleId = Roles.Customer

      console.log(roleId)

      this.userService.setUserRole(id, roleId).subscribe({
        next: () => this.alert.success("Role Added", '').then(() => {
          this.router.navigate(['/admin-panel/users']).then(() => window.location.reload());
        }),
        error: err => this.alert.error("Role Not Added", err.error.message)
      });
    })
  }

  removeRole(id: number, roleName: string, roles: RoleModel[]) {
    // Only allow removal if user actually has this role
    const hasRole = roles.some(r => r.name === roleName);
    if (!hasRole) return;

    if (roles.length < 2) {
      this.alert.error("Cannot Remove Last Role", '')
      return;
    }

    this.alert.confirm(`Do you want to remove ${roleName}?`).then(res => {
      if (!res.isConfirmed) return;

      // Map roleName to roleId
      let roleId: Roles;
      if (roleName === 'Admin') roleId = Roles.Admin;
      else if (roleName === 'Worker') roleId = Roles.Worker;
      else roleId = Roles.Customer;

      // Call backend to remove role
      this.userService.removeUserRole(id, roleId).subscribe({
        next: () => this.alert.success("Role Removed", '').then(() => {
          this.router.navigate(['/admin-panel/users']).then(() => window.location.reload());
        }),
        error: err => this.alert.error("Role Not Removed", err.error.message)
      });
    });
  }

  getAllUsers(): void {
    const token = localStorage.getItem('token')
    this.userService.GetAll(token != null ? token : '').subscribe((data) =>
      this.users.set(data)
    )
  }

  getAllCustomers(): void {
    const token = localStorage.getItem('token')
    this.userService.GetAllCustomers(token != null ? token : '').subscribe((data) =>
      this.users.set(data)
    )
  }

  openUser(user: UserModel) {
    if (user.username == this.selectedUser()?.username) {
      if (!this.editMode())
        this.selectedUser.set(null)
    } else {
      this.selectedUser.set(user)
    }
    this.editMode.set(false)
  }

  openEditUser(user: UserModel) {
    if (user.username == this.selectedUser()?.username) {
      if (this.editMode())
        this.selectedUser.set(null)
    } else {
      this.selectedUser.set(user)
    }
    this.editMode.set(true)
    this.username.set(user.username)
    this.email.set(user.email)
    this.password.set('')
  }

  closeModal() {
    this.selectedUser.set(null)
  }

  getRoleClass(role: string) {
    return {
      'role-admin': role === 'admin',
      'role-worker': role === 'worker',
      'role-customer': role === 'customer'
    };
  }

  updateUserDetails(form: any) {
    if (form.invalid) {
      if (!this.username()) this.alert.error("Update Failed", "Username is empty");
      if (!this.email()) this.alert.error("Update Failed", "Email is empty");
      if (!this.password()) this.alert.error("Update Failed", "Password is empty");
      return;
    }

    this.alert.confirm("Are You Sure?").then(conf => {
      if (!conf.isConfirmed) return;

      const user = this.selectedUser()
      if (!user) return;

      this.userService.UpdateUserProfile(
        this.localStorage.getItem('token'),
        user.id,
        {
          username: this.username(),
          email: this.email(),
          password: this.password()
        }
      ).subscribe({
        next: () => this.alert.success("Account Info Updated", '').then(() => {
          this.router.navigate(['/admin-panel/users']).then(() => window.location.reload());
        }),
        error: err => this.alert.error("Update Failed", err.error.message)
      });
    });
  }

  deleteUser(id: number) {
    this.alert.confirm("Are You Sure?").then((res) => {
      if (!res.isConfirmed) return;

      this.userService.removeUserProfile(this.localStorage.getItem('token'), id).subscribe({
        next: () => this.alert.success("Account Deleted", '').then(() => {
          this.router.navigate(['/admin-panel/users']).then(() => window.location.reload());
        }),
        error: err => this.alert.error("Account Not Deleted", '')
      });
    })
  }

}
