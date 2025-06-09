import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { AddOn, AddOnCreateDto } from './addon.model';

@Injectable({
  providedIn: 'root'
})
export class AddonService {
  private apiUrl = 'https://localhost:7266/api/AddOn';

    private getAuthHeaders(): HttpHeaders {
        const token = localStorage.getItem('authToken');
        if (!token) {
          throw new Error('Missing authentication token');
        }
        return new HttpHeaders({
          Authorization: `Bearer ${token}`,
          'Content-Type': 'application/json',
        });
      }
 

  constructor(private http: HttpClient) {}

 getAllAddons(): Observable<AddOn[]> {
   const headers = this.getAuthHeaders();
  return this.http.get<any>('https://localhost:7266/api/AddOn',{headers}).pipe(
    map(res => res?.$values ?? [])
  );
}
  addAddon(addon: AddOnCreateDto): Observable<AddOn> {
     const headers = this.getAuthHeaders();
    return this.http.post<AddOn>(this.apiUrl, addon,{headers});
  }

  updateAddon(id: number, addon: AddOnCreateDto): Observable<AddOn> {
     const headers = this.getAuthHeaders();
    return this.http.put<AddOn>(`${this.apiUrl}/${id}`, addon,{headers});
  }

  deleteAddon(id: number): Observable<void> {
     const headers = this.getAuthHeaders();
    return this.http.delete<void>(`${this.apiUrl}/${id}`,{headers});
  }
}
