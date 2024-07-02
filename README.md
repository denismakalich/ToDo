# To-Do List Management API

Этот микросервис на .NET предоставляет REST API для управления списком задач (to-do list). API позволяет добавлять, изменять, удалять и получать задачи, а также поддерживает фильтрацию, сортировку и пагинацию.

## Оглавление

- Функциональные возможности
- Технические требования
- Использование API
  - Регистрация пользователя
  - Авторизация пользователя
  - Добавление новой задачи
  - Получение задачи по Id
  - Изменение задачи
  - Удаление задачи
  - Получение списка всех задач
    
## Функциональные возможности

API поддерживает следующие операции:

1. Регистрация пользователя
2. Авторизация пользователя
3. Получение списка всех задач с возможностью фильтрации по статусу и сортировки по приоритету и датам.
4. Получение задачи по Id.
5. Добавление новой задачи.
6. Изменение задачи.
7. Удаление задачи.

## Технические требования

- ASP.NET Core (последняя стабильная версия).
- Entity Framework Core для хранения данных.
- Поддержка пагинации для списка задач.
- Обработка ошибок и валидация входных данных.
- Логирование основных операций с задачами.
- Docker для контейнеризации приложения.

## Использование API

### Регистрация пользователя

- **Endpoint**: `POST /api/users/register`
- **Описание**: Регистрация нового пользователя в системе
- **Тело запроса**:
- `email` (string): Адрес электронной почты пользователя
- `password` (string): Пароль пользователя

  ```json
  {
    "email": "testUserEmail@example.com",
    "password": "testPass",
  }
  ```

- **Ответ**:
- `id` (guid): Идентификатор пользователя в системе
- `jwtToken` (string): Токен для возможности пользоваться системой

  ```json
  {
  "id": "5148b659-cb97-4bf6-bb71-3c80b34cd19a",
  "jwtToken":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiNTE0OGI2NTktY2I5Ny00YmY2LWJiNzEtM2M4MGIzNGNkMTlhIiwiZXhwIjoxNzE5ODcwMzE2LCJpc3MiOiJNeUF1dGhTZXJ2ZXIiLCJhdWQiOiJNeUF1dGhDbGllbnQifQ.oSCNTATZTynxH0jGa4ckU2Yr_RkT7r0tIshhZ4rCZtA"
  }
  ```
