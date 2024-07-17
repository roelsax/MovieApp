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
      'block py-2 px-3 text-white bg-blue-700 rounded md:bg-transparent md:text-blue-700 md:p-0 dark:text-white md:dark:text-blue-500': routeName === this.route,
      'block py-2 px-3 text-gray-900 rounded hover:bg-gray-100 md:hover:bg-transparent md:border-0 md:hover:text-blue-700 md:p-0 dark:text-white md:dark:hover:text-blue-500 dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent': routeName != this.route,
    }
  }
}
