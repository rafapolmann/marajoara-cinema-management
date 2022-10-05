import { Pipe, PipeTransform } from '@angular/core';
import { formatDate } from '@angular/common';

@Pipe({
  name: 'dateTimeCustomFormat',
  pure: true,
})
export class DateTimeCustomFormat implements PipeTransform {

  transform(value: Date): string {
    const parsedDate = new Date(value);
    return parsedDate.toLocaleString([], {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit',
    });
  }

  transformToTime(value: Date): string {
    const parsedDate = new Date(value);
    return parsedDate.toLocaleString([], {
      hour: '2-digit',
      minute: '2-digit',
    });
  }

  transformToDate(value: Date): string {
    const parsedDate = new Date(value);
    return parsedDate.toLocaleString([], {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
    });
  }

  transformToISOString(value: Date): string {
    return formatDate(value, "yyyy-MM-ddTHH:mm:ss", "en-US");
  }

  getFullDate(baseDate: Date, time: string): Date {
    const _date: Date = new Date(baseDate);
    const timeProperties: string[] = time.split(':');
    const fullDate = new Date(
      _date.getFullYear(),
      _date.getMonth(),
      _date.getDate(),
      Number(timeProperties[0]),
      Number(timeProperties[1]),
      0,
      0
    );

    return fullDate;
  }
}
