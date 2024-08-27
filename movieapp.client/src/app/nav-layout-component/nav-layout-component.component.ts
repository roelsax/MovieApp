import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'nav-layout-component',
  templateUrl: './nav-layout-component.component.html',
  styleUrl: './nav-layout-component.component.css'
})
export class NavLayoutComponent {
  constructor(private router: Router) {
    router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
        this.route = router.url
      }
    })
  }

  route = '';

  
  cssClasses(routeName: string): object {
    return {
      'rounded-md bg-gray-900 px-3 py-2 text-sm font-medium text-white': routeName === this.route,
      'rounded-md px-3 py-2 text-sm font-medium text-gray-300 hover:bg-gray-700 hover:text-white': routeName != this.route,
    }
  }
}
