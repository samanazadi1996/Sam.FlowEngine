import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JalaliDatePipe } from '../pipes/jalali-date.pipe';
import { GameLevelPipe } from '../pipes/game-level';


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

    JalaliDatePipe

  ]
})
export class SharedModule {}
