import { Component, ViewChild, ViewContainerRef } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './header/header.component';
import { AlertService } from './uiService/alert.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  @ViewChild('alertContainer', { read: ViewContainerRef })
  vcRef!: ViewContainerRef;
  title = 'TodoList';
  constructor(private alertService: AlertService) {}

  ngAfterViewInit() {
    this.alertService.registerContainer(this.vcRef);
  }
}
