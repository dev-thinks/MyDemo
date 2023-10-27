import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { config } from 'src/assets/config';

/**
 * ^The generate PDF service for call API to generate PDF
 */
@Injectable({
  providedIn: 'root',
})
export class GeneratePDFService {
  constructor(protected http: HttpClient) {
  }

  /**
   * Generate the TP Grade Appendix PDF
   * @returns
   */
  public generatePDF() {
    var url = config.apiUrl + "/generatePDF/generate";
    return this.http.get(url, { responseType: 'blob' });
  }
}
