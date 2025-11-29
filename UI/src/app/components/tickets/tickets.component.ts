import { Component, OnInit } from '@angular/core';
import { TicketService, Ticket, TicketCreate, TicketUpdate } from '../../services/ticket-service.service';

@Component({
  selector: 'app-tickets',
  standalone: false,
  templateUrl: './tickets.component.html',
  styleUrl: './tickets.component.css'
})
export class TicketsComponent implements OnInit {
  // serialNum =;
  numbers: number[] = [10,20,30,40,50];
  tickets: Ticket[] = [];
  filteredTickets: Ticket[] = [];

  newTicket: TicketCreate = { title: '', status: '', priority: '' };
  showAddForm: boolean = false;

  editingTicketId: number | null = null;
  editedTicket: TicketUpdate = { title: '', status: '', priority: '' };

  selectedStatusFilter: string = 'All';
  searchQuery: string = '';

  constructor(private ticketService: TicketService) { }

  ngOnInit() {
    this.loadTickets();
  }

  loadTickets() {
    this.ticketService.getTickets().subscribe(t => {
      this.tickets = t;
      this.applyFilters();
    });
  }

  toggleAddForm() {
    this.showAddForm = !this.showAddForm;
  }

  addTicket() {
    if (!this.newTicket.title.trim()) return;

    this.ticketService.addTicket(this.newTicket).subscribe(() => {
      this.loadTickets();
      this.showAddForm = false;
      this.newTicket = { title: '', status: '', priority: '' };
    });
  }

  cancelAdd() {
    this.newTicket = { title: '', status: '', priority: '' };
    this.showAddForm = false;
  }

  startEdit(ticket: Ticket) {
    this.editingTicketId = ticket.id;
    this.editedTicket = {
      title: ticket.title,
      status: ticket.status,
      priority: ticket.priority
    };
  }

  cancelEdit() {
    this.editingTicketId = null;
    this.editedTicket = { title: '', status: '', priority: '' };
  }

  saveEdit(ticketId: number) {
    if (!this.editedTicket.title.trim()) return;

    this.ticketService.updateTicket(ticketId, this.editedTicket).subscribe(() => {
      this.loadTickets();
      this.cancelEdit();
    });
  }

  deleteTicket(ticketId: number) {
    if (confirm('Are you sure you want to delete this ticket?')) {
      this.ticketService.deleteTicket(ticketId).subscribe(() => {
        this.loadTickets();
      });
    }
  }

  applyFilters() {
    let filtered = [...this.tickets];

    if (this.selectedStatusFilter !== 'All') {
      filtered = filtered.filter(t => t.status === this.selectedStatusFilter);
    }

    if (this.searchQuery.trim()) {
      const q = this.searchQuery.toLowerCase();
      filtered = filtered.filter(t =>
        t.title.toLowerCase().includes(q) || t.priority.toLowerCase().includes(q)
      );
    }

    this.filteredTickets = filtered;
  }

  onStatusFilterChange() {
    this.applyFilters();
  }
}
