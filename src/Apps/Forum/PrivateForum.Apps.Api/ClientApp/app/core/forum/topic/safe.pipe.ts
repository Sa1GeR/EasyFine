import { Pipe, PipeTransform } from '@angular/core';
import { SafeHtml, DomSanitizer } from '@angular/platform-browser';

@Pipe({ name: 'safe', pure: false })
export class SafePipe implements PipeTransform {
    constructor(private sanitizer: DomSanitizer) { }
    transform(html) {
        return this.sanitizer.bypassSecurityTrustHtml(html);
    }
} 