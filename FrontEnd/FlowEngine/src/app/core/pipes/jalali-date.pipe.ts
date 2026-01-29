import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'jalaliDate',
  standalone: true,
})
export class JalaliDatePipe implements PipeTransform {
  transform(value: any, format: string = 'yyyy/MM/dd'): string {
    if (!value) return '';

    const persianDigitsToEnglish = (n: string) =>{
          if (!n) return 0;
    const map: Record<string, string> = {
      '۰': '0', '۱': '1', '۲': '2', '۳': '3', '۴': '4',
      '۵': '5', '۶': '6', '۷': '7', '۸': '8', '۹': '9',
      // عربی-هندی (در برخی محیط‌ها ممکنه بیاد)
      '٠': '0', '١': '1', '٢': '2', '٣': '3', '٤': '4',
      '٥': '5', '٦': '6', '٧': '7', '٨': '8', '٩': '9'
    };
    return Number(n.replace(/[۰-۹٠-٩]/g, (ch) => map[ch] ?? ch));
  }

    const toFa2 = (n: Number) =>
      n.toLocaleString('fa-IR', { minimumIntegerDigits: 2, useGrouping: false });

    const date = value instanceof Date ? value : new Date(value);

    const faDate = date.toLocaleDateString('fa-IR', {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
    });

    const [year, month, day] = faDate.split('/');
    const monthNameFa = this.getMonthByIndex(persianDigitsToEnglish(month));

    const hours24 = persianDigitsToEnglish(date.toLocaleString('fa-IR', { hour: '2-digit', hour12: false }));
    const minutes = persianDigitsToEnglish(date.toLocaleString('fa-IR', { minute: '2-digit' }));
    const seconds = persianDigitsToEnglish(date.toLocaleString('fa-IR', { second: '2-digit' }));

    return format
      .replace('yyyy', year)
      .replace('MMM', monthNameFa)
      .replace('MM', month)
      .replace('dd', day)
      .replace('HH', toFa2(hours24))
      .replace('mm', toFa2(minutes))
      .replace('ss', toFa2(seconds));
  }
  getMonthByIndex(month: any): string {
    const months = [
      'فروردین', 'اردیبهشت', 'خرداد',
      'تیر', 'مرداد', 'شهریور',
      'مهر', 'آبان', 'آذر',
      'دی', 'بهمن', 'اسفند'
    ];
    const index = month - 1;
    
    return months[index];
  }

}
