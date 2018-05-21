import { CommonModule } from '@angular/common';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MatButtonModule, MatCardModule, MatListModule, MatIconModule, MatToolbarModule, MatProgressSpinnerModule, MatSnackBarModule, MatFormFieldModule, MatInputModule, MatChipsModule, MatTabsModule } from '@angular/material';
import { MomentModule } from 'ngx-moment';
import { NgxEditorModule } from 'ngx-editor';

import { SharedComponents, SharedEntryComponents } from './components';

const SHARED_PROVIDERS = [
];

const SHARED_MODULES = [
  FormsModule,
  ReactiveFormsModule,
  MomentModule,
  NgxEditorModule,
  MatDialogModule,
  MatCardModule,
  MatButtonModule,
  MatListModule,
  MatIconModule,
  MatToolbarModule,
  MatProgressSpinnerModule,
  MatSnackBarModule,
  MatFormFieldModule,
  MatInputModule,
  MatChipsModule,
  MatTabsModule
];

const SHARED_DECLARATIONS = [
  ...SharedComponents
];

@NgModule({
  imports: [
    CommonModule,
    ...SHARED_MODULES
  ],
  declarations: SharedComponents,
  entryComponents: SharedEntryComponents,
  exports: [
    ...SHARED_MODULES,
    ...SHARED_DECLARATIONS
  ],
  providers: [...SHARED_PROVIDERS]

})
export class SharedModule { }