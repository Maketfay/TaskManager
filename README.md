## Task Manager
---

#### 1 Описание проекта:
WebAPI "Диспетчер задач для команды" предоставляет возможность команде управлять задачами, распределять работу и отслеживать выполнение задач. API предоставляет возможность для:
- Создание задач и назначение их участникам команды.
- Отслеживание статуса выполнения задач.
- Подробные отчеты о задачах и выполнении работ.
- Аутентификация и авторизация пользователей.   

#### Документация API
Документация API доступна по следующему адресу: https://localhost:44309/swagger. Вы найдете здесь описание доступных эндпоинтов и сможете протестировать их прямо из браузера. 


#### Графический материал:
- [Диаграммы](https://github.com/Maketfay/TaskManager)		
- [Скриншоты](https://github.com/Maketfay/TaskManager) 



#### Готовый проект:

- [Исходный код проекта](https://github.com/Maketfay/TaskManager).
- [Скрипт создания базы данных](https://github.com/Maketfay/TaskManager).

#### Аутентификация
Для доступа к некоторым эндпоинтам API требуется аутентификация. Вы можете использовать токен JWT для аутентификации. Получите токен, отправив POST-запрос на эндпоинт /user/login с вашими учетными данными.

#### Установка

Чтобы развернуть API локально, выполните следующие шаги:

1. Убедитесь, что у вас установлен .NET Core SDK. Если нет, скачайте его с [официального сайта](https://dotnet.microsoft.com/download/dotnet).

2. Клонируйте репозиторий:

	>git clone https://github.com/Maketfay/TaskManager.git
	
3. Перейдите в директорию проекта:

	>cd code
	
4. В файле конфигурации appsettings.json настройте подключение к базе данных и другие параметры, если это необходимо.

5. Выполните [скрипт создания базы данных](https://github.com/Maketfay/TaskManager).

6. Запустите API:

	>dotnet run
	
API будет доступен по адресу http://localhost:44309.

#### Взиамодействие
Мы приветствуем вклад в развитие этого проекта. Если у вас есть идеи, предложения или исправления, пожалуйста, создайте Pull Request.

#### Лицензия
Этот проект лицензирован в соответствии с MIT License.