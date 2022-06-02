import { AfterViewChecked, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { LoaderService } from 'src/app/Shared/Helper/loader.service';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.css']
})
export class LoaderComponent implements AfterViewChecked {

  loading: boolean = false;
  constructor(private loaderService: LoaderService, private cdRef: ChangeDetectorRef) {

  }

  ngAfterViewChecked(): void {
    this.loaderService.isLoading.subscribe((isLoading) => {
      this.loading = isLoading;
      this.cdRef.detectChanges();
    });
  }

}
