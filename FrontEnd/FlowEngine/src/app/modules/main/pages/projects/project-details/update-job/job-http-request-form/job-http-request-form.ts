import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { readonly } from '@angular/forms/signals';

@Component({
  selector: 'app-job-http-request-form',
  standalone: false,
  templateUrl: './job-http-request-form.html',
  styleUrl: './job-http-request-form.scss',
})
export class JobHttpRequestForm implements OnInit {
  httpMethods = ['Get', 'Post', 'Put', 'Delete', 'Patch', 'Options', 'Head', 'Query', 'Connect', 'Trace',]
  headers: { key: string; value: string; readonly: boolean }[] = [];

  @Input() model: any;

  ngOnInit(): void {
    this.loadHeadersFromDictionary();
  }

  private loadHeadersFromDictionary() {
    const headersDict = JSON.parse(this.model.jobParameters['Headers'] ?? '{}');

    // تبدیل دیکشنری به آرایه برای نمایش در رابط کاربری
    this.headers = Object.entries(headersDict).map(([key, value]) => ({
      key,
      value: value as string,
      readonly: true
    }));
  }

  addHeader() {
    this.headers.push({ key: '', value: '', readonly: false })
    this.onchange()
  }

  deleteHeader(item: any) {
    this.headers = this.headers.filter(p => p != item)
    this.onchange()

  }
  editHeader(item: any) {
    item.readonly = !item.readonly
    this.onchange()
  }

  onchange() {
    const headersDict: { [key: string]: string } = {};

    this.headers.forEach(header => {
      if (header.key.trim()) {
        headersDict[header.key] = header.value;
      }
    });

    this.model.jobParameters['Headers'] = JSON.stringify(headersDict);
  }


  readFromCurlSimple() {
    const curlCommand = prompt("Please paste your curl command:");

    if (!curlCommand) return;
    var url = '';

    const urlPatterns = [
      /(?:')(https?:\/\/[^'\s]+)(?:')/,
      /(?:")(https?:\/\/[^"\s]+)(?:")/,
      /(?:')(https?:\/\/localhost:\d+\/[^'\s]*)(?:')/,
      /(?:")(https?:\/\/localhost:\d+\/[^"\s]*)(?:")/,
      /(https?:\/\/localhost:\d+\/[^\s'"`]+)/,
      /(https?:\/\/[^\s'"`]+)/
    ];

    for (const pattern of urlPatterns) {
      const match = curlCommand.match(pattern);
      if (match) {
        url = match[1] || match[0];
        break;
      }
    }

    const methodMatch = curlCommand.match(/-X\s+['"]?(\w+)['"]?/);
    const method = methodMatch ? methodMatch[1].toUpperCase() : 'GET';

    const headers: Record<string, string> = {};
    const headerMatches = curlCommand.matchAll(/-H\s+['"]([^'"]+)['"]/g);
    for (const match of headerMatches) {
      const [_, header] = match;
      const [key, value] = header.split(':').map(s => s.trim());
      if (key && value) {
        headers[key] = value;
      }
    }

    let body = '';
    const dataMatch = curlCommand.match(/-d\s+['"]([\s\S]*?)['"](?=\s+-|\s*$)/);
    if (dataMatch) {
      body = dataMatch[1];
      body = body.replace(/\s*\n\s*/g, ' ').trim();
    }

    console.log("Parsed curl:", { url, method, headers, body });

    this.model.jobParameters['Headers'] = JSON.stringify(headers);
    this.model.jobParameters['Url'] = url;
    debugger
    this.model.jobParameters['Body'] = body;
    this.model.jobParameters['Method'] = this.httpMethods.filter(p => p.toLowerCase() == method.toLocaleLowerCase())[0];

    this.loadHeadersFromDictionary();


  }
}

