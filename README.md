<h1>Описание</h1>
Проект выполняет расчет скорости распространения заболевания в городах заданной страны. Динамика для каждого города рассчитывается отдельно.
При подсчете результатов учитываются принимаемые меры для предотвращения эпидемии в каждом городе. 
<br />
<h1>DiseaseMapMVC</h1>
Основной проект, содержит:
<br />
<br />
<ul>
<li>Логику расчета распространения заданного заболевания для заданной болезни.</li>
<li>Редактируемые таблицы с данными по странам и городам.</li>
</ul>
<br /> 
Поддерживаемые языки русский и английский.
<h1>База данных</h1>
В качестве базы данных использован MongoDB. Структура базы данных находится в проекте DiseaseMongoModel. 
<br />
<br />
Чтобы заполнить базу данных можно:
<br />
<ul>
<li>Запустить консольное приложение DiseaseDataFilling, но с помощью этого приложения нельзя добавить новые, заранее не прописанные в приложении данные.</li>
<li>Запустить приложение DiseaseMapMVC и на главной странице выбрать пункт меню "Задать параметры страны". Таким образом можно добавить страны и города в них, 
но не новую болезнь.</li>
</ul>
