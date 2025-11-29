import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Ticket {
  id: number;
  // slNo: number;
  title: string;
  status: string;
  priority: string;
  createdAt: string;
}

export interface TicketCreate {
  title: string;
  status: string;
  priority: string;
}

export interface TicketUpdate {
  title: string;
  status: string;
  priority: string;
}

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  private apiUrl = 'https://localhost:7124/api/Tickets'; // remove trailing slash

  constructor(private http: HttpClient) { }

  getTickets(): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(`${this.apiUrl}/all`);
  }

  getTicket(id: number): Observable<Ticket> {
    return this.http.get<Ticket>(`${this.apiUrl}/by/${id}`);
  }

  addTicket(ticket: TicketCreate): Observable<Ticket> {
    return this.http.post<Ticket>(`${this.apiUrl}/add`, ticket);
  }

  updateTicket(id: number, ticket: TicketUpdate): Observable<Ticket> {
    return this.http.put<Ticket>(`${this.apiUrl}/update/${id}`, ticket);
  }

  deleteTicket(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/delete/${id}`);
  }
}
