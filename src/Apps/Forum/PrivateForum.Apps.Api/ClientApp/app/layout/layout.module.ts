import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MatCardModule, MatButtonModule } from "@angular/material";

import { SharedModule } from "../shared";
import { HeaderComponent } from "./header/header.component";
import { FooterComponent } from "./footer/footer.component";
import { LayoutComponent } from "./layout.component";


const LayoutComponents = [
  HeaderComponent,
  FooterComponent,
  LayoutComponent
]

@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  declarations: [
    ...LayoutComponents
  ],
  exports: [
    ...LayoutComponents
  ]
})
export class LayoutModule { }