import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JalaliDatePipe } from '../pipes/jalali-date.pipe';
import { GameLevelPipe } from '../pipes/game-level';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTreeModule } from '@angular/material/tree';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    JalaliDatePipe,
    GameLevelPipe
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatTreeModule,
    MatIconModule,
    MatButtonModule,

    JalaliDatePipe

  ]
})
export class SharedModule {}
