import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Chart, ChartConfiguration, registerables } from 'chart.js';
import { Ticket, TicketService } from '../../services/ticket-service.service';

Chart.register(...registerables)

// interface Ticket {
//   id: number;
//   title: string;
//   status: string;
//   priority: string;
//   createdAt: Date;
// }

@Component({
  selector: 'app-tickets-details',
  standalone: false,
  templateUrl: './tickets-details.component.html',
  styleUrl: './tickets-details.component.css'
})

 export class TicketsDetailsComponent implements OnInit {

  tickets: Ticket[] = [];
  @ViewChild('statusChart', { static: true }) statusChartRef!: ElementRef<HTMLCanvasElement>;
  chart!: Chart;

  constructor(private ticketService: TicketService) { }

  ngOnInit() {
    this.loadTickets();
  }

  loadTickets() {
    this.ticketService.getTickets().subscribe(t => {
      this.tickets = t;
      this.renderChart(); // ✅ render chart after tickets are loaded
    });
  }

  renderChart() {
    if (!this.statusChartRef) return; // safety check

    const openCount = this.tickets.filter(t => t.status === 'Open').length;
    const inProgressCount = this.tickets.filter(t => t.status === 'In Progress').length;
    const closedCount = this.tickets.filter(t => t.status === 'Closed').length;

    const data = {
      labels: ['Open', 'In Progress', 'Closed'],
      datasets: [{
        data: [openCount, inProgressCount, closedCount],
        backgroundColor: ['#28a745', '#ffc107', '#dc3545']
      }]
    };

    const config: ChartConfiguration = {
      type: 'pie',
      data: data,
      options: {
        responsive: true,
        maintainAspectRatio: false, // ✅ allow resizing
        plugins: { legend: { position: 'bottom' } }
      }
    };

    // Destroy existing chart if exists (prevents overlay on reload)
    if (this.chart) this.chart.destroy();

    this.chart = new Chart(this.statusChartRef.nativeElement, config);
  }}
