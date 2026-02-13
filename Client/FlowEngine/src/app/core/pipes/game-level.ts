import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'gameLevel',
    standalone: true,
})
export class GameLevelPipe implements PipeTransform {
    transform(value: any): string {
        if (!value) return '';

        var data = [
            "",
            "ساده",
            "متوسط",
            "سخت",
            "حرفه‌ای",
            "افسانه‌ای"]

        return data[value];

    }
}
