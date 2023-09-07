import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationComponent } from './containers/navigation/navigation.component';
import { NavigationHttpService } from './services/navigation-http-service';
import { CarouselModule } from 'primeng/carousel';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [NavigationComponent],
  imports: [CommonModule, HttpClientModule, CarouselModule],
  exports: [NavigationComponent],
  providers: [NavigationHttpService],
})
export class NavigationModule {}
