import { formatNumber } from '@angular/common';
import { Inject, LOCALE_ID ,Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'hours'
})
export class HoursPipe implements PipeTransform {
  
  constructor(@Inject(LOCALE_ID) private locale:string) {}

  transform(value: number): unknown {
    
    const hours =this.format(Math.floor(value/60)); 
    const minutes = this.format(value% 60);   
    return `${hours}:${minutes}`;
  }

  private format(value: number ){
    return formatNumber(value,this.locale,'2.0');
  }

}
