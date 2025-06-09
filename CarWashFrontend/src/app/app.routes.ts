


import { Routes } from '@angular/router';
import { PackageListComponent } from './User/package-list/package-list.component';
import { PaymentComponent } from './User/payment/payment.component';
import { RoleGuard } from './core/role.guard';
import { ManagePackagesComponent } from './admin/manage-packages/manage-packages.component';
import { AddCarComponent } from './User/add-car/add-car.component';
import { AddonComponent } from './User/addon/addon.component';
import { ManageAddonsComponent } from './admin/manage-addons/manage-addons.component';
import { ManageCouponsComponent } from './admin/manage-coupons/manage-coupons.component';
import { ManageOrdersComponent } from './admin/manage-orders/manage-order.component';
import { GenerateReportComponent } from './admin/generate-report/generate-report.component';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./landing-page/landing-page.component').then(m => m.LandingPageComponent)
  },
  {
    path: 'login',
    loadComponent: () => import('./auth/login/login.component').then(m => m.LoginComponent),
  },
  {
    path: 'signup',
    loadComponent: () => import('./auth/signup/signup.component').then(m => m.SignupComponent),
  },
  {
    path: 'user-dashboard',
    loadComponent: () => import('./User/user-dashboard/user-dashboard.component').then(m => m.UserDashboardComponent),
  },
  {
    path: 'add-car',
    loadComponent: () => import('./User/add-car/add-car.component').then(m => m.AddCarComponent),
  },
  {
    path: 'package-list',
    component: PackageListComponent
  },
  {
    path: 'addons',
    loadComponent: () => import('./User/addon/addon.component').then(m => m.AddonComponent)
  },
  {
    path: 'select-coupon',
    loadComponent: () => import('./User/coupon/coupon.component').then(m => m.SelectCouponComponent)
  },
  {
    path: 'confirm-order',
    loadComponent: () => import('./User/confirm-order/confirm-order.component').then(m => m.ConfirmOrderComponent)
  },
  { path: 'payment', component: PaymentComponent },
 

{
  path: 'admin-dashboard',
  canActivate: [RoleGuard],
  loadComponent: () => import('./admin/admin-dashboard/admin-dashboard.component').then(m => m.AdminDashboardComponent)
},
{
  path: 'manage-users',
  loadComponent: () => import('./admin/manage-customers/manage-customers.component').then(m => m.ManageCustomersComponent),
  canActivate: [RoleGuard]
}
,{
  path: 'manage-washers',
  loadComponent:() => import('./admin/manage-washers/manage-washers.component').then(m =>m.ManageWashersComponent)
  
},
{
  path: 'packages',
  component: ManagePackagesComponent // if standalone, import it directly here or lazy load
},
{
  path: 'manage-addons',
  component:ManageAddonsComponent
}
,
{
  path: 'manage-coupons',
  component:ManageCouponsComponent
}
,
{
  path:'manage-order',
  component:ManageOrdersComponent
},
{
  path:'report-generate',
  component:GenerateReportComponent
},
{
  path: 'washer-dashboard',
  canActivate: [RoleGuard],
  loadComponent: () => import('./washer/washer-dashboard/washer-dashboard.component').then(m => m.WasherDashboardComponent)
},
{
    path: 'washer/profile',
    canActivate: [RoleGuard],
    loadComponent: () => import('./washer/washer-profile/washer-profile.component').then(m => m.WasherProfileComponent)
  },
  {
    path: 'washer/available-orders',
    canActivate: [RoleGuard],
    loadComponent: () => import('./washer/available-orders/available-orders.component').then(m => m.AvailableOrdersComponent)
  },
  {
    path: 'washer/current-orders',
    canActivate: [RoleGuard],
    loadComponent: () => import('./washer/current-orders/current-orders.component').then(m => m.CurrentOrdersComponent)
  }


];
