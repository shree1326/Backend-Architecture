import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'customerFilter'
})
export class SearchPipe implements PipeTransform {
  transform(items: any, searchText: string): any[] {
    if(!items) return [];
    if(!searchText) return items;

    searchText = searchText.toLowerCase();

    return items.filter( (item: string) => {
      return item.toLowerCase().includes(searchText);
    });
  }
}