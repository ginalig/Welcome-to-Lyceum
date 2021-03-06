# Александр Нигай - "Welcome to Lyceum!"

## Группа: 10 - И - 1

## Электронная почта: nigai_sasha@mail.ru

## VK: www.vk.com/matkacharogeyka  

### [ Сценарий 1 - Выход из игры ]

1. Игрок нажимает на кнопку "Escape" или на кнопку на экране
2. Программа останавливает внутреигровое время
3. Загрузка сцены меню
4. Анимация возникновения текста на экране
5. Игрок наводит мышкой на кнопку "Выйти из игры"
6. Возникает анимация кнопки, показывающая что пользователь на нее навелся
7. Игрок нажимает на кнопку
8. Появлется окно с подтверждением выхода с кнопками "Да" и "Нет"
9. Если игрок нажимает "Нет", окно закрывается
10. Если игрок нажимает "Да", игровой процесс сохраняется в отдельный файл
11. Игра завершает процесс

### [ Сценарий 2 - Новая игра ]

1. После запуска игры в главном меню игрок наводится на кнопку "Новая игра"
2. Возникает анимация кнопки, показывающая что пользователь на нее навелся
3. Игрок нажимает на кнопку
4. Создается новый файл для сохранения текущего игрового процесса
5. Происходит анимация перехода в сцену загрузки
6. Загружается первый уровень игры
7. Переход на первый уровень

### [ Сценарий 3 - Выполнение задач ]

1. Игрок нажимает кнопку "Tab" или на кнопку открытия дневника на экране
2. Справа на экране открывается окно дневника
3. В окне загружается очередь актуальных задач
4. Кликнув по задаче, игрок сможет посмотреть место выполенения квеста
5. Пройдя в место квеста, активируется триггер, начинающий задание
6. При активации триггера, загружается сцена задания
7. После выполнения задания загржается предыдущая сцена
8. В дневнике выполненное задание помечается как "выполненное"

### [ Сценарий 4 - Обучение игрока ]

1. При загрузке первого уровня появляется окно, рекомендующее пройти обучение
с двумя кнопками "Пройти обучение" и "Пропустить обучение"
2. Если игрок пропускает обучение, загружается следующий уровень
3. Если игрок решает пройти обучение, на экране появляется "Куратор", помощник игрока, дающий советы и подсказки во время прохождения
4. Под "Куратором" появляется текстовое поле, в котором он сообщает игроку на какие кнопки двигаться и куда ему следует пройти
5. С помощью кнопок перемещения игрок передвигается в заданную точку
6. "Куратор" поздравляет игрока, а затем переходит к следующему этапу обучения
7. Аналогично "Куратор" обучает игрока взаимодействовать с окружением (Открывать двери, здороваться с другими лицеистами) и знакомит с виртуальным дневником
8. После прохождения обучения "Куратор" поздравляет игрока, загружается следующий уровень

### [ Сценарий 5 - Изменение уровня громкости ]

1. Игрок нажимает на кнопку "Escape" или на кнопку на экране
2. Программа останавливает внутреигровое время
3. Загрузка сцены меню
4. Анимация возникновения текста на экране
5. Игрок нажимает на кнопку "Настройки"
6. Переход во вкладку настроек, в которой есть кнопка "Звук"
7. Игрок нажимает на кнопку "Звук"
8. Переход во вкладку настроек звука, в которой есть ползунок уровня громкости
9. При изменении положения ползунка меняется уровень громкости

### [ Сценарий 6 - Передвижение игрока ]

1. Игрок нажимает на кнопки передвижения персонажа, по умолчанию кнопки - WASD
2. Активный компонент "PlayerMovement" (скрипт, отвечающий за передвижение) игрового объекта "Player" обрабатывает нажатие кнопок и положение курсора мыши
3. В зависимости от положения курсора персонаж меняет направление взгляда
4. В зависимости от нажатой кнопки на клавиатуре задается вектор направления игрока
5. Заданный вектор умножается на скорость игрока
6. Поле "velocity" компонента "RigidBody2D" (этот компонент отвечает за физические свойтсва игрового объекта) приравнивается к заданному вектору, это приводит к движению игрока
7. При ненулевом значении "velocity" запускается анимация ходьбы, при нулевом значении анимация прекращается
8. На каждый второй кадр анимации ходьбы воспроизводится звук шага

### [ Сценарий 7 - Приближение к "Стрессу"*]

*"Стресс" - один из внутриигровых противников

1. Во время загрузки сцены также загружаются и представители стресса
2. После загрузки у них запускается анимация покоя
3. При приближении игрока к противнику, активируется триггер
4. Триггер запускает анимацию плевка у стресса
5. Во время каждой итерации цикличной анимации компонент "StressEnemy" спавнит сам плевок перед врагом
6. Во время спавна плевка ему задается направление, в котором нужно лететь
7. При спавне у плевка запускается цикличная анимация вращения
8. Также воспроизводится звук плевка
9. При попадании в цель плевок запустит анимацию попадания и уничтожится, в противном случае плевок уничтожится в течение нескольких секунд
