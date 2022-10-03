import { Pipe, PipeTransform } from '@angular/core';

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
}
