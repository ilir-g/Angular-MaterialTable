import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

const baseUrl = 'https://localhost:44324/api/bookentity';

@Injectable({
  providedIn: 'root'
})
export class BookEntitiesService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<any> {
    return this.http.get(`${baseUrl}/BookEntitiesList`);
  }

  getById(id): Observable<any> {
    return this.http.get(`${baseUrl}/GetBookById/${id}`);
  }

  create(data): Observable<any> {
    return this.http.post(`${baseUrl}/CreateBookEntity`, data);
  }

  update(id, data): Observable<any> {
    return this.http.put(`${baseUrl}/${id}`, data);
  }

  delete(id): Observable<any> {
    return this.http.delete(`${baseUrl}/${id}`);
  }

}

