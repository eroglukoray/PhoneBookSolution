# PhoneBook Microservices App

##  Projeler

- `ContactService`: Kişi ve iletişim yönetimi
- `ReportService`: Rapor üretimi (asenkron)
- `Shared`: Ortak modeller ve event tanımları

##  Swagger Uç Noktaları
- POST /api/person

- POST /api/report/request

- GET /api/report

- GET /api/report/{id}

## Teknolojiler
.NET 8

MassTransit + RabbitMQ

PostgreSQL + EF Core

CQRS yapısına uygun yapı

##  Çalıştırmak için

1. PostgreSQL ve RabbitMQ'yu docker üzerinden ayağa kaldırın 
2. Projeleri şu sırayla çalıştırın:
   - ContactService (örn. https://localhost:7095)
   - ReportService

##  Docker Compose

```yaml
version: "3.9"
services:
  postgres:
    image: postgres
    environment:
      POSTGRES_PASSWORD: yourpassword
    ports:
      - "5432:5432"

  rabbitmq:
    image: rabbitmq:3-management
    ports:
      - "5672:5672"
      - "15672:15672"