![image](https://github.com/denismakalich/ToDo/assets/88092446/57918447-a47a-4b2a-8d43-196aaa27b776)

### Авторизация пользователя

- **Endpoint**: `POST /api/users/login`
- **Описание**: Авторизация пользователя в системе
- **Тело запроса**:
- `email` (string): Адрес электронной почты пользователя
- `password` (string): Пароль пользователя

  ```json
  {
    "email": "testUserEmail@example.com",
    "password": "testPass",
  }
  ```

- **Ответ**:
- `id` (guid): Идентификатор пользователя в системе
- `jwtToken` (string): Токен для возможности пользоваться системой

```json
  {
  "id": "5148b659-cb97-4bf6-bb71-3c80b34cd19a",
  "jwtToken":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiNTE0OGI2NTktY2I5Ny00YmY2LWJiNzEtM2M4MGIzNGNkMTlhIiwiZXhwIjoxNzE5ODcwMzE2LCJpc3MiOiJNeUF1dGhTZXJ2ZXIiLCJhdWQiOiJNeUF1dGhDbGllbnQifQ.oSCNTATZTynxH0jGa4ckU2Yr_RkT7r0tIshhZ4rCZtA"
  }
```
![image](https://github.com/denismakalich/ToDo/assets/88092446/90a38980-3770-42a3-92d6-18e11031c46b)

### Добавление новой задачи

- **Endpoint**: `POST /api/tasks/create`
- **Описание**: Добавление новой задачи
- **Тело запроса**:
- `title` (string): Название задачи
- `description` (string): Описание задачи
- `priority` (int): Приоритет задачи, представленный целым числом от 0 до 10
- `status` (enum): Текущий статус задачи, принимающий одно из значений: New, InProgress, Completed
- `userId` (guid): Идентификатор пользователя, кому принадлежит задача

  ```json
   {
  "title": "Пример названия задачи",
  "description": "Пример описания задачи",
  "priority": 4,
  "status": "New",
  "userId": "5148b659-cb97-4bf6-bb71-3c80b34cd19a"
  }
  ```

- **Ответ**:
Статус 204 - успешное добавление задачи

![image](https://github.com/denismakalich/ToDo/assets/88092446/2d3dc6bc-fbf0-4123-a8e7-59d1c071f588)

### Получение задачи по Id

- **Endpoint**: `GET /api/tasks/get/{id}`
- **Описание**: Получение задачи по id
- **Тело запроса**:
- `id` (guid): Идентификатор пользователя, кому принадлежит задача

  ```json
  {
    "id": "5148b659-cb97-4bf6-bb71-3c80b34cd19a"
  }
  ```

- **Ответ**:
- `id` (guid): Идентификатор задачи
- `title` (string): Название задачи
- `description` (string): Описание задачи
- `prioriry` (int): Приоритет задачи
- `status` (enum): Статус задачи
- `createdOn` (DateTimeOffSet): Дата создания задачи
- `modifiedOn` (DateTimeOffSet): Дата изменения задачи
- `userId` (guid): Идентификатор пользователя, кому принадлежит задача

```json
 {
  "id": "d354d47d-c8d6-47ef-92c9-a467a11c4a69",
  "title": "TestTaskItem1",
  "description": "Test description for postman",
  "priority": 2,
  "status": "New",
  "createdOn": "2024-07-01T20:23:43.863843+00:00",
  "modifiedOn": "2024-07-01T20:23:43.864442+00:00",
  "userId": "5148b659-cb97-4bf6-bb71-3c80b34cd19a"
}
```
![image](https://github.com/denismakalich/ToDo/assets/88092446/d991fc1f-d5d2-4a14-bf9f-c208dd6aad27)

### Изменение задачи

- **Endpoint**: `PUT /api/tasks/update/{id}`
- **Описание**: Обновление задачи
- **Тело запроса**:
- `title` (string): Название задачи
- `description` (string): Описание задачи
- `priority` (int): Приоритет задачи, представленный целым числом от 0 до 10
- `status` (enum): Текущий статус задачи, принимающий одно из значений: New, InProgress, Completed

  ```json
   {
  "title": "Пример названия задачи",
  "description": "Пример описания задачи",
  "priority": 4,
  "status": "New",
  }
  ```

- **Ответ**:
Статус 204 - успешное обновление задачи
![image](https://github.com/denismakalich/ToDo/assets/88092446/79cc803b-be6e-433e-a1c5-23d60f6f30aa)

### Поиск задач

- **Endpoint**: `GET /api/tasks/search`
- **Описание**: Получение задач по поиску
- **Параметры запроса**:
- `page` (int): Номер страницы
- `pageSize` (int): Количество записей на странице
- `status` (enum): Фильтрация по сортировке: New, InProgress, Completed
- `userId` (guid): Фильтрация по задачам конкретного пользователя
- `sortBy` (enum): По какому полю сортируется список задач: Priority, CreatedOn, ModifiedOn

  ```json
  {
    "page": 0,
    "pageSize": 20,
    "status": "New",
    "userId": "5148b659-cb97-4bf6-bb71-3c80b34cd19a",
    "sortBy": "Priority"
  }
  ```

- **Ответ**:
- Список задач:
- `id` (guid): Идентификатор задачи
- `title` (string): Название задачи
- `description` (string): Описание задачи
- `prioriry` (int): Приоритет задачи
- `status` (enum): Статус задачи
- `createdOn` (DateTimeOffSet): Дата создания задачи
- `modifiedOn` (DateTimeOffSet): Дата изменения задачи
- `userId` (guid): Идентификатор пользователя, кому принадлежит задача

```json
 {
  "taskItems": [
        {
            "id": "dff0f111-b52c-4c57-906b-af2d8decfce1",
            "title": "string",
            "description": "string",
            "priority": 0,
            "status": "New",
            "createdOn": "2024-07-01T21:01:49.309627+00:00",
            "modifiedOn": "2024-07-01T21:01:49.309632+00:00",
            "userId": "5148b659-cb97-4bf6-bb71-3c80b34cd19a"
        },
        {
            "id": "d354d47d-c8d6-47ef-92c9-a467a11c4a69",
            "title": "TestTaskItem1",
            "description": "Test description for postman",
            "priority": 2,
            "status": "New",
            "createdOn": "2024-07-01T20:23:43.863843+00:00",
            "modifiedOn": "2024-07-01T20:23:43.864442+00:00",
            "userId": "5148b659-cb97-4bf6-bb71-3c80b34cd19a"
        },
        {
            "id": "4cacb91c-a17a-4082-aa3b-ca7c430ef5ac",
            "title": "TestTaskItem1",
            "description": "Test description for postman",
            "priority": 2,
            "status": "New",
            "createdOn": "2024-07-01T21:03:00.317401+00:00",
            "modifiedOn": "2024-07-01T21:03:00.317405+00:00",
            "userId": "5148b659-cb97-4bf6-bb71-3c80b34cd19a"
        }
    ]
}
```
![image](https://github.com/denismakalich/ToDo/assets/88092446/91bf656d-e571-498c-8f43-505241a2857c)

### Удаление задачи

- **Endpoint**: `DELETE /api/tasks/delete/{id}`
- **Описание**: Удаление задачи
- **Параметры**:
- `id` (guid): Идентификатор задачи

  ```json
   {
    "id": "dff0f111-b52c-4c57-906b-af2d8decfce1",
   }
  ```

- **Ответ**:
Статус 204 - успешное удаление задачи
![image](https://github.com/denismakalich/ToDo/assets/88092446/83d12dc0-9609-48ec-b6b6-290b601d03da)
