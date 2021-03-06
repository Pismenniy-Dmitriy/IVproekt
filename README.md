1)Введение:
"Программа для телефонов на базе Android, позволяющая мгновенно начать съемку видео при быстром изъятии телефона из кармана, с последующей загрузкой видео на облачное хранилище(Google Drive)" 

2)Назначение разработки:
Упрощение и автоматизация экстренной съемки видео с дальнейшей загрузкой на облачный ресурс

3)Требования к программе или программному изделию:
3.1) Требования к функциональным характеристикам:
Программа должна работать в фоне и отслеживать показатели гироскопа(даже в заблокированном состоянии), и при резком изъятии телефона из кармана автоматически начать запись видео. После окончания записи видео загружается на облачное хранилище Google Drive(при условии включенного интернета и авторизации пользователя в Google Drive на мобильном устройстве)
3.2) Требования к информационной и программной совместимости:
Программа совместима с устройствами, работащими на базе Android 5.0+, при отсутствии блокировок пользователя(или иных проблемах) в облачном хранилище Google Drive

4)Стадии и этапы разработки:
- Знакомство с Xamarin.Forms, определение общего объема работы и равномерное распределение нагрузки на всех участников разработки.
- Подключение всех необходимых библиотек и настройка Xamarin.Forms для Visual Studio
- Реализация работы гироскопа (пробуждение, считывание с него данных, отслеживание "резких" движений телефона, которые в последствии должны начать запись видео)
- Реализация записи видео (подключение камеры, съемка и сохранение видео в локальное хранилище на устройстве)
- Реализация загрузки видео на облачное хранилище Google Drive (после записи видео, получившийся файл загружается в хранилище Google Drive пользователя, при условии заблаговременной авторизации на устройстве)
- Реализация работы программы в фоновом режиме (добавление ряда функций, которые с периодичностью раз в  ~3 секунды будут "пробуждать" гироскоп и считывать с него данные, даже в заблокированном состоянии устройства)
- Сборка и отладка приложения
